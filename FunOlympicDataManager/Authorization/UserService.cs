using FunOlympicDataManager.Authorization;
using FunOlympicDataManager.Library.DataAccess.Interface;
using FunOlympicDataManager.Library.DataAccess.Internal;
using FunOlympicDataManager.Library.Model;
using FunOlympicDataManager.Library.Models;
using FunOlympicDataManager.Library.ResponseModel;

namespace FunOlympicDataManager.Helpers;

public interface IUserService
{
    LoginResponse Authenticate(LoginModel model, string ipAddress);
    RefreshTokenListResponse GetById(string userID);
    LoginResponse RefreshToken(string token, string ipAddress);
    RefreshTokenResponse RevokeToken(string token, string ipAddress);
    
}


public class UserService : IUserService
{
    private readonly ILoginData _login;
    private readonly IJwtUtils _jwtUtils;
    private readonly ISqlDataAccess _sql;
   

    public UserService(ILoginData login, IJwtUtils jwtUtils, ISqlDataAccess sql)
    {
        _login = login;
        _jwtUtils = jwtUtils;
        _sql = sql;
       
    }

    public LoginResponse Authenticate(LoginModel model, string ipAddress)
    {
        LoginResponse sr = new();
        try
        {
            if (model.userName == null || model.password == null)
                return null;
            var user = _login.login(model);

            // validate
            if (user.statusCode != 0)
            {
                sr.statusCode = user.statusCode;
                sr.statusMessage = user.statusMessage;
                return sr;
            }
            var loggeduser = user.data;
            UserIDModel user1 = new();
            user1.UserID = loggeduser.ID;
            user1.UserName = model.userName;
            // authentication successful so generate jwt and refresh tokens
            var jwtToken = _jwtUtils.GenerateJwtToken(user1);
            RefreshTokenModel refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            refreshToken.UserID = user1.UserID;
            loggeduser.token = jwtToken;
            // save changes to db
            InsertRefreshToken(refreshToken);
            // remove old refresh tokens from user
            removeOldRefreshTokens(refreshToken);
            loggeduser.RefreshToken = refreshToken.Token;
            sr.data = loggeduser;
            sr.statusMessage = "Success";
            sr.statusCode = 0;
        }
        catch (Exception ex)
        {
            sr.statusCode = 3;
            sr.statusMessage= ex.Message;
            sr.data = null;
        }
        return sr;
        // save changes to db
        
    }

    

    private void InsertRefreshToken(RefreshTokenModel refreshToken)
    {
        string q = @"INSERT INTO [dbo].[RefreshToken] ([Token],[Expires],[Created],[CreatedByIp],[UserID])
                Values(@Token,@Expires,@Created,@CreatedByIp,@UserID)";
        _sql.SaveDataQ(q, refreshToken, "SqlConn");
    }

    public LoginResponse RefreshToken(string token, string ipAddress)
    {
        LoginResponse sr = new();

        var tokenModel = getUserByRefreshToken(token);
        if(tokenModel == null)
        {
            sr.statusMessage = "Invalid Refresh token";
            sr.statusCode = 2;
            return sr;
        }
        string q = @"select UserName from Users where ID=@userid";
        var p = new { userid = tokenModel.UserID };
        string username = _sql.LoadFirstData<string, dynamic>(q, p, "SqlConn");
        if (username == null || username.Trim() =="")
        {
            sr.statusCode = 1;
            sr.statusMessage = "Warning user not found";
            return sr;
        }
       
        if (tokenModel.IsRevoked)
        {
            // revoke all descendant tokens in case this token has been compromised
            revokeDescendantRefreshTokens( tokenModel, ipAddress,
                $"Attempted reuse of revoked ancestor token: {token}");           
        }

        if (!tokenModel.IsActive)
        {
            sr.statusCode = 2;
            sr.statusMessage = "Invalid Token";
            return sr;        
        }

        // replace old refresh token with a new one (rotate token)
        var newRefreshToken = rotateRefreshToken(tokenModel, ipAddress);
        RefreshTokenModel rs = new RefreshTokenModel();
        newRefreshToken.UserID = tokenModel.UserID;
        InsertRefreshToken(newRefreshToken);

         // remove old refresh tokens from user
         removeOldRefreshTokens(tokenModel);

        // save changes to db
        UserIDModel user = new ();
        user.UserID = tokenModel.UserID;
        user.UserName = username;
        // generate new jwt
        var jwtToken = _jwtUtils.GenerateJwtToken(user);
        TokenModel data = new();
        data.ID = newRefreshToken.UserID;
        data.RefreshToken = newRefreshToken.Token;
        data.token = jwtToken;
        
        sr.data = data;
        sr.statusMessage = "Success";
        sr.statusCode = 0;
        return sr;
    }

    public RefreshTokenResponse RevokeToken(string token, string ipAddress)
    {
        RefreshTokenResponse sr = new();
        try
        {
            if(token == null)
            {
                sr.statusMessage = "Token is required";
                sr.statusCode = 1;
                return sr;
            }
            var refreshToken = getUserByRefreshToken(token);
            if (refreshToken == null)
            {
                sr.statusMessage = "Invalid Token";
                sr.statusCode = 1;
                return sr;
            }
            if (!refreshToken.IsActive)
            {
                sr.statusMessage = "Invalid Token";
                sr.statusCode = 1;
                return sr;
            }

            // revoke token and save
            sr = revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
        }
        catch(Exception e)
        {
            sr.statusMessage = e.Message;
            sr.statusCode = 3;
        }
        return sr;
    }
    public RefreshTokenListResponse GetById(string userID)
    {
        RefreshTokenListResponse sr = new();
        try
        {
            if (userID == null)
            {
                sr.statusMessage = "UserID is required";
                sr.statusCode = 1;
                return sr;
            }
            string q = "select * from Activeuser where UserID=@UserID";
            var p = new { UserID = userID };
          
            var user = _sql.LoadDataq<RefreshTokenModel, dynamic>(q, p, "SqlConn");
            sr.statusMessage = "Success";
            sr.statusCode = 0;
            sr.data = user;
        }
        catch (Exception e)
        {
            sr.statusMessage = e.Message;
            sr.statusCode = 3;
        }
        return sr;
    }

    // helper methods

    private RefreshTokenModel getUserByRefreshToken(string token)
    {
        string q = "select * from Activeuser where Token=@Token";
        var p= new {Token = token};

        var user = _sql.LoadFirstData<RefreshTokenModel, dynamic>(q, p, "SqlConn");

        if (user == null)
            return null;

        return user;
    }

    private RefreshTokenModel rotateRefreshToken(RefreshTokenModel refreshToken, string ipAddress)
    {
        var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
        revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
        return newRefreshToken;
    }
    private void removeOldRefreshTokens(RefreshTokenModel refreshToken)
    {

        // remove old inactive refresh tokens from user based on TTL in app settings
        try
        {
            string q = "delete from RefreshToken where  Expires+1 <=GETDATE() and UserID=@UserID";
            var p = new { UserID = refreshToken.UserID };
            _sql.SaveDataQ(q, p, "SqlConn");
        }
        catch { }     
    }
    private void revokeDescendantRefreshTokens(RefreshTokenModel refreshToken,  string ipAddress, string reason)
    {
        // recursively traverse the refresh token chain and ensure all descendants are revoked
        if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
        {
            var childToken = getUserByRefreshToken(refreshToken.ReplacedByToken);
            if (childToken == null)
            {
                return;
            }
            if (childToken.IsActive)
                revokeRefreshToken(childToken, ipAddress, reason);
            else
                revokeDescendantRefreshTokens(childToken, ipAddress, reason);
        }
    }
    private RefreshTokenResponse revokeRefreshToken(RefreshTokenModel token, string ipAddress, string reason = null, string replacedByToken = null)
    {
        RefreshTokenResponse sr = new RefreshTokenResponse();
        try
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
            string q = @"update RefreshToken set [Revoked]=@Revoked,RevokedByIp=@RevokedByIp,
                    [ReasonRevoked]=@ReasonRevoked,ReplacedByToken=@ReplacedByToken where Token=@Token";
            _sql.SaveDataQ(q, token, "SqlConn");
            sr.statusCode = 0;
            sr.data = token;
            sr.statusMessage = "Success";
        }
        catch (Exception e)
        {
            sr.statusCode = 3;
            sr.statusMessage = e.Message;
        }
        return sr;
    }
}

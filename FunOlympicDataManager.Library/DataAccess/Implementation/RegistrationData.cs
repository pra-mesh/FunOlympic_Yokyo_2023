using Dapper;
using FunOlympicDataManager.Library.DataAccess.Interface;
using FunOlympicDataManager.Library.DataAccess.Internal;
using FunOlympicDataManager.Library.Models;
using FunOlympicDataManager.Library.ResponseModel;
using Microsoft.AspNetCore.Http;

namespace FunOlympicDataManager.Library.DataAccess.Implementation;
public class RegistrationData : IRegistrationData
{
    private readonly ISqlDataAccess _sql;
    private readonly IMailService _mail;

    public RegistrationData(ISqlDataAccess sql, IMailService mail)
    {
        _sql = sql;
        _mail = mail;
    }
    public async Task<StringResponse> SendResetCodeAsync(MailData gm)
    {
        StringResponse sr = new StringResponse();
        try
        {
            
          
            Random r = new Random();
            var x = r.Next(0, 1000000);

            string s = x.ToString("000000");
            gm.Body = "<br/><br/>Dear User<br/>Your resetcode is <br/>" + " <br/><br/> " + s + "</a> ";
            bool result = await _mail.SendAsync(gm, new CancellationToken());
            if (!result)
            {
                sr.statusCode = 1;
                sr.statusMessage = "An error occured. The Mail could not be sent.";
                sr.data = "Failed";
                return sr;
            }
            var v= updateResetPassword(new PasswordReset {userID=gm.To,ResetCode=s});
            if (v.statusCode != 0)
            {
                sr.statusCode = 1;
                sr.data = v.data;
                sr.statusMessage = v.statusMessage;
                return sr;
            }
            sr.statusMessage = "Success";
            sr.statusCode = 0;
            sr.data = "Saved";
        }
       catch (Exception ex)
        {
            sr.statusCode = 1;
            sr.statusMessage = ex.Message;
            sr.data = "Failed";
            return sr;
        }
        return sr;
    }
    public StringResponse updateResetPassword(PasswordReset gm)
    {
        StringResponse sr = new();
      
        try
        {
            string q = "Delete from ResetPassword where [EmailID]=@email";
            var p1 = new { @email = gm.userID };
            int i = _sql.SaveDataQ<dynamic>(q, p1, "SqlConn");
             q = @"INSERT INTO ResetPassword
           ([EmailID]
           ,[ResetCode])
     VALUES
           (@userID,@ResetCode)";
            i = _sql.SaveDataQ(q, gm, "SqlConn");
            sr.statusMessage = "Success";
            sr.statusCode = 0;
            sr.data = "Saved";

        }
        catch(Exception ex)
        {
            sr.data = "failed";
            sr.statusCode = 2;
            sr.statusMessage = ex.Message;

        }
        return sr;
    }
    public StringResponse changepassword(LoginModel loginModel)
    {
        StringResponse sr = new StringResponse();

        var p = new DynamicParameters(loginModel);
        try
        {
            _sql.SaveData<dynamic>("dbo.spResetpassword", p, "SqlConn");
            sr.statusCode = 0;
            sr.statusMessage = "Success";
            sr.data = "Password Changed";
        }
        catch (Exception ex)
        {
            sr.data = null;
            sr.statusCode = 1;
            sr.statusMessage = ex.Message;
        }
        //SendVerificationLinkEmail(userModel.Email, Guid.NewGuid().ToString(), "https", "localhost", "7059");
        return sr;

    }

    public StringResponse RestCodeValid(PasswordReset gm)
    {
        StringResponse sr = new();

        try
        {
            string q = @"SELECT [EmailID] as userID
                ,[ResetCode] FROM [dbo].[ResetPassword] where [EmailID]=@userID and ResetCode=@ResetCode";
            var p1 = new { userID = gm.userID };
            var PasswordReset = _sql.LoadFirstData<PasswordReset,dynamic>(q, parameters: gm, "SqlConn");
            if (PasswordReset != null)
            {
                sr.statusMessage = "Success";
                sr.statusCode = 0;
                sr.data = "Saved";
            }
            else
            {
                sr.statusMessage = "Not found";
                sr.statusCode = 1;
                sr.data = "Not found";
            }
        }
        catch (Exception ex)
        {
            sr.data = "failed";
            sr.statusCode = 2;
            sr.statusMessage = ex.Message;

        }
        return sr;
    }
    public UsersResponse Registration(UserModel userModel)
    {
        UsersResponse sr = new UsersResponse();

        var p = new DynamicParameters(userModel);
        try
        {
            _sql.SaveData<dynamic>("dbo.spRegistration", p, "SqlConn");
            sr.statusCode = 0;
            sr.statusMessage = "Success";
            sr.data = UserInfo(userModel.Id).data;
        }
        catch (Exception ex)
        {
            sr.data = null;
            sr.statusCode = 1;
            sr.statusMessage = ex.Message;
        }
        //SendVerificationLinkEmail(userModel.Email, Guid.NewGuid().ToString(), "https", "localhost", "7059");
        return sr;

    }

    public UsersResponse Activate(string userID)
    {
        UsersResponse sr = new UsersResponse();

        var p = new { userID = userID };
        try
        {

            _sql.SaveDataQ<dynamic>("update [Users] set EmailConfirmed= 1 where Id=@userID", p, "SqlConn");
            sr.statusCode = 0;
            sr.statusMessage = "Success";
            sr.data = UserInfo(userID).data;
        }
        catch (Exception ex)
        {
            sr.data = null;
            sr.statusCode = 1;
            sr.statusMessage = ex.Message;
        }
        //SendVerificationLinkEmail(userModel.Email, Guid.NewGuid().ToString(), "https", "localhost", "7059");
        return sr;

    }


    public UsersResponse UserInfo(string userID)
    {
        UsersResponse sr = new UsersResponse();
        UserInfoModel sp;
        var p = new { userID };
        try
        {
            sp = _sql.LoadFirstData<UserInfoModel, dynamic>("Select * from users where Id=@userID", p, "SqlConn");
            if (sp.username == "")
            {
                sr.statusCode = 1;
                sr.statusMessage = "User not found";
                return sr;
            }
            sr.statusCode = 0;
            sr.statusMessage = "Success";
            sr.data = sp;
        }
        catch (Exception ex)
        {
            sr.data = null;
            sr.statusCode = 3;
            sr.statusMessage = ex.Message;
        }
        return sr;

    }

}

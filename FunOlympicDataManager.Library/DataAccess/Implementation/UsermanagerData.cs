using FunOlympicDataManager.Library.DataAccess.Interface;
using FunOlympicDataManager.Library.DataAccess.Internal;
using FunOlympicDataManager.Library.Models;
using FunOlympicDataManager.Library.ResponseModel;

namespace FunOlympicDataManager.Library.DataAccess.Implementation;
public class UsermanagerData : IUsermanagerData
{

    private readonly ISqlDataAccess _sql;
    private readonly IRegistrationData _registration;

    public UsermanagerData(ISqlDataAccess sql, IRegistrationData registration)
    {
        _sql = sql;
        _registration = registration;
    }
    public UsersListResponse UserInfo()
    {
        UsersListResponse sr = new UsersListResponse();
        List<UserInfoModel> sp;
        var p = new { };
        try
        {
            sr.data = _sql.LoadDataq<UserInfoModel, dynamic>("Select * from users order by FirstName,LastName", p, "SqlConn");
            sr.statusCode = 0;
            sr.statusMessage = "Success";
        }

        catch (Exception ex)
        {
            sr.data = null;
            sr.statusCode = 1;
            sr.statusMessage = ex.Message;
        }
        return sr;

    }

    public BooleanResponse UpdateIsdiabled(string userID, bool isDisabled)
    {
        UsersResponse si = _registration.UserInfo(userID);
        BooleanResponse sr = new BooleanResponse();
        if (si.data == null)
        {
            sr.statusCode = 1;
            sr.statusMessage = si.statusMessage;
            sr.data = false;
            return sr;
        }
        try
        {

            var p = new { userID, isDisabled };

            _sql.SaveDataQ<dynamic>("update users set isDisabled=@isDisabled where Id=@userID", p, "SqlConn");
            sr.statusCode = 0;
            sr.statusMessage = "Success";
            sr.data = true;
        }
        catch (Exception ex)
        {
            sr.data = false;
            sr.statusCode = 1;
            sr.statusMessage = ex.Message;
        }
        return sr;

    }

    public BooleanResponse UpdateRoles(string userID, string Roles)
    {
        UsersResponse si = _registration.UserInfo(userID);
        BooleanResponse sr = new BooleanResponse();
        if (si.data == null)
        {
            sr.statusCode = 1;
            sr.statusMessage = si.statusMessage;
            sr.data = false;
            return sr;
        }
        try
        {

            var p = new { userID, Roles };

            _sql.SaveDataQ<dynamic>("update users set Roles=@Roles,isDisabled=@isDisabled  where Id=@userID", p, "SqlConn");
            sr.statusCode = 0;
            sr.statusMessage = "Success";
            sr.data = true;
        }
        catch (Exception ex)
        {
            sr.data = false;
            sr.statusCode = 1;
            sr.statusMessage = ex.Message;
        }
        return sr;

    }


    public BooleanResponse UpdateUsers(UserUpdateModel user)
    {
        UsersResponse si = _registration.UserInfo(user.Id);
        BooleanResponse sr = new BooleanResponse();
        if (si.statusCode != 0)
        {
            sr.statusCode = si.statusCode;
            sr.statusMessage = si.statusMessage;
            sr.data = false;
            return sr;
        }
        try
        {

            var p = new { userID = user.Id, Roles = user.Roles, isDisabled = user.isDisabled };

            _sql.SaveDataQ<dynamic>("update users set Roles=@Roles,isDisabled=@isDisabled  where Id=@userID", p, "SqlConn");
            sr.statusCode = 0;
            sr.statusMessage = "Success";
            sr.data = true;
        }
        catch (Exception ex)
        {
            sr.data = false;
            sr.statusCode = 3;
            sr.statusMessage = ex.Message;
        }
        return sr;

    }
}

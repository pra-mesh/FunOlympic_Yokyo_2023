using FunOlympicDataManager.Library.DataAccess.Interface;
using FunOlympicDataManager.Library.DataAccess.Internal;
using FunOlympicDataManager.Library.Models;
using FunOlympicDataManager.Library.ResponseModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FunOlympicDataManager.Library.DataAccess.Implementation;
public class RegistrationData : IRegistrationData
{
    private readonly ISqlDataAccess _sql;
    public RegistrationData(ISqlDataAccess sql)
    {
        _sql = sql;
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
            sp = _sql.LoadFirstData<UserInfoModel,dynamic>("Select * from users where Id=@userID", p, "SqlConn");
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

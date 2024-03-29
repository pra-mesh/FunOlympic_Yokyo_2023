﻿using FunOlympicDataManager.Library.Models;
using FunOlympicDataManager.Library.ResponseModel;

namespace FunOlympicDataManager.Library.DataAccess.Interface;
public interface IRegistrationData
{
    UsersResponse Registration(UserModel userModel);
    UsersResponse Activate(string userID);
    UsersResponse UserInfo(string id);
    StringResponse changepassword(LoginModel loginModel);
    Task<StringResponse> SendResetCodeAsync(MailData gm);
    StringResponse RestCodeValid(PasswordReset gm);
}
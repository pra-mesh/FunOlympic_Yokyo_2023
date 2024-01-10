using FunOlympic_Web.Model;

namespace FunOlympic_Web.Services.Interface;
public interface IUserManagerService
{
    Task<string> changepassword(LoginModel gm);
    Task<bool> RestCodeValid(PasswordReset gm);
    Task<string> Sendresetcode(string Email);
    Task<bool> UpdateUserStatus(UserUpdateModel gm);
    Task<List<UserInfoModel>> UserInfoModels();
}
using FunOlympic_Web.Model;

namespace FunOlympic_Web.Services.Interface;
public interface IAuthenticationService
{
    Task<string> login(LoginModel loginModel);
    Task<bool> Logout();
    Task<bool> Revoke();
}
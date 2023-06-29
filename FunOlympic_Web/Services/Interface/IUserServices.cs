using FunOlympic_Web.Model;

namespace FunOlympic_Web.Services.Interface;
public interface IUserServices
{

    Task<LoggedInModel> LoginAsync(LoginModel user);
}
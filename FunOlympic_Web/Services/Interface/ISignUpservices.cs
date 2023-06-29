using FunOlympic_Web.Model;

namespace FunOlympic_Web.Services.Interface;
public interface ISignUpservices
{
    Task<string> Activate(string ID);
    Task<string> ResendMail();
    Task<string> Signup(UserModel um);
}
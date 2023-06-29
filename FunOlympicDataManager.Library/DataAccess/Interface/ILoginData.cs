using FunOlympicDataManager.Library.Models;
using FunOlympicDataManager.Library.ResponseModel;

namespace FunOlympicDataManager.Library.DataAccess.Interface;
public interface ILoginData
{
    LoginResponse login(LoginModel loginModel);
}
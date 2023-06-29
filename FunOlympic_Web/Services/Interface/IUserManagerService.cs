using FunOlympic_Web.Model;

namespace FunOlympic_Web.Services.Interface;
public interface IUserManagerService
{
    Task<List<UserInfoModel>> UserInfoModels();
}
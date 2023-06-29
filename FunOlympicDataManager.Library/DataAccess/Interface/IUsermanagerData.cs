using FunOlympicDataManager.Library.Models;
using FunOlympicDataManager.Library.ResponseModel;

namespace FunOlympicDataManager.Library.DataAccess.Interface;
public interface IUsermanagerData
{
    BooleanResponse UpdateIsdiabled(string userID, bool isDisabled);
    BooleanResponse UpdateRoles(string userID, string Roles);
    BooleanResponse UpdateUsers(UserUpdateModel user);
    UsersListResponse UserInfo();
}
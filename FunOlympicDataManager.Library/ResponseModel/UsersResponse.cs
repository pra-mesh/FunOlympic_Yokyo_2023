
using FunOlympicDataManager.Library.Models;

namespace FunOlympicDataManager.Library.ResponseModel
{
    public class UsersResponse : BaseResponse
    {
        public UserInfoModel? data { get; set; }
    }

    public class UsersListResponse : BaseResponse
    {
        public List<UserInfoModel>? data { get; set; }
    }
}

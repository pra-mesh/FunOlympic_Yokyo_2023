namespace FunOlympic_Web.Model.ResponseModel;
    public class UsersResponse : BaseResponse
    {
        public UserModel ? data { get; set; }
    }

public class UsersInfoResponse : BaseResponse
{
    public List<UserInfoModel>? data { get; set; }
}


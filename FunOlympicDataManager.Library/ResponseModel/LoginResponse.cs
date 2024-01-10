using FunOlympicDataManager.Library.Models;

namespace FunOlympicDataManager.Library.ResponseModel;
public class LoginResponse : BaseResponse
{
    public TokenModel? data { get; set; }
}

namespace FunOlympic_Web.Model.ResponseModel;
public class BooleanResponse : BaseResponse
{
    public bool data { get; set; } = false;
}
public class StringResponse : BaseResponse
{
    public string data { get; set; }
}
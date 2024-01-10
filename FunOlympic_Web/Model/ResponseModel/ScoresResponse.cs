
namespace FunOlympic_Web.Model.ResponseModel;
public class ScoresResponse: BaseResponse
{
    public ScoreModel? data { get; set; }
}

public class ScoresListResponse : BaseResponse
{
    public List<ScoreModel>? data { get; set; }
}

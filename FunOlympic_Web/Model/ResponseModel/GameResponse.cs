namespace FunOlympic_Web.Model.ResponseModel;
public class GameResponse :BaseResponse
{
    public GameModel? data { get; set; }
}

public class ListGameResponse : BaseResponse
{
    public List<GameModel>? data { get; set; }
}


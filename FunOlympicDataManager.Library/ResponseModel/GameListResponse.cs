using FunOlympicDataManager.Library.Models;

namespace FunOlympicDataManager.Library.ResponseModel;
public class GameListResponse :BaseResponse
{
    public List<GameListModel>? data { get; set; }
}

using FunOlympicDataManager.Library.ResponseModel;

namespace FunOlympicDataManager.Library.DataAccess.Interface;
public interface IGameListData
{
    GameListResponse gameList();
}
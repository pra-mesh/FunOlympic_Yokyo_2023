using FunOlympicDataManager.Library.Models;
using FunOlympicDataManager.Library.ResponseModel;
using Microsoft.AspNetCore.Http;

namespace FunOlympicDataManager.Library.DataAccess.Interface;
public interface IGameData
{
    GameResponse Game(string id);
    GameListResponse gameList(bool live);
    Task<StringResponse> Imageupload(IFormFile file);
    Task<GameResponse> InsertGames(GameImg model);
    ListGameResponse ListofHighlightse(int GameID, int count, DateTime? date);
    ListGameResponse ListofliveGame(int GameID, int count, DateTime? date);
    Task<StringResponse> Videoupload(IFormFile file);
}
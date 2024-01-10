using FunOlympic_Web.Model;

namespace FunOlympic_Web.Services.Interface;
public interface IGamesService
{
    Task<List<GameListModel>> GameLists();
    Task<GameModel> GamesVideo(string gameId);
    Task<List<GameListModel>> HighlightsGameList();
    Task<ScoreModel> InsertScores(ScoresInsertModel sc);
    Task<List<GameListModel>> LiveGameLists();
    Task<GameModel> InsertLiveGames(MultipartFormDataContent sc);
    Task<List<ScoreModel>> ScoresList(int gameId);
    Task<List<GameModel>> TopHIghlights(int gameId);
    Task<List<GameModel>> TopLiveGame(int gameId);
    Task<List<ScoreModel>> TopScores(int gameId);
    Task<List<GameModel>> LiveGames(int gameId);
    Task<List<GameModel>> HIghlights(int gameId);
}
using FunOlympicDataManager.Library.Models;
using FunOlympicDataManager.Library.ResponseModel;

namespace FunOlympicDataManager.Library.DataAccess.Interface;
public interface IScoresData
{
    ScoresResponse InsertScores(ScoresInsertModel model);
    ScoresResponse Scores(int scoreID);
    ScoresListResponse ScoresList(int gameId, int count);
}
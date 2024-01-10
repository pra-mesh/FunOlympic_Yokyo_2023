using Dapper;
using FunOlympicDataManager.Library.DataAccess.Interface;
using FunOlympicDataManager.Library.DataAccess.Internal;
using FunOlympicDataManager.Library.Models;
using FunOlympicDataManager.Library.ResponseModel;
using System.Data;

namespace FunOlympicDataManager.Library.DataAccess.Implementation;
public class ScoresData : IScoresData
{
    private readonly ISqlDataAccess _sql;

    public ScoresData(ISqlDataAccess sql)
    {
        _sql = sql;
    }

    public ScoresResponse Scores(int scoreID)
    {
        ScoresResponse sr = new ScoresResponse();
        try
        {
            var p = new { scoreID };
            string q = @"Select * from AllScores where id =@scoreID ";
            var data = _sql.LoadFirstData<ScoreModel, dynamic>(q, p, "SqlConn");
            if(data == null)
            {
                sr.statusMessage = "Score not found";
                sr.statusCode = 1;
                return sr;
            }
            sr.data = data;
            sr.statusMessage = "Success";
            sr.statusCode = 0;

        }
        catch (Exception ex)
        {
            sr.statusCode = 3;
            sr.statusMessage = ex.Message;
            sr.data = null;
        }
        return sr;
    }

    public ScoresListResponse ScoresList(int gameId, int count)
    {
        ScoresListResponse sr = new ScoresListResponse();
        try
        {
            var p = new DynamicParameters();
            string q = @"Select * from AllScores  order by [GameDate] desc ";
            if (count == 4)
                q = @"Select top(4) * from AllScores order by [GameDate] desc ";
            if (gameId != 0)
            {
                p.Add("@gameId", gameId, DbType.Int32);
                q = @"Select * from AllScores where GameID =@gameId order by [GameDate] desc ";
                if (count == 4)
                    q = @"Select top(4) * from AllScores where GameID =@gameId order by [GameDate] desc ";
            }
            var data = _sql.LoadDataq<ScoreModel, dynamic>(q, p, "SqlConn");
            sr.data = data;
            if (data.Count<1)
            {
                sr.statusMessage = "Score not found";
                sr.statusCode = 1;
                return sr;
            }
            
            sr.statusMessage = "Success";
            sr.statusCode = 0;

        }
        catch (Exception ex)
        {
            sr.statusCode = 3;
            sr.statusMessage = ex.Message;
            sr.data = null;
        }
        return sr;
    }

    public ScoresResponse InsertScores(ScoresInsertModel model)
    {
        ScoresResponse sr = new ScoresResponse();
        try
        {
            string q = @"INSERT INTO [dbo].[Scores2P]
           ([GameDate]
           ,[Team1]
           ,[team1Score]
           ,[team2]
           ,[team2Score]
           ,[GameID],[Description])
     VALUES (@GameDate,@Team1,@team1Score,@team2,@team2Score,@GameID,@Description); SELECT CAST(SCOPE_IDENTITY() as int)";
            var i = _sql.SaveDataQ<ScoresInsertModel>(q, model, "SqlConn");
            ScoresResponse si = new ScoresResponse();
            si = Scores(i);
            if (si.statusCode != 0)
            {
                sr.statusCode = si.statusCode;
                sr.statusMessage = si.statusMessage;
                sr.data = null;
                return sr;
            }
            sr.data = si.data;
            sr.statusMessage = "Success";
            sr.statusCode = 0;
        }
        catch (Exception ex)
        {
            sr.statusCode = 3;
            sr.statusMessage = ex.Message;
            sr.data = null;
        }
        return sr;
    }


}

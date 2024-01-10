using Dapper;
using FunOlympicDataManager.Library.DataAccess.Interface;
using FunOlympicDataManager.Library.DataAccess.Internal;
using FunOlympicDataManager.Library.Helper;
using FunOlympicDataManager.Library.Models;
using FunOlympicDataManager.Library.ResponseModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FunOlympicDataManager.Library.DataAccess.Implementation;
public class GameData : IGameData
{
    private readonly ISqlDataAccess _sql;

    public GameData(ISqlDataAccess sql)
    {
        _sql = sql;
    }
    public ListGameResponse ListofliveGame(int GameID, int count, DateTime? date)
    {
        ListGameResponse sr = new ListGameResponse();
        try
        {
            string st = "";
            if (count == 4)
                st = " top(4) ";
            var p = new DynamicParameters();
            string q = @$"Select {st} * from AllGames where livestatus=1 ";
            if (date is not null)
            {
                p.Add("@gameDate", Convert.ToDateTime(date).Date, DbType.Date);
                q = q + " and GameDate=@gameDate";
            }
            if (GameID != 0)
            {
                p.Add("@gameId", GameID, DbType.Int32);
                q = q + " and gameId=@gameId";
            }
            q = q + " order by GameDate";
            var data = _sql.LoadDataq<GameModel, dynamic>(q, p, "SqlConn");
            if (data == null)
            {
                sr.statusMessage = "Games not found";
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
    public ListGameResponse ListofHighlightse(int GameID, int count, DateTime? date)
    {
        ListGameResponse sr = new ListGameResponse();
        try
        {
            string st = "";
            if (count == 4)
                st = " top(4) ";
            var p = new DynamicParameters();
            string q = @$"Select {st} * from AllGames where livestatus=0 ";
            if (date is not null)
            {
                p.Add("@gameDate", Convert.ToDateTime(date).Date, DbType.Date);
                q = q + " and GameDate=@gameDate";
            }
            if (GameID != 0)
            {
                p.Add("@gameId", GameID, DbType.Int32);
                q = q + " and gameId=@gameId";
            }
            q = q + " order by GameDate";
            var data = _sql.LoadDataq<GameModel, dynamic>(q, p, "SqlConn");
            if (data == null)
            {
                sr.statusMessage = "Games not found";
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
    public GameResponse Game(string id)
    {
        GameResponse sr = new GameResponse();
        try
        {
            var p = new { id };
            string q = @"Select * from AllGames where id =@id ";
            var data = _sql.LoadFirstData<GameModel, dynamic>(q, p, "SqlConn");
            if (data == null)
            {
                sr.statusMessage = "Game not found";
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

    public async Task<StringResponse> Imageupload(IFormFile file)
    {
        ImageUpload ig = new ImageUpload();
        StringResponse sr = new StringResponse();
        if (file != null)
        {
            Guid guid = Guid.NewGuid();
            string st = await ig.UploadedFile(file, guid.ToString(), "Thumbnails");
            if (st == null || st == "")
            {
                sr.data = "";
                sr.statusCode = 1;
                sr.statusMessage = "Image upload failed";
                return sr;
            }
            sr.data = st;
            sr.statusCode = 0;
            sr.statusMessage = "Image upload";
            return sr;
        }
        sr.data = "";
        sr.statusMessage = "Image not found";
        sr.statusCode = 2;
        return sr;

    }

    public async Task<StringResponse> Videoupload(IFormFile file)
    {
        ImageUpload ig = new ImageUpload();
        StringResponse sr = new StringResponse();
        if (file != null)
        {
            Guid guid = Guid.NewGuid();
            string st = "";
            st = await ig.UploadedFile(file, guid.ToString(), "Video");
            if (st == null || st == "")
            {
                sr.data = ""; sr.statusCode = 1;
                sr.statusMessage = "Video upload failed";
                return sr;
            }
            sr.data = st;
            sr.statusCode = 0;
            sr.statusMessage = "Video upload";
            return sr;

        }
        sr.data = "";
        sr.statusMessage = "Video not found";
        sr.statusCode = 2;
        return sr;

    }

    public async Task<GameResponse> InsertGames(GameImg model)
    {
        GameResponse sr = new GameResponse();
        try
        {
            ImageUpload ig = new ImageUpload();
            GamesInsert gm = new GamesInsert(model.id, model.GameDate, model.Team1, model.team1Score, model.team2, model.team2Score,
                model.GameID, model.LinktoGame, model.Livestatus, model.GameEndDate, model.Description, model.photoloc,model.streamID); ;
           
            if (model.video != null)
            {
                gm.LinktoGame = await ig.UploadedFile(model.video, model.id, "Videos");
                if (gm.LinktoGame == null)
                {
                    sr.data = null; sr.statusCode = 1;
                    sr.statusMessage = "Videos upload failed";
                    return sr;
                }
            }
            Guid guid = Guid.NewGuid();
            model.id = guid.ToString();
            string q = @"INSERT INTO [dbo].[Games]
           ([Id]
           ,[GameDate]
           ,[Team1]
           ,[team1Score]
           ,[team2]
           ,[team2Score]
           ,[GameID]
           ,[LinktoGame]
           ,[Livestatus]
           ,[GameEndDate]
           ,[Description]
           ,[photoloc])
     VALUES
           (@id,@GameDate,@Team1,@team1Score,@Team2,@team2Score,@GameID,@LinktoGame,@Livestatus,@GameEndDate,@Description,@photoloc)";
            var i = _sql.SaveDataQ<GameImg>(q, model, "SqlConn");
            GameResponse si = new GameResponse();
            si = Game(model.id);
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

    public GameListResponse gameList(bool live)
    {
        GameListResponse sr = new GameListResponse();
        try
        {
            string q = "Select Distinct GameName,GameID as ID,'' as GameType from AllGames where livestatus=0";
            if (live)
                q = "Select Distinct GameName,GameID as ID,'' as GameType from AllGames where livestatus=1";
          
            var p = new { };
            var data = _sql.LoadDataq<GameListModel, dynamic>(q, p, "SqlConn");
            sr.statusCode = 0;
            sr.data = data;
            sr.statusMessage = "Success";
        }
        catch (Exception ex)
        {
            sr.data = null;
            sr.statusMessage = ex.Message;
            sr.statusCode = 3;
        }
        return sr;
    }

}

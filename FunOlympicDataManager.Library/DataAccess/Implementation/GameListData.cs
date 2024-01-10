using FunOlympicDataManager.Library.DataAccess.Interface;
using FunOlympicDataManager.Library.DataAccess.Internal;
using FunOlympicDataManager.Library.Models;
using FunOlympicDataManager.Library.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunOlympicDataManager.Library.DataAccess.Implementation;
public class GameListData : IGameListData
{
    private readonly ISqlDataAccess _sql;

    public GameListData(ISqlDataAccess sql)
    {
        _sql = sql;
    }

    public GameListResponse gameList()
    {
        GameListResponse sr = new GameListResponse();
        try
        {
            var p = new { };
            var data = _sql.LoadDataq<GameListModel, dynamic>("Select * from Gamelist", p, "SqlConn");
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

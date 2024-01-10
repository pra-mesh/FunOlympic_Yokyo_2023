using FunOlympicDataManager.Library.DataAccess.Interface;
using FunOlympicDataManager.Library.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FunOlympicDataManager.Controllers;
[Route("api/[controller]")]
[ApiController]

public class GameListController : ControllerBase
{
    private readonly IGameListData _gameList;

    public GameListController(IGameListData gameList)
    {
        _gameList = gameList;
    }

    [HttpGet]
    public GameListResponse gameList() => _gameList.gameList();
}

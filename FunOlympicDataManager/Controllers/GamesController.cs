using FunOlympicDataManager.Authorization;
using FunOlympicDataManager.Library.DataAccess.Implementation;
using FunOlympicDataManager.Library.DataAccess.Interface;
using FunOlympicDataManager.Library.Models;
using FunOlympicDataManager.Library.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FunOlympicDataManager.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GamesController : ControllerBase
{
    private readonly IGameData _gameData;

    public GamesController(IGameData gameData)
    {
        _gameData = gameData;
    }
    [HttpPost("InsertVideo")]
    public async Task<GameResponse> InsertData([FromForm] GameImg gm) { 
        return await _gameData.InsertGames(gm); }
    [HttpPost("Image")]
    public async Task<StringResponse> ImageUpload([FromForm]IFormFile formFile)
    {
        return await _gameData.Imageupload(formFile);
    }
    [HttpPost("Video")]
    public async Task<StringResponse> VideoUpload([FromForm] IFormFile formFile)
    {
        return await _gameData.Videoupload(formFile);
    }
    [AllowAnonymous]
    [HttpGet("TopLives/{GameID}")]
    public ListGameResponse Livetop4(int GameID) => _gameData.ListofliveGame(GameID, 4, null);

    
    [HttpGet("AllLives/{GameID}")]
    public ListGameResponse LiveAll(int GameID) => _gameData.ListofliveGame(GameID, 0, null);
    [AllowAnonymous]
    [HttpGet("TopHighlights/{GameID}")]
    public ListGameResponse Highlightstop4(int GameID) => _gameData.ListofHighlightse(GameID, 4, null);


    [HttpGet("AllHighlights/{GameID}")]
    public ListGameResponse HighlightsAll(int GameID) => _gameData.ListofHighlightse(GameID, 0, null);

    [HttpGet("LiveGameList")]
    public GameListResponse gameList() => _gameData.gameList(true);

    [HttpGet("Videos/{id}")]
    public GameResponse gameVideo(string id) => _gameData.Game(id);

    [HttpGet("HighlightsGameList")]
    public GameListResponse HighlightsgameList() => _gameData.gameList(false);
}

using FunOlympicDataManager.Authorization;
using FunOlympicDataManager.Library.DataAccess.Interface;
using FunOlympicDataManager.Library.Models;
using FunOlympicDataManager.Library.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace FunOlympicDataManager.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ScoreController : ControllerBase
{
    private readonly IScoresData _scoresData;
    
    public ScoreController(IScoresData scoresData)
    {
        _scoresData = scoresData;
    }
    [AllowAnonymous]
    [HttpGet("{scoreID}")]
    public ScoresResponse scores(int scoreID) => _scoresData.Scores(scoreID);

    [AllowAnonymous]
    [HttpGet("TopScore/{GameID}")]
    public ScoresListResponse scorestop4(int GameID) => _scoresData.ScoresList(GameID, 4);

    [HttpGet("List/{GameID}")]
    public ScoresListResponse scoreslist(int GameID) => _scoresData.ScoresList(GameID, 0);

    [HttpPost("insert")]
    public ScoresResponse scoreInsert(ScoresInsertModel gm) => _scoresData.InsertScores(gm);
}

using FunOlympic_Web.Model.ResponseModel;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text.Json;
using FunOlympic_Web.Model;
using FunOlympic_Web.Services.Interface;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Numerics;
using System.Linq;
using System.Net.Http.Headers;

namespace FunOlympic_Web.Services.Implementation;

public class GamesService : IGamesService
{
    private readonly IHttpClientFactory _httpClient;

    public GamesService(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<GameListModel>> GameLists()
    {

        List<GameListModel> ls = new List<GameListModel>();
        var client = _httpClient.CreateClient("API");

        var authResult = await client.GetAsync("/api/GameList");
        var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
        if (authResult.IsSuccessStatusCode == false)
        {
            return ls;
        }
        GameListResponse sr = JsonSerializer.Deserialize<GameListResponse>(
                   data,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        ls = sr.data;
        return ls;
    }

    public async Task<List<GameListModel>> LiveGameLists()
    {

        List<GameListModel> ls = new List<GameListModel>();
        var client = _httpClient.CreateClient("API");

        var authResult = await client.GetAsync("/api/Games/LiveGameList");
        var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
        if (authResult.IsSuccessStatusCode == false)
        {
            return ls;
        }
        GameListResponse sr = JsonSerializer.Deserialize<GameListResponse>(
                   data,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        ls = sr.data;
        return ls;
    }

    public async Task<List<GameListModel>> HighlightsGameList()
    {

        List<GameListModel> ls = new List<GameListModel>();
        var client = _httpClient.CreateClient("API");

        var authResult = await client.GetAsync("/api/Games/HighlightsGameList");
        var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
        if (authResult.IsSuccessStatusCode == false)
        {
            return ls;
        }
        GameListResponse sr = JsonSerializer.Deserialize<GameListResponse>(
                   data,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        ls = sr.data;
        return ls;
    }
    public async Task<List<ScoreModel>> TopScores(int gameId)
    {

        List<ScoreModel> ls = new List<ScoreModel>();
        var client = _httpClient.CreateClient("API");

        var authResult = await client.GetAsync($"/api/Score/TopScore/{gameId}");
        var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
        if (authResult.IsSuccessStatusCode == false)
        {
            return ls;
        }
        ScoresListResponse sr = JsonSerializer.Deserialize<ScoresListResponse>(
                   data,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        ls = sr.data;
        return ls;
    }
    public async Task<List<ScoreModel>> ScoresList(int gameId)
    {

        List<ScoreModel> ls = new List<ScoreModel>();
        var client = _httpClient.CreateClient("API");

        var authResult = await client.GetAsync($"/api/Score/List/{gameId}");
        var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
        if (authResult.IsSuccessStatusCode == false)
        {
            return ls;
        }
        ScoresListResponse sr = JsonSerializer.Deserialize<ScoresListResponse>(
                   data,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        ls = sr.data;
        return ls;
    }
    public async Task<List<GameModel>> TopLiveGame(int gameId)
    {

        List<GameModel> ls = new List<GameModel>();
        var client = _httpClient.CreateClient("API");

        var authResult = await client.GetAsync($"/api/Games/TopLives/{gameId}");
        var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
        if (authResult.IsSuccessStatusCode == false)
        {
            return ls;
        }
        ListGameResponse sr = JsonSerializer.Deserialize<ListGameResponse>(
                   data,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        ls = sr.data;
        return ls;
    }
    public async Task<List<GameModel>> LiveGames(int gameId)
    {

        List<GameModel> ls = new List<GameModel>();
        var client = _httpClient.CreateClient("API");

        var authResult = await client.GetAsync($"/api/Games/AllLives/{gameId}");
        var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
        if (authResult.IsSuccessStatusCode == false)
        {
            return ls;
        }
        ListGameResponse sr = JsonSerializer.Deserialize<ListGameResponse>(
                   data,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        ls = sr.data;
        return ls;
    }
    public async Task<GameModel> GamesVideo(string gameId)
    {

        GameModel ls = new GameModel();
        var client = _httpClient.CreateClient("API");

        var authResult = await client.GetAsync($"/api/Games/Videos/{gameId}");
        var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
        if (authResult.IsSuccessStatusCode == false)
        {
            return ls;
        }
        GameResponse sr = JsonSerializer.Deserialize<GameResponse>(
                   data,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        ls = sr.data;
        return ls;
    }
    public async Task<List<GameModel>> TopHIghlights(int gameId)
    {

        List<GameModel> ls = new List<GameModel>();
        var client = _httpClient.CreateClient("API");

        var authResult = await client.GetAsync($"/api/Games/TopHighlights/{gameId}");
        var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
        if (authResult.IsSuccessStatusCode == false)
        {
            return ls;
        }
        ListGameResponse sr = JsonSerializer.Deserialize<ListGameResponse>(
                   data,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        ls = sr.data;
        return ls;
    }

    public async Task<List<GameModel>> HIghlights(int gameId)
    {

        List<GameModel> ls = new List<GameModel>();
        var client = _httpClient.CreateClient("API");

        var authResult = await client.GetAsync($"/api/Games/AllHighlights/{gameId}");
        var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
        if (authResult.IsSuccessStatusCode == false)
        {
            return ls;
        }
        ListGameResponse sr = JsonSerializer.Deserialize<ListGameResponse>(
                   data,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        ls = sr.data;
        return ls;
    }
    public async Task<ScoreModel> InsertScores(ScoresInsertModel sc)
    {

        ScoreModel model= new ScoreModel();
        var client = _httpClient.CreateClient("API");
        string payload = JsonSerializer.Serialize(sc);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var authResult = await client.PostAsync($"/api/Score/insert",content);
        var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
        if (authResult.IsSuccessStatusCode == false)
        {
            return model;
        }
        ScoresResponse sr = JsonSerializer.Deserialize<ScoresResponse>(
                   data,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        model = sr.data;
        return model;
    }

    public async Task<GameModel> InsertLiveGames(MultipartFormDataContent sc)
    {
        try
        {
            GameModel model = new GameModel();
            var client = _httpClient.CreateClient("API");
            //string payload = JsonSerializer.Serialize(sc);
            
           
           
            var authResult = await client.PostAsync($"/api/Games/Image", sc);
            var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
            if (authResult.IsSuccessStatusCode == false)
            {
                return model;
            }
            GameResponse sr = JsonSerializer.Deserialize<GameResponse>(
                       data,
                       new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            model = sr.data;
            return model;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

  
}

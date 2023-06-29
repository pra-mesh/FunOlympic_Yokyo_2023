using FunOlympic_Web.Model;
using FunOlympic_Web.Model.ResponseModel;
using System.Net.Http;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
using System.Net.Http.Json;
using FunOlympic_Web.Services.Interface;

namespace FunOlympic_Web.Services.Implementation;

public class UserManagerService : IUserManagerService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public UserManagerService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<UserInfoModel>> UserInfoModels()
    {
        UsersInfoResponse sr = new UsersInfoResponse();
        List<UserInfoModel> ls = new List<UserInfoModel>();
        var client = _httpClientFactory.CreateClient("API");

        var authResult = await client.GetAsync("/api/Usermanager/UserInfo");
        var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
        if (authResult.IsSuccessStatusCode == false)
        {
            return ls;
        }
        sr = JsonSerializer.Deserialize<UsersInfoResponse>(
                   data,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        ls = sr.data;
        return ls;
    }
}

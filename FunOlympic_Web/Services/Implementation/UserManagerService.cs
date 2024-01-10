using Blazorise;
using FunOlympic_Web.Helper;
using FunOlympic_Web.Model;
using FunOlympic_Web.Model.ResponseModel;
using FunOlympic_Web.Services.Interface;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

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

    public async Task<bool> UpdateUserStatus(UserUpdateModel gm)
    {
        BooleanResponse sr = new BooleanResponse();
        bool ls = false;
        var client = _httpClientFactory.CreateClient("API");
        string payload = JsonSerializer.Serialize(gm);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var authResult = await client.PostAsync("/api/Usermanager/UpdateUserStatus", content);
        var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
        if (authResult.IsSuccessStatusCode == false)
        {
            return ls;
        }
        sr = JsonSerializer.Deserialize<BooleanResponse>(
                   data,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        ls = sr.data;
        return ls;
    }

    public async Task<bool> RestCodeValid(PasswordReset gm)
    {
        StringResponse sr = new StringResponse();
        bool ls = false;
        var client = _httpClientFactory.CreateClient("API");
        string payload = JsonSerializer.Serialize(gm);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var authResult = await client.PostAsync("/api/Registration/RestCodeValid", content);
        var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
        if (authResult.IsSuccessStatusCode == false)
        {
            return ls;
        }
        sr = JsonSerializer.Deserialize<StringResponse>(
                   data,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if(sr.statusCode ==0)
        ls = true;
        return ls;
    }

    public async Task<string> Sendresetcode(string Email)
    {
        var client = _httpClientFactory.CreateClient("API");
        Guid guid = Guid.NewGuid();
        Random r = new Random();
        var x = r.Next(0, 1000000);
        string s = x.ToString("000000");
        var Message = "<br/><br/>Dear User<br/>Your resetcode is <br/>" + " <br/><br/> " + s + "</a> ";
        MailData md = new MailData(Email, "Password Reset Request", Message, "support@funolympic.com", Email, "support@funolympic.com", "Fun Olympic");
        string payload2 = JsonSerializer.Serialize(md);
        var content2 = new StringContent(payload2, Encoding.UTF8, "application/json");
        var authResult2 = await client.PostAsync("/api/Registration/sendresetcode", content2);

        string status;
        if (authResult2.IsSuccessStatusCode != true)
        {

            status = "Invalid Email Address or try again later";
        }
        else
        {
            status = "Success";
        }
        return status;
    }

    public async Task<string> changepassword(LoginModel gm)
    {
        StringResponse sr = new StringResponse();
        string ls = "Failed";
        var client = _httpClientFactory.CreateClient("API");
        gm.password = Utility.Encrypt(gm.password);
        string payload = JsonSerializer.Serialize(gm);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        var authResult = await client.PostAsync("/api/Registration/changepassword", content);
        var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
        if (authResult.IsSuccessStatusCode == false)
        {
            return ls;
        }
        sr = JsonSerializer.Deserialize<StringResponse>(
                   data,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        if (sr.statusCode == 0)
            ls = sr.data;
        return ls;
    }

}

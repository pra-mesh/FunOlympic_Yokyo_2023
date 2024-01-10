using Blazored.SessionStorage;
using FunOlympic_Web.Helper;
using FunOlympic_Web.Model;
using FunOlympic_Web.Model.ResponseModel;
using FunOlympic_Web.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FunOlympic_Web.Services.Implementation;

public class AuthenticationService : IAuthenticationService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AuthenticationStateProvider _auth;
    private readonly ISessionStorageService _localStorage;
    private readonly IConfiguration _config;

    public AuthenticationService(IHttpClientFactory httpClientFactory,
                              AuthenticationStateProvider Auth,
                              ISessionStorageService localStorage,
                              IConfiguration config)
    {
        _httpClientFactory = httpClientFactory;
        _auth = Auth;
        _localStorage = localStorage;
        _config = config;
    }

    public async Task<string> login(LoginModel loginModel)
    {
        var Auth = _config.GetValue<string>("AuthStorageKey");
        LoginResponse lm = new LoginResponse();

        try
        {
            loginModel.password = Utility.Encrypt(loginModel.password);
            var client = _httpClientFactory.CreateClient("API");
            string payload = JsonSerializer.Serialize(loginModel);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var authResult = await client.PostAsync("/api/Login/token", content);

            var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
            if (authResult.IsSuccessStatusCode == false)
            {
                return null;
            }
            lm = data.Deserialize<LoginResponse>(
                 new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (lm.statusCode == 0)
            {
                LoggedInModel loggedID = lm.data;
                if (lm.data is not null)
                {
                    await _localStorage.SetItemAsStringAsync("tempStoredValue", loggedID.ID);
                    await _localStorage.SetItemAsStringAsync("userName", loggedID.UserName);
                    await _localStorage.SetItemAsStringAsync("Email", loginModel.userName);

                    if (!loggedID.EmailConfirmed)
                    {
                        return "EmailVerify";
                    }

                    if (loggedID.isDisabled)
                    {
                        return "Disabled";
                    }
                    await _localStorage.SetItemAsStringAsync(Auth, "Authorized");

                    ((CustomAuthenticationStateProvider)_auth).NotifyUserAuthentication("Authorized", loggedID.UserName, loggedID.EmailConfirmed, loggedID.Roles);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loggedID.token);
                    await _localStorage.RemoveItemAsync("tempStoredValue");
                }
                else
                {
                    lm.statusMessage = "Failed";
                }
            }
        }
        catch (Exception ex)
        {

        }

        return lm.statusMessage;
    }
    public async Task<bool> Revoke()
    {


        try
        {
            string model = "md";

            //var postBody = new { model = "Blazor POST Request Example" };
            var _httpClient = _httpClientFactory.CreateClient("API");
            var authResult = await _httpClient.PostAsync("/api/Login/revoke-token/model" + model, null);
            var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
            var s = authResult.RequestMessage;
            var st = authResult.StatusCode;
            if (authResult.IsSuccessStatusCode == false)
            {
                var br = data.Deserialize<LoginResponse>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return false;
            }
            var result = data.Deserialize<LoginResponse>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex)
        {
        }
        return false;
    }
    public async Task<bool> Logout()
    {

        //var postBody = new { model = "Blazor POST Request Example" };
        var _httpClient = _httpClientFactory.CreateClient("API");
        var authResult = await _httpClient.PostAsync("/api/Login/Logout", null);
        var st = authResult.IsSuccessStatusCode;
        if (authResult.IsSuccessStatusCode == true)
        {
            var Auth = _config.GetValue<string>("AuthStorageKey");
            await _localStorage.RemoveItemAsync(Auth);
            await _localStorage.RemoveItemAsync("userName");
            await _localStorage.RemoveItemAsync("Email");

            ((CustomAuthenticationStateProvider)_auth).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
            return true;
        }
        return false;

    }
}

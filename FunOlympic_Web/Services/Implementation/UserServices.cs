using FunOlympic_Web.Helper;
using FunOlympic_Web.Model;
using FunOlympic_Web.Model.ResponseModel;
using FunOlympic_Web.Services.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FunOlympic_Web.Services.Implementation;

public class UserServices : IUserServices
{
    public HttpClient _httpClient { get; }
    public AppSettings _appSettings { get; }

    public UserServices(HttpClient httpClient, IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
        //httpClient.BaseAddress = new Uri(_appSettings.BaseAddress);

        _httpClient = httpClient;
    }

    public async Task<LoggedInModel> LoginAsync(LoginModel user)
    {
        user.password = Utility.Encrypt(user.password);
        string serializedUser = JsonConvert.SerializeObject(user);

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7059/api/Login/token");
        requestMessage.Content = new StringContent(serializedUser);

        requestMessage.Content.Headers.ContentType
            = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        string responseBody = "";
        try
        {
            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
             responseBody = await response.Content.ReadAsStringAsync();
        }
        catch(Exception ex)
        {
            
        }
        var returnedUser = JsonConvert.DeserializeObject<LoginResponse>(responseBody);

        return await Task.FromResult(returnedUser.data);

    }

}

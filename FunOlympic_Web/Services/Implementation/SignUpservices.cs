using Blazored.SessionStorage;
using FunOlympic_Web.Helper;
using FunOlympic_Web.Model;
using FunOlympic_Web.Model.ResponseModel;
using FunOlympic_Web.Services.Interface;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace FunOlympic_Web.Services.Implementation;

public class SignUpservices : ISignUpservices
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly NavigationManager _navigation;
    private readonly ISessionStorageService _localStorage;

    public SignUpservices(IHttpClientFactory httpClientFactory, NavigationManager navigation, ISessionStorageService localStorage)
    {
        _httpClientFactory = httpClientFactory;
        _navigation = navigation;
        _localStorage = localStorage;
    }
    public async Task<string> Signup(UserModel um)
    {
        var client = _httpClientFactory.CreateClient("API");
        string resgiter = "Not Registered";
        Guid guid = Guid.NewGuid();
        um.Id = guid.ToString();
        var baseuri = _navigation.BaseUri;
        var baseurl = baseuri + "/activate/" + um.Id;
        var Message = "<br/><br/>Welcome to Advance Cyber Security.<br/>Your Activation Link is <br/>" + " <br/><br/><a href='" + baseurl + "'>Click Here</a> ";
        MailData md = new MailData(um.Email, "Welcome to Fun Olympic 2023", Message, "support@funolympic.com", um.FirstName + ' ' + um.LastName, "support@funolympic.com", "Fun Olympic");
        string payload2 = JsonSerializer.Serialize(md);
        var content2 = new StringContent(payload2, Encoding.UTF8, "application/json");
        var authResult2 = await client.PostAsync("/api/Registration/sendmail", content2);

        if (authResult2.IsSuccessStatusCode != true)
        {

            resgiter = "Invalid Email Address or try again later";
        }
        else
        {
            um.Password = Utility.Encrypt(um.Password);

            UsersResponse ur = new UsersResponse();
            string payload = JsonSerializer.Serialize(um);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var authResult = await client.PostAsync("/api/Registration", content);
            var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
            if (authResult.IsSuccessStatusCode == false)
            {
                resgiter = "Unsuccessfuly Register";
            }
            ur = JsonSerializer.Deserialize<UsersResponse>(
                    data,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (ur.statusCode == 0)
            {
                UserModel uj = new UserModel();

                resgiter = "Success";
            }
            else
            {

                resgiter = ur.statusMessage;
            }
        }
        return resgiter;
    }

    public async Task<string> ResendMail()
    {

        var userID = await _localStorage.GetItemAsync<string>("tempStoredValue");
        var userName = await _localStorage.GetItemAsync<string>("userName");
        var Email = await _localStorage.GetItemAsync<string>("Email");
        var client = _httpClientFactory.CreateClient("API");
        string resgiter = "Not Registered";
        Guid guid = Guid.NewGuid();

        var baseuri = _navigation.BaseUri;
        var baseurl = baseuri + "/activate/" + userID;
        var Message = "<br/><br/>Welcome to Advance Cyber Security.<br/>Your Activation Link is <br/>" + " <br/><br/><a href='" + baseurl + "'>Click Here</a> ";
        MailData md = new MailData(Email, "Welcome to fun olympic 2023", Message, "support@funolympic.com", userName, "support@funolympic.com", "Fun Olympic");
        string payload2 = JsonSerializer.Serialize(md);
        var content2 = new StringContent(payload2, Encoding.UTF8, "application/json");
        var authResult2 = await client.PostAsync("/api/Registration/sendmail", content2);

        if (authResult2.IsSuccessStatusCode != true)
        {

            resgiter = "Invalid Email Address or try again later";
        }
        else
        {
            resgiter = "Success";
        }
        return resgiter;
    }
    public async Task<string> Activate(string ID)
    {
        var client = _httpClientFactory.CreateClient("API");
        string resgiter = "Not Registered";
        Guid guid = Guid.NewGuid();
        var authResult = await client.PostAsync("/api/Registration/Activate/" + ID, null);

        if (authResult.IsSuccessStatusCode == false)
        {

            resgiter = "Invalid Email Address or try again later";
        }
        var data = await authResult.Content.ReadFromJsonAsync<JsonElement>();
        UsersResponse ur = new UsersResponse();
        ur = JsonSerializer.Deserialize<UsersResponse>(
                   data,
                   new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (ur.statusCode == 0)
        {
            if (ur.data is null)
            {
                resgiter = "Invalid Email Address or try again later";
                return resgiter;
            }
            UserModel uj = new UserModel();
            uj = ur.data;
            await _localStorage.RemoveItemAsync("userName");
            await _localStorage.RemoveItemAsync("Email");
            resgiter = $"Success Authorize {uj.FirstName} {uj.LastName} ({uj.Email})";
        }
        return resgiter;
    }
    public async Task<string> ForgetPassword(string Email)
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
        var authResult2 = await client.PostAsync("/api/Registration/sendmail", content2);

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

}

using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;

namespace FunOlympic_Web.Helper;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient;
    private readonly ISessionStorageService _localStorage;
    private readonly IConfiguration _config;
    private readonly AuthenticationState _anonymous = null;
    public CustomAuthenticationStateProvider(HttpClient httpClient, ISessionStorageService localStorage, IConfiguration config)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _config = config;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var Auth = _config.GetValue<string>("AuthStorageKey");
            var auth = await _localStorage.GetItemAsync<string>(Auth);
            var userName = await _localStorage.GetItemAsync<string>("userName");
            ClaimsIdentity identity = new ClaimsIdentity();
            if (string.IsNullOrWhiteSpace(auth))
            {

                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
            }
            identity = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, userName),
             new Claim("auth", auth),
              new Claim(ClaimTypes.Role, "Other"),
        }, "apiauth_type");

            var user = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(user));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return await Task.FromResult(new AuthenticationState(null));
        }
    }

    public void NotifyUserAuthentication(string Auth, string userName, bool emailConfirmed, string Roles)
    {
        var authenticatedUser = new ClaimsPrincipal(

            new ClaimsIdentity(new[]
            {
              new Claim(ClaimTypes.Name, userName),
              new Claim("auth", Auth),
               new Claim(ClaimTypes.Role, Roles)

            }, "apiauth_type")) ;
        var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
        NotifyAuthenticationStateChanged(authState);
    }
    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(_anonymous);
        NotifyAuthenticationStateChanged(authState);
    }
}

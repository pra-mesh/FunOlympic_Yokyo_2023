using Blazored.SessionStorage;
using Blazorise;
using Blazorise.AntDesign;
using Blazorise.Icons.FontAwesome;
using FunOlympic_Web;
using FunOlympic_Web.Helper;
using FunOlympic_Web.Services.Implementation;
using FunOlympic_Web.Services.Interface;
using GoogleCaptchaComponent;
using GoogleCaptchaComponent.Configuration;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var baseurl = builder.Configuration.GetValue<string>("baseURl");

builder.Services.AddGoogleCaptcha(configuration =>
{
    configuration.ServerSideValidationRequired = true;
    configuration.SiteKey = builder.Configuration.GetValue<string>("CaptchaSiteToken"); ;
    configuration.CaptchaVersion = CaptchaConfiguration.Version.V2;
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<CookieHandler>();
builder.Services.AddHttpClient("API", options =>
{
    options.BaseAddress = new Uri(baseurl);
}).AddHttpMessageHandler<CookieHandler>();
//builder.Services.AddHttpClient<IUserServices, UserServices>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ISignUpservices, SignUpservices>();
builder.Services.AddScoped<IUserManagerService, UserManagerService>();
AddBlazorise(builder.Services);

await builder.Build().RunAsync();


void AddBlazorise(IServiceCollection services)
{
    services
        .AddBlazorise();
    services
        .AddAntDesignProviders()
        .AddFontAwesomeIcons();

}

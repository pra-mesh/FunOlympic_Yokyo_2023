using FunOlympicDataManager.Authorization;
using FunOlympicDataManager.Helpers;
using FunOlympicDataManager.Library.DataAccess.Implementation;
using FunOlympicDataManager.Library.DataAccess.Interface;
using FunOlympicDataManager.Library.DataAccess.Internal;
using FunOlympicDataManager.Library.Models;
using FunOlympicDataManager.Library.ResponseModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FunOlympic DataManager", Version = "v1" });
});
builder.Services.AddCors();
builder.Services.Configure<ApiBehaviorOptions>(a =>
{
    a.InvalidModelStateResponseFactory = context =>
    {
        BadRequestResponse br = new BadRequestResponse();
        br.statusCode = 2;
        List<string> st = new List<string>();
        br.statusMessage = "Invalid Data";
        foreach (var keyModelStatePair in context.ModelState)
        {
            var key = keyModelStatePair.Key;
            var errors = keyModelStatePair.Value.Errors;
            if (errors != null && errors.Count > 0)
            {
                var errorMessage = string.IsNullOrEmpty(errors[0].ErrorMessage) ?
    "The input was not valid." :
errors[0].ErrorMessage;
                st.Add(errorMessage);
            }
        }
        br.data = st;
        return new BadRequestObjectResult(br);
    };
});
var key = "SuperSecretSaltforASSignment";
builder.Services.AddSingleton<IJwtUtils>(new JwtUtils(key));
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IUsermanagerData, UsermanagerData>();
builder.Services.AddTransient<IRegistrationData, RegistrationData>();
builder.Services.AddTransient<ILoginData, LoginData>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IScoresData, ScoresData>();
builder.Services.AddTransient<IGameData, GameData>();
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme
).AddCookie();
builder.Services.Configure<MailSettingModel>(builder.Configuration.GetSection(nameof(MailSettingModel)));
builder.Services.AddTransient<IMailService, MailService>();
builder.Services.AddTransient<IGameListData, GameListData>();
var app = builder.Build();

app.UseCors(cors => cors
.AllowAnyMethod()
.AllowAnyHeader()
.SetIsOriginAllowed(origin => true)
.AllowCredentials()
);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), @"Resources");
if (!Directory.Exists(pathToSave))
{
    // Try to create the directory.
    DirectoryInfo di = Directory.CreateDirectory(pathToSave);
}
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(pathToSave),
    RequestPath = new PathString("/StaticFiles")
});
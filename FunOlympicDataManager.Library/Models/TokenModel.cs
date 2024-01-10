using System.Text.Json.Serialization;

namespace FunOlympicDataManager.Library.Models;
public class TokenModel
{
    public string token { get; set; } = "";
    public string ID { get; set; }
    public string UserName { get; set; }

    [JsonIgnore] // refresh token is returned in http only cookie
    public string RefreshToken { get; set; } = "";

    public bool EmailConfirmed { get; set; } = false;
    public bool isDisabled { get; set; } = false;
    public string Roles { get; set; }
}

namespace FunOlympic_Web.Model;

public class LoggedInModel
{
    public string token { get; set; } = "";
    public string ID { get; set; } = "";
    public bool EmailConfirmed { get; set; } = false;
    public string UserName { get; set; } = "";
    public bool isDisabled { get; set; } = false;
    public string Roles { get; set; } = "Normal";
}



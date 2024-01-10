using System.ComponentModel.DataAnnotations;

namespace FunOlympic_Web.Model;

public class PasswordReset
{
    [Required]
    public string userID { get; set; }
    [Required]
    public string ResetCode { get; set; }
}


using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace FunOlympic_Web.Model;

public class LoginModel
{
    [DisplayName("Email")]
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Format"),]

    public string userName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string password { get; set; } = "";
}

using System.ComponentModel.DataAnnotations;

namespace FunOlympicDataManager.Library.Models;
public class UserInfoModel
{
    [Required, MaxLength(256, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
    public string username { get; set; } = "";
    [Required]
    public string Id { get; set; } = "";
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; } = "";

    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "{0} value must be number")]
    public string PhoneNumber { get; set; } = "";
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string Gender { get; set; } = "Male";
    public DateTime DOB { get; set; } = (DateTime.Now);

    public string Roles { get; set; } = "Normal";

    public bool isDisabled { get; set; } = false;
}
public class UserUpdateModel
{
    [Required]
    public string Id { get; set; } = "";
    public string Roles { get; set; } = "Normal";
    public bool isDisabled { get; set; } = false;
}
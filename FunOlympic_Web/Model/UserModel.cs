using FunOlympic_Web.Helper;
using System.ComponentModel.DataAnnotations;

namespace FunOlympic_Web.Model;
public class UserModel
{

    public string Id { get; set; } = "";
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; } = "";

    [DataType(DataType.PhoneNumber)]
    [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "{0} value must be Phone Number")]
    public string PhoneNumber { get; set; } = "";
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";
    [Required]
    [PasswordVerifer]
    // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$_!%*?&])[A-Za-z\d$@$_!%*?&]{8,}$" , 
    //ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")]
    [DataType(DataType.Password)]

    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
    public string Password { get; set; } = "";
    [Required]
    [Compare("Password",
        ErrorMessage = "Password and Confirm Password must match")]
    public string? ConfirmPassword { get; set; }

    [RegularExpression("Male|Female|Other", ErrorMessage = "{0} Invalid Value {1}.")]
    public string Gender { get; set; } = "Male";
    public DateTime DOB { get; set; } = DateTime.Now;


}

public class ResetViewModel
{
    [Required]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string Email { get; set; } = "";
    [Required]
    [PasswordVerifer]
    // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$_!%*?&])[A-Za-z\d$@$_!%*?&]{8,}$" , 
    //ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")]
    [DataType(DataType.Password)]

    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
    public string Password { get; set; } = "";
    [Required]
    [Compare("Password",
        ErrorMessage = "Password and Confirm Password must match")]
    public string? ConfirmPassword { get; set; }
    [Required]
    [MaxLength(6)]
    public string? ResetCode { get; set; }
}


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

    public string Roles { get; set; } = "Role";

    public bool isDisabled { get; set; } = false;

    public string FullName
    {
        get { return FirstName + " " + LastName; }
    }
}

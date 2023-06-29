using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FunOlympicDataManager.Library.Models;
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
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
    public string Password { get; set; } = "";
    public string Gender { get; set; } = "Other";

    public DateTime DOB { get; set; } = (DateTime.Now);


}

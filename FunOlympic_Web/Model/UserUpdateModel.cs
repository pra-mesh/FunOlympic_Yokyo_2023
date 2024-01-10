using System.ComponentModel.DataAnnotations;

public class UserUpdateModel
{
    [Required]
    public string Id { get; set; } = "";
    public string Roles { get; set; } = "Normal";
    public bool isDisabled { get; set; } = false;
}
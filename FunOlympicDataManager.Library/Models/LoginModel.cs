﻿using System.ComponentModel.DataAnnotations;

namespace FunOlympicDataManager.Library.Models;
public class LoginModel
{
    [Required, MaxLength(256, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
    public string userName { get; set; } = "";
    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
    public string password { get; set; } = "";
}

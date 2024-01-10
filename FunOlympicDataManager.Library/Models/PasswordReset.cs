using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunOlympicDataManager.Library.Models;
public class PasswordReset
{
    [Required]
    public string userID { get; set; }
    [Required]
    public string ResetCode { get; set; }
}

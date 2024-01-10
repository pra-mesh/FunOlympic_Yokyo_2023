
using System.ComponentModel.DataAnnotations;

namespace FunOlympic_Web.Helper;

public class PasswordVerifer : ValidationAttribute
{

    public PasswordVerifer()
        : base("{0} invalid password.")
    {

    }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    { //
        string st = "Password must";
        if (value == null)
            return new ValidationResult("Password must be atleast 8 character , digit and special charater and upperCase.");
        var textValue = value.ToString();

        if (textValue.Length < 8)
            st = st + " atleast 8 character";
        if (!textValue.Any(char.IsDigit))
        {
            st = Valided(st, " have at least one digit.");

        }
        if (!textValue.Any(char.IsLetterOrDigit))
        {
            st = Valided(st, " have at least one special Character.");

        }

        if (!textValue.Any(char.IsLower) || !textValue.Any(char.IsUpper))
        {
            st = Valided(st, "have at least one upper and lowercase.");
        }
        if (st != "Password must")
        {
            return new ValidationResult(st);
        }
        else
            return ValidationResult.Success;

    }

    private static string Valided(string st, string st2)
    {
        if (st == "Password must")
            st += st2;
        else
        {
            st.Replace(".", "");
            st.Replace(" and", ",");
            st += " and" + st2;
        }

        return st;
    }
}
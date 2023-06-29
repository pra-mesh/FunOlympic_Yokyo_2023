using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace FunOlympic_Web.Helper;

public class Utility
{
    public static string Encrypt(string clearText)
    {
        StringBuilder Sb = new StringBuilder();

        using (SHA256 hash = SHA256Managed.Create())
        {
            Encoding enc = Encoding.UTF8;
            Byte[] result = hash.ComputeHash(enc.GetBytes(clearText));

            foreach (Byte b in result)
                Sb.Append(b.ToString("x2"));
        }

        return Sb.ToString();
    }
}

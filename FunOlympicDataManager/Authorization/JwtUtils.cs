
using FunOlympicDataManager.Library.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FunOlympicDataManager.Authorization;
public interface IJwtUtils
{
    public string GenerateJwtToken(UserIDModel user);
    public string? ValidateJwtToken(string token);
    public RefreshTokenModel GenerateRefreshToken(string ipAddress);
}

public class JwtUtils : IJwtUtils
{
    private readonly string _key = "";


    public JwtUtils(string key)
    {
        _key = key;

    }

    public string GenerateJwtToken(UserIDModel user)
    {
        // generate token that is valid for 5 minutes
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim("id", user.UserID.ToString()),
                new Claim("userName", user.UserName.ToString()) }),

            Expires = DateTime.UtcNow.AddMinutes(60),
            NotBefore = DateTime.UtcNow,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
            , SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string? ValidateJwtToken(string token)
    {
        if (token == null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_key);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = (jwtToken.Claims.First(x => x.Type == "id").Value).ToString();

            // return user id from JWT token if validation successful
            return userId;
        }
        catch
        {
            // return null if validation fails
            return null;
        }
    }

    public RefreshTokenModel GenerateRefreshToken(string ipAddress)
    {
        var refreshToken = new RefreshTokenModel
        {
            Token = getUniqueToken(),
            // token is valid for 7 days
            Expires = DateTime.UtcNow.AddHours(3),
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };

        return refreshToken;

        string getUniqueToken()
        {
            // token is a cryptographically strong random sequence of values
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            //todo: ensure token is unique by checking against db
            // var tokenIsUnique = !_context.Users.Any(u => u.RefreshTokens.Any(t => t.Token == token));

            //if (!tokenIsUnique)
            //    return getUniqueToken();

            return token;
        }
    }
}
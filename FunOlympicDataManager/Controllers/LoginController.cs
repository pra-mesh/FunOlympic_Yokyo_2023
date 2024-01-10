using FunOlympicDataManager.Authorization;
using FunOlympicDataManager.Helpers;
using FunOlympicDataManager.Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace FunOlympicDataManager.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LoginController : ControllerBase
{
    private readonly IUserService _userService;

    public LoginController(IUserService userService)
    {
        _userService = userService;
    }
    [AllowAnonymous]
    [HttpPost("token")]
    public async Task<IActionResult> Login(LoginModel LoginModel)
    {
        var response = _userService.Authenticate(LoginModel, ipAddress());
        string dv = devices();
        var data = response.data;
        if (data != null)
        {
            setTokenCookie(data.token, data.RefreshToken);
            setReTokenCookie(data.token, data.RefreshToken);
        }
        return Ok(response);
    }
    private void setTokenCookie(string token, string refreshToken)
    {
        // append cookie with refresh token to the http response
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(1),
            Secure = true,
            SameSite = SameSiteMode.None

        };
        Response.Cookies.Append("Token", token, cookieOptions);


    }
    private void setReTokenCookie(string token, string refreshToken)
    {
        // append cookie with refresh token to the http response
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(1),
            Secure = true,
            SameSite = SameSiteMode.None

        };
        Response.Cookies.Append("RefreshToken", refreshToken, cookieOptions);


    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public IActionResult RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var response = _userService.RefreshToken(refreshToken, ipAddress());
        var data = response.data;
        if (data != null)
        {
            setTokenCookie(data.token, data.RefreshToken);
            setReTokenCookie(data.token, data.RefreshToken);

        }
        return Ok(response);
    }

    [HttpPost("revoke-token/{model}")]
    public IActionResult RevokeToken(string model)
    {
        // accept refresh token in request body or cookie
        var token = model ?? Request.Cookies["refreshToken"];

        if (string.IsNullOrEmpty(token))
            return BadRequest(new { message = "Refresh Token is required" });

        var sr = _userService.RevokeToken(token, ipAddress());
        foreach (var cookie in Request.Cookies.Keys)
        {
            Response.Cookies.Delete(cookie);
        }
        return Ok(sr);
    }
    [AllowAnonymous]
    [HttpPost("Logout")]
    public IActionResult Logout()
    {
        // accept refresh token in request body or cookie

        foreach (var cookie in Request.Cookies.Keys)
        {
            Response.Cookies.Delete(cookie);
        }
        return Ok("Logged out");
    }
    private string ipAddress()
    {
        // get source ip address for the current request
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"];
        else
            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }
    private string devices()
    {
        // get source ip address for the current request
        if (Request.Headers.ContainsKey("X-Drutt-Device-id"))
            return Request.Headers["X-Drutt-Device-id"];
        else
            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }

}

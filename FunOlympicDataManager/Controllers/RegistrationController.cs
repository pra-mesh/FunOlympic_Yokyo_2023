using FunOlympicDataManager.Authorization;
using FunOlympicDataManager.Library.DataAccess.Interface;
using FunOlympicDataManager.Library.Models;
using FunOlympicDataManager.Library.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FunOlympicDataManager.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationData _registration;
    private readonly IMailService _mail;

    public RegistrationController(IRegistrationData registration, IMailService mail)
    {
        _registration = registration;
        _mail = mail;
    }
    [AllowAnonymous]
    [HttpPost]
    public UsersResponse Registration(UserModel userModel) => _registration.Registration(userModel);
    
    [HttpGet("{id}")]
    public UsersResponse UserInfo(string id) => _registration.UserInfo(id);
    [AllowAnonymous]
    [HttpPost("Activate/{id}")]
    public UsersResponse Activate(string id) => _registration.Activate(id);

    [AllowAnonymous]
    [HttpPost("sendmail")]
    public async Task<IActionResult> SendMailAsync(MailData mailData)
    {
        bool result = await _mail.SendAsync(mailData, new CancellationToken());

        if (result)
        {
            return StatusCode(StatusCodes.Status200OK, "Mail has successfully been sent.");
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail could not be sent.");
        }
    }
}

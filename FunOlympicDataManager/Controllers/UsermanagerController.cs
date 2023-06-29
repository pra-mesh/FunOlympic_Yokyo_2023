using FunOlympicDataManager.Library.DataAccess.Interface;
using FunOlympicDataManager.Library.Models;
using FunOlympicDataManager.Library.ResponseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FunOlympicDataManager.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsermanagerController : ControllerBase
{
    private readonly IUsermanagerData _um;

    public UsermanagerController(IUsermanagerData um)
    {
        _um = um;
    }

    [HttpGet("UserInfo")]
    public UsersListResponse UserInfo() => _um.UserInfo();

    [HttpPost("UpdateUserStatus")]
    public BooleanResponse UpdateUserStatus(UserUpdateModel gm) => _um.UpdateUsers(gm);
}

namespace FunOlympicDataManager.Authorization;

using FunOlympicDataManager.Library.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        // authorization 
        var user = (UsersResponse)context.HttpContext.Items["User"];
        if (user == null)
            context.Result = new JsonResult(new { statusmessage = "Unauthorized", statuscode = 4 }) { StatusCode = StatusCodes.Status401Unauthorized };
    }
}
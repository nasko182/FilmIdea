namespace FilmIdea.Web.Controllers;

using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class BaseController : Controller
{
    public string GetUserName()
    {
        return User.FindFirst(ClaimTypes.Name).Value;
    }

    public string GetUserId()
    {
        return User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }

    public bool IsAuthenticated()
    {
        return User.Identity?.IsAuthenticated ?? false;
    }
}

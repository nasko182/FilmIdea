namespace FilmIdea.Web.Controllers;

using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class BaseController : Controller
{
    public string GetUserName()
    {
        return this.User.FindFirstValue(ClaimTypes.Name);
    }

    public string GetUserId()
    {
        return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public bool IsAuthenticated()
    {
        return this.User.Identity?.IsAuthenticated ?? false;
    }
}

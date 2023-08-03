namespace FilmIdea.Web.Controllers;

using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using static Common.NotificationMessageConstants;

[Authorize]
public class BaseController : Controller
{
    protected string GetUserName()
    {
        return this.User.FindFirstValue(ClaimTypes.Name);
    }

    protected string GetUserId()
    {
        return this.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    protected bool IsAuthenticated()
    {
        return this.User.Identity?.IsAuthenticated ?? false;
    }

    protected IActionResult GeneralError()
    {
        this.TempData[ErrorMessage] =
            "Unexpected error occurred! Please try again later or contact administrator";

        return this.RedirectToAction(this.TempData["LastAction"]!.ToString(), this.TempData["LastController"]!.ToString());
    }

    protected IActionResult UnexpectedDataError(string parameter)
    {
        TempData[ErrorMessage] = $"Invalid {parameter}. Please try again later";

        return this.RedirectToAction("Index", "Home");
    }
}

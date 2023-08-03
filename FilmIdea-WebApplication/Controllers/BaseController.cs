namespace FilmIdea.Web.Controllers;

using System.Security.Claims;
using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using static Common.NotificationMessageConstants;
using static Common.ExceptionMessages;

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
        this.TempData[ErrorMessage] = GeneralErrorMessage;

        return this.RedirectToAction(this.TempData["LastAction"]!.ToString(), this.TempData["LastController"]!.ToString());
    }

    protected IActionResult InvalidDataError(string input)
    {
        TempData[ErrorMessage] = InvalidInputErrorMessage(input);

        return this.RedirectToAction("Index", "Home");
    }
}

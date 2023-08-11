namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Infrastructure.Extensions;

public class HomeController : BaseController
{
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Index()
    {
        if (User.IsAdmin())
        {
            return this.RedirectToAction("Index", "Home", new { Area = "Admin" });
        }
        if (this.IsAuthenticated())
        {
            return this.RedirectToAction("Swipe","Swipe");
        }
        return this.View();
    }

    [AllowAnonymous]
    [HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int statusCode)
    {
        if (statusCode == 404 || statusCode == 400)
        {
            return this.View("Error404");
        }
        if (statusCode == 401)
        {
            return this.View("Error401");
        }
        return this.View();
    }
}
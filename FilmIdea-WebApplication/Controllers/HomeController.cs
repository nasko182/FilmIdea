namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class HomeController : BaseController
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        if (this.IsAuthenticated())
        {
            return this.RedirectToAction("Swipe","Swipe");
        }
        return this.View();
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int statusCode)
    {
        if (statusCode == 404 || statusCode == 400)
        {
            return this.View("Error404");
        }
        else if (statusCode == 401)
        {
            return this.View("Error404");
        }
        return this.View();
    }
}
namespace FilmIdea.Web.Controllers;

using System.Diagnostics;
using FilmIdea.Web.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class HomeController : BaseController
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        if (this.User?.Identity?.IsAuthenticated ?? false)
        {
            return this.RedirectToAction("All","Movie");
        }
        return this.View();
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
    }
}
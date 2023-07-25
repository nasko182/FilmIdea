namespace FilmIdea.Controllers;

using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Web.Controllers;
using Web.ViewModels.Home;

public class HomeController : BaseController
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        if (User?.Identity?.IsAuthenticated ?? false)
        {
            return RedirectToAction("All","Movie");
        }
        return View();
    }

    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class DirectorController : BaseController
{
    [AllowAnonymous]
    public async Task<IActionResult> Details()
    {
        return View();
    }
}

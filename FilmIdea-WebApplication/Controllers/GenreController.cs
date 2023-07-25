using Microsoft.AspNetCore.Mvc;

namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Authorization;

public class GenreController : BaseController
{
    [AllowAnonymous]
    public async Task<IActionResult> Details(int genreId)
    {
        ViewBag.GenreId = genreId;
        return View();
    }
}

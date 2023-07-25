using Microsoft.AspNetCore.Mvc;

namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Authorization;

public class MovieController : BaseController
{
    [AllowAnonymous]
    public async Task<IActionResult> All()
    {
        return View();
    }
}

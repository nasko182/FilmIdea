namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Mvc;

public class CriticController : BaseController
{
    public async Task<IActionResult> Become()
    {
        return View();
    }
}

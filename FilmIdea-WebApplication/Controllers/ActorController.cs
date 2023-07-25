namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class ActorController : BaseController
{
    [AllowAnonymous]
    public async Task<IActionResult> Details(int actorId)
    {
        return View();
    }
}

using Microsoft.AspNetCore.Mvc;

namespace FilmIdea.Web.Controllers;
public class SwipeController : BaseController
{
    public async Task<IActionResult> Swipe()
    {
        return View();
    }
}

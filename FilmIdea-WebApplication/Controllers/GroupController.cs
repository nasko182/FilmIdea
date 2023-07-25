using Microsoft.AspNetCore.Mvc;

namespace FilmIdea.Web.Controllers;
public class GroupController : BaseController
{
    public async Task<IActionResult> All()
    {
        return View();
    }
}

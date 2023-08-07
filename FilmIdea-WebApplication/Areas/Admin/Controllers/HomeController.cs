namespace FilmIdea.Web.Areas.Admin.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services.Data.Interfaces;

public class HomeController : BaseAdminController
{
    public HomeController(IDropboxService dropboxService)
    :base(dropboxService)
    {
        
    }
    public IActionResult Index()
    {
        return View();
    }
}

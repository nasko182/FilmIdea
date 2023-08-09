namespace FilmIdea.Web.Controllers;

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

public class VideoController : BaseController
{
    public IActionResult All(string serializedVideos)
    {
        var model = JsonSerializer.Deserialize<List<string>>(serializedVideos);

        return View(model);
    }
}



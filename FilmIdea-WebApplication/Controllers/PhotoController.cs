namespace FilmIdea.Web.Controllers;

using System.Text.Json;

using Microsoft.AspNetCore.Mvc;

public class PhotoController : BaseController
{
    public IActionResult All(string serializedPhotos)
    {
        var model = JsonSerializer.Deserialize<List<string>>(serializedPhotos);

        return View(model);
    }
}



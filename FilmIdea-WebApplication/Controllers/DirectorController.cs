namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using FilmIdea.Services.Data.Interfaces;

public class DirectorController : BaseController
{
    private readonly IDirectorService _directorService;

    public DirectorController(IDirectorService directorService)
    {
        this._directorService = directorService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int directorId, string information)
    {
        try
        {
            var director = await this._directorService.GetDirectorDetailsAsync(directorId, this.GetUserId());

            if (director == null)
            {
                return this.NotFound();
            }
            return View(director);
        }
        catch
        {
            return this.InvalidDataError("director id");
        }
    }
}

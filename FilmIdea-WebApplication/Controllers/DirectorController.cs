namespace FilmIdea.Web.Controllers;

using FilmIdea.Services.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class DirectorController : BaseController
{
    private readonly IDirectorService _directorService;

    public DirectorController(IDirectorService directorService)
    {
        this._directorService = directorService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int directorId)
    {
        try
        {
            var director = await this._directorService.GetDirectorDetailsAsync(directorId, this.GetUserId());

            return View(director);
        }
        catch
        {
            return this.UnexpectedDataError("director id");
        }
    }
}

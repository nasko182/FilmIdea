namespace FilmIdea.Web.Controllers;

using FilmIdea.Services.Data.Interfaces;
using Infrastructure.Extensions;
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
    public async Task<IActionResult> Details(int directorId, string information)
    {
        try
        {
            var director = await this._directorService.GetDirectorDetailsAsync(directorId, this.GetUserId());

            if (director == null || director.GetUrlInformation()!=information)
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

using Microsoft.AspNetCore.Mvc;

namespace FilmIdea.Web.Controllers;

using FilmIdea.Data.Models;
using FilmIdea.Services.Data;
using Services.Data.Interfaces;

public class SwipeController : BaseController
{
    private readonly ISwipeService _swipeService;

    public SwipeController(ISwipeService swipeService)
    {
        this._swipeService = swipeService;
    }
    public async Task<IActionResult> Swipe()
    {
        var movies = await this._swipeService.GetMoviesAsync(this.GetUserId());

        TempData["LastAction"] = ControllerContext.ActionDescriptor.ActionName;
        TempData["LastController"] = ControllerContext.ActionDescriptor.ControllerName;

        return View(movies);
    }

    public async Task<IActionResult> Reset()
    {
        await this._swipeService.ResetPassedList(this.GetUserId());

        return this.RedirectToAction("Swipe");
    }


    public async Task<IActionResult> PassMovie(int movieId)
    {
        await this._swipeService.AddMovieToUserPassedList(GetUserId(), movieId);

        return RedirectToAction(TempData["LastAction"]!.ToString(), TempData["LastController"]!.ToString());
    }
}

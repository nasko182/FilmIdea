namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Mvc;

using Services.Data.Interfaces;

public class SwipeController : BaseController
{
    private readonly ISwipeService _swipeService;

    public SwipeController(ISwipeService swipeService)
    {
        this._swipeService = swipeService;
    }

    [HttpGet]
    public async Task<IActionResult> Swipe()
    {
        try
        {
            var movies = await this._swipeService.GetMoviesAsync(this.GetUserId());

            TempData["LastAction"] = ControllerContext.ActionDescriptor.ActionName;
            TempData["LastController"] = ControllerContext.ActionDescriptor.ControllerName;

            return View(movies);
        }
        catch
        {
            return this.GeneralError();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Reset()
    {
        try
        {
            await this._swipeService.ResetPassedListAsync(this.GetUserId());

            return this.RedirectToAction("Swipe");
        }
        catch
        {
            return this.GeneralError();
        }
    }


    [HttpPost]
    public async Task<IActionResult> PassMovie(int movieId)
    {
        try
        {
            await this._swipeService.AddMovieToUserPassedListAsync(GetUserId(), movieId);

            return RedirectToAction(TempData["LastAction"]!.ToString(), TempData["LastController"]!.ToString());
        }
        catch
        {
            return this.GeneralError();
        }
    }
}

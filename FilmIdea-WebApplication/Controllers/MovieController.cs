namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Services.Data.Interfaces;

public class MovieController : BaseController
{
    private readonly IFilmIdeaService _filmIdeaService;

    public MovieController(IFilmIdeaService filmIdeaService)
    {
        this._filmIdeaService = filmIdeaService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> All()
    {
        var movies = await _filmIdeaService.GetAllMoviesAsync(GetUserId());

        return View(movies);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int movieId)
    {
        var movie = await _filmIdeaService.GetMovieAsync(movieId, GetUserId());

        if (movie == null)
        {
            return RedirectToAction("All");
        }

        return View(movie);
    }

    [HttpPost]
    public async Task<IActionResult> AddRating(int movieId, int ratingValue)
    {
        await _filmIdeaService.AddRatingAsync(movieId, ratingValue, GetUserId());

        return RedirectToAction("Details", new { id = movieId });
    }
}

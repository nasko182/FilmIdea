namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Services.Data.Interfaces;
using ViewModels.Movie;

public class MovieController : BaseController
{
    //TODO: Make Filter by genre in all,new,roulette and genre views

    private readonly IFilmIdeaService _filmIdeaService;

    public MovieController(IFilmIdeaService filmIdeaService)
    {
        this._filmIdeaService = filmIdeaService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> All()
    {
        AllMoviesViewModel movies;
        if (this.IsAuthenticated())
        {
            movies = await _filmIdeaService.GetAllMoviesAsync(this.GetUserId());
        }
        else
        {
            movies = await _filmIdeaService.GetAllMoviesAsync(null);
        }
        return View(movies);
    }

    [AllowAnonymous]
    public async Task<IActionResult> New()
    {
        var movies = await _filmIdeaService.GetNewMoviesAsync(GetUserId());

        return View(movies);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Top250()
    {
        var movies = await _filmIdeaService.GetTop250MoviesAsync(GetUserId());

        TempData["LastAction"] = ControllerContext.ActionDescriptor.ActionName;
        TempData["LastController"] = ControllerContext.ActionDescriptor.ControllerName;

        return View(movies);
    }

    [AllowAnonymous]
    public async Task<IActionResult> BrowseGenre()
    {
        var genres = await this._filmIdeaService.GetGenresAsync();

        return View(genres);
    }

    [AllowAnonymous]
    public async Task<IActionResult> ByGenre(int genreId,string genreName)
    {
        var movies = await _filmIdeaService.GetMoviesByGenreAsync(GetUserId(),genreId);

        TempData["LastAction"] = ControllerContext.ActionDescriptor.ActionName;
        TempData["LastController"] = ControllerContext.ActionDescriptor.ControllerName;

        ViewBag.GenreName = genreName;
        
        return View(movies);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Roulette()
    {
        MovieViewModel movie;
        if (this.IsAuthenticated())
        {
            movie = await _filmIdeaService.GetRouletteMovieAsync(GetUserId());
        }
        else
        {
            movie = await _filmIdeaService.GetRouletteMovieAsync(null);
        }

        TempData["LastAction"] = ControllerContext.ActionDescriptor.ActionName;
        TempData["LastController"] = ControllerContext.ActionDescriptor.ControllerName;

        return View(movie);
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

    [HttpPost]
    public async Task<IActionResult> AddToUserWatchlist(int movieId)
    {
        await _filmIdeaService.AddToUserWatchlist(GetUserId(),movieId);

        return RedirectToAction(TempData["LastAction"]!.ToString(), TempData["LastController"]!.ToString());
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromUserWatchlist(int movieId)
    {
        await _filmIdeaService.RemoveFromUserWatchlist(GetUserId(), movieId);

        return RedirectToAction(TempData["LastAction"].ToString(), TempData["LastController"].ToString());
    }

}

﻿namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Services.Data.Interfaces;
using ViewModels.Movie;

public class MovieController : BaseController
{
    //TODO: Make Filter by genre in all,new,roulette and genre views

    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        this._movieService = movieService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> All()
    {
        AllMoviesViewModel movies;
        if (this.IsAuthenticated())
        {
            movies = await this._movieService.GetAllMoviesAsync(this.GetUserId());
        }
        else
        {
            movies = await this._movieService.GetAllMoviesAsync(null);
        }

        TempData["LastAction"] = ControllerContext.ActionDescriptor.ActionName;
        TempData["LastController"] = ControllerContext.ActionDescriptor.ControllerName;

        return View(movies);
    }

    [AllowAnonymous]
    public async Task<IActionResult> New()
    {
        var movies = await this._movieService.GetNewMoviesAsync(GetUserId());

        TempData["LastAction"] = ControllerContext.ActionDescriptor.ActionName;
        TempData["LastController"] = ControllerContext.ActionDescriptor.ControllerName;

        return View(movies);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Top250()
    {
        var movies = await this._movieService.GetTop250MoviesAsync(GetUserId());

        TempData["LastAction"] = ControllerContext.ActionDescriptor.ActionName;
        TempData["LastController"] = ControllerContext.ActionDescriptor.ControllerName;

        return View(movies);
    }

    [AllowAnonymous]
    public async Task<IActionResult> BrowseGenre()
    {
        var genres = await this._movieService.GetGenresAsync();

        return this.View(genres);
    }

    [AllowAnonymous]
    public async Task<IActionResult> ByGenre(int genreId,string genreName)
    {
        var movies = await this._movieService.GetMoviesByGenreAsync(GetUserId(),genreId);

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
            movie = await this._movieService.GetRouletteMovieAsync(GetUserId());
        }
        else
        {
            movie = await this._movieService.GetRouletteMovieAsync(null);
        }

        TempData["LastAction"] = ControllerContext.ActionDescriptor.ActionName;
        TempData["LastController"] = ControllerContext.ActionDescriptor.ControllerName;

        return View(movie);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int movieId)
    {
        var movie = await this._movieService.GetMovieAsync(movieId, GetUserId());

        if (movie == null)
        {
            return RedirectToAction("All");
        }

        return View(movie);
    }

    public async Task<IActionResult> Watchlist()
    {
        var movies = await this._movieService.GetWatchlistMoviesAsync(GetUserId());

        TempData["LastAction"] = ControllerContext.ActionDescriptor.ActionName;
        TempData["LastController"] = ControllerContext.ActionDescriptor.ControllerName;

        return View(movies);
    }

    [HttpPost]
    public async Task<IActionResult> AddRating(int movieId, int ratingValue)
    {
        await this._movieService.AddRatingAsync(movieId, ratingValue, GetUserId());

        return RedirectToAction("Details", new { id = movieId });
    }

    [HttpPost]
    public async Task<IActionResult> AddToUserWatchlist(int movieId)
    {
        await this._movieService.AddToUserWatchlist(GetUserId(),movieId);

        return RedirectToAction(TempData["LastAction"]!.ToString(), TempData["LastController"]!.ToString());
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromUserWatchlist(int movieId)
    {
        await this._movieService.RemoveFromUserWatchlist(GetUserId(), movieId);

        return RedirectToAction(TempData["LastAction"]!.ToString(), TempData["LastController"]!.ToString());
    }

}
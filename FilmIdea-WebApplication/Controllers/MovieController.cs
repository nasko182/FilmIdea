namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Services.Data.Interfaces;
using ViewModels.Comment;
using ViewModels.Movie;
using ViewModels.Review;

using static Common.NotificationMessageConstants;
using FilmIdea.Services.Data.Models.Movies;

public class MovieController : BaseController
{
    //TODO: (Ask)Edit views to be more beautiful
    //TODO: (Ask)Add soft Delete(add is active to props, set isActive when delete, add where(prop=>prop.isActive) in all queries to get valid data)
    //TODO: Check site like user,critic and un logged
    //TODO: Hide buttons from users that don't need to see them
    //TODO: Add edit and delete for Reviews and Comments
    //TODO: Add success messages when add and edit also put them in class
    //TODO: Add exception messages class
    //TODO: Make Filter by genre in all,new,roulette and genre views(second workshop like 1h from begging)
    //TODO: Add pagination

    private readonly IMovieService _movieService;

    private readonly ICriticService _criticService;

    public MovieController(IMovieService movieService, ICriticService criticService)
    {
        this._movieService = movieService;
        this._criticService = criticService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> All([FromQuery]MoviesQueryModel queryModel)
    {
        MoviesFilteredAndPagesServiceModel serviceModel
            = await this._movieService.AllAsync(queryModel,this.GetUserId());

        queryModel.Movies = serviceModel.Movies;
        queryModel.TotalMovies = serviceModel.TotalMoviesCount;
        queryModel.Genres = await this._movieService.GetAllGenresAsync();
        queryModel.TopSectionMovies = await this._movieService.GetMoviesForTopSectionAsync();

        return View(queryModel);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> New()
    {
        try
        {
            var movies = await this._movieService.GetNewMoviesAsync(this.GetUserId());

            this.TempData["LastAction"] = this.ControllerContext.ActionDescriptor.ActionName;
            this.TempData["LastController"] = this.ControllerContext.ActionDescriptor.ControllerName;

            return this.View(movies);
        }
        catch
        {
            return this.GeneralError();
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Top250()
    {
        try
        {
            var movies = await this._movieService.GetTop250MoviesAsync(this.GetUserId());

            this.TempData["LastAction"] = this.ControllerContext.ActionDescriptor.ActionName;
            this.TempData["LastController"] = this.ControllerContext.ActionDescriptor.ControllerName;

            return this.View(movies);
        }
        catch
        {
            return this.GeneralError();
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> BrowseGenre()
    {
        var genres = await this._movieService.GetAllGenresAsync();

        return this.View(genres);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> ByGenre(int genreId)
    {
        bool isValid = await this._movieService.IsGenreIdValid(genreId);

        if (!isValid)
        {
            try
            {
                var movies = await this._movieService.GetMoviesByGenreAsync(this.GetUserId(), genreId);

                this.TempData["LastAction"] = this.ControllerContext.ActionDescriptor.ActionName;
                this.TempData["LastController"] = this.ControllerContext.ActionDescriptor.ControllerName;

                var genreName = await this._movieService.GetGenreNameByIdAsync(genreId);

                this.ViewBag.GenreName = genreName!;

                return this.View(movies);
            }
            catch
            {
                this.GeneralError();
            }
        }

        return this.UnexpectedDataError("genre id");
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Roulette()
    {
        try
        {
            MovieViewModel movie;
            if (this.IsAuthenticated())
            {
                movie = await this._movieService.GetRouletteMovieAsync(this.GetUserId());
            }
            else
            {
                movie = await this._movieService.GetRouletteMovieAsync(null);
            }

            this.TempData["LastAction"] = this.ControllerContext.ActionDescriptor.ActionName;
            this.TempData["LastController"] = this.ControllerContext.ActionDescriptor.ControllerName;

            return this.View(movie);
        }
        catch
        {
            return this.GeneralError();
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Details(int movieId)
    {
        MovieDetailsViewModel? movie;
        try
        {
            movie = await this._movieService.GetMovieAsync(movieId, this.GetUserId());
        }
        catch
        {
            TempData[ErrorMessage] = "Invalid movie id. Please try again later";

            return this.RedirectToAction("Index", "Home");
        }

        if (movie == null)
        {
            return this.RedirectToAction("All");
        }

        var isCriticExist = await this._criticService.CriticExistByUserIdAsync(this.GetUserId());

        if (isCriticExist)
        {
            this.ViewBag.CriticId = await this._criticService.GetCriticIdAsync(this.GetUserId());
        }

        var model = new MovieAndReviewViewModel()
        {
            AddReview = new AddReviewViewModel(),
            AddComment = new AddCommentViewModel(),
            MovieDetails = movie
        };

        return this.View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Watchlist()
    {
        try
        {
            var movies = await this._movieService.GetWatchlistMoviesAsync(this.GetUserId());


            this.TempData["LastAction"] = this.ControllerContext.ActionDescriptor.ActionName;
            this.TempData["LastController"] = this.ControllerContext.ActionDescriptor.ControllerName;

            return this.View(movies);
        }
        catch
        {
            return this.GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddReview(AddReviewViewModel model, int movieId)
    {
        bool isCritic =
            await this._criticService.CriticExistByUserIdAsync(this.GetUserId());

        if (this.ModelState.IsValid)
        {
            if (!isCritic)
            {
                this.TempData[ErrorMessage] = "You must become an critic in order to add new review";

                return this.RedirectToAction("Become", "Critic");
            }

            try
            {
                var criticId = await this._criticService.GetCriticIdAsync(this.GetUserId());
                await this._movieService.AddReviewAsync(model, movieId, criticId);
            }
            catch
            {
                this.TempData[ErrorMessage] = "Something went wrong. Please try again later.";
            }
        }
        return this.RedirectToAction("Details", "Movie", new { movieId });
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(AddCommentViewModel model, string reviewId, int movieId)
    {

        if (this.ModelState.IsValid)
        {
            try
            {
                await this._movieService.AddCommentAsync(model, reviewId, this.GetUserId());
            }
            catch
            {
                this.TempData[ErrorMessage] = "Invalid review";
            }

        }
        return this.RedirectToAction("Details", "Movie", new { movieId });
    }

    [HttpPost]
    public async Task<IActionResult> AddRating(int movieId, int ratingValue)
    {
        try
        {
            await this._movieService.AddRatingAsync(movieId, ratingValue, this.GetUserId());

            return this.RedirectToAction("Details", new { id = movieId });
        }
        catch
        {
            return this.GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddToUserWatchlist(int movieId)
    {
        try
        {
            await this._movieService.AddToUserWatchlist(this.GetUserId(), movieId);

            return this.RedirectToAction(this.TempData["LastAction"]!.ToString(),
                this.TempData["LastController"]!.ToString());
        }
        catch
        {
            return this.GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromUserWatchlist(int movieId)
    {
        try
        {
            await this._movieService.RemoveFromUserWatchlist(this.GetUserId(), movieId);

            return this.RedirectToAction(this.TempData["LastAction"]!.ToString(),
                this.TempData["LastController"]!.ToString());
        }
        catch
        {
            return this.GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddRemoveLike(string reviewId, int movieId)
    {
        try
        {
            await this._movieService.AddRemoveLike(reviewId, this.GetUserId());

            return this.RedirectToAction("Details", "Movie", new { movieId });
        }
        catch
        {
            return this.GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddRemoveDislike(string reviewId, int movieId)
    {
        try
        {
            await this._movieService.AddRemoveDislike(reviewId, this.GetUserId());

            return this.RedirectToAction("Details", "Movie", new { movieId });
        }
        catch
        {
            return this.GeneralError();
        }
    }
}

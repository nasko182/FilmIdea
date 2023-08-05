namespace FilmIdea.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Services.Data.Models.Movies;
using Services.Data.Interfaces;
using ViewModels.Comment;
using ViewModels.Movie;
using ViewModels.Review;


using static Common.NotificationMessageConstants;
using static Common.ExceptionMessages;
using static Common.SuccessMessages;

public class MovieController : BaseController
{
    //TODO: Everywhere where Critic is Write something use criticName in blue,  Everywhere where user write something use UserName in green, to be clear that this is critic
    //TODO: Security from(CSRF _|_,Parameter-tampering using guid, )
    //TODO: Add security from parameter tampering for int-s(Security (1:10m), and XXS
    //TODO: Implement receiving messages with SignalR
    //TODO: Fix bug with likes and dislikes Web API
    //TODO: Fix bug with reload page in details Deleting Edit Like don't reload properly 
    //TODO: Check for buttons to hide from users that don't need to see them
    //TODO: Check all views and js for more todo
    //TODO: Check all using
    //TODO: Check site like user,critic and un logged
    //TODO: Change All forms to asp to add validations(critic,new group.....)
    //TODO: Make manage button to be size of the username
    //TODO: Edit views to be more beautiful
    //TODO: Edit Swipe View Add Link to details on movie pic in swipe
    //TODO: Remove user from validation constants also using in applicationUser

    private readonly IMovieService _movieService;

    private readonly ICriticService _criticService;

    public MovieController(IMovieService movieService, ICriticService criticService)
    {
        this._movieService = movieService;
        this._criticService = criticService;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> All([FromQuery] MoviesQueryModel queryModel)
    {
        MoviesFilteredAndPagesServiceModel serviceModel
            = await this._movieService.AllAsync(queryModel, this.GetUserId());

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
        bool isValid = await this._movieService.IsGenreIdValidAsync(genreId);

        if (isValid)
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

        return this.InvalidDataError("genre id");
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
            movie = await this._movieService.GetMovieDetailsAsync(movieId, this.GetUserId());
        }
        catch
        {
            TempData[ErrorMessage] = InvalidInputErrorMessage("movie id");

            return this.RedirectToAction("Index", "Home");
        }

        if (movie == null)
        {
            return this.RedirectToAction("All");
        }

        var isCriticExist = await this._criticService.CriticExistByUserIdAsync(this.GetUserId());

        var model = new MovieAndReviewViewModel()
        {
            AddReview = new AddReviewViewModel(),
            AddComment = new AddCommentViewModel(),
            MovieDetails = movie
        };

        this.TempData["LastAction"] = this.ControllerContext.ActionDescriptor.ActionName;
        this.TempData["LastController"] = this.ControllerContext.ActionDescriptor.ControllerName;

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
                this.TempData[ErrorMessage] = UnauthorizedAccessErrorMessage;

                return this.RedirectToAction("Become", "Critic");
            }

            try
            {
                var criticId = await this._criticService.GetCriticIdAsync(this.GetUserId());
                await this._movieService.AddReviewAsync(model, movieId, criticId!);
            }
            catch
            {
                this.TempData[ErrorMessage] = GeneralErrorMessage;
            }
        }

        return this.RedirectToAction("Details", "Movie", new { movieId });
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(AddCommentViewModel model, int movieId)
    {

        if (this.ModelState.IsValid)
        {
            try
            {
                await this._movieService.AddCommentAsync(model, this.GetUserId());
            }
            catch
            {
                this.TempData[ErrorMessage] = InvalidInputErrorMessage("review id");
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
            await this._movieService.AddToUserWatchlistAsync(this.GetUserId(), movieId);

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
            await this._movieService.RemoveFromUserWatchlistAsync(this.GetUserId(), movieId);

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
            await this._movieService.AddRemoveLikeAsync(reviewId, this.GetUserId());

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
            await this._movieService.AddRemoveDislikeAsync(reviewId, this.GetUserId());

            return this.RedirectToAction("Details", "Movie", new { movieId });
        }
        catch
        {
            return this.GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditReview(EditReviewViewModel model, int movieId)
    {
        try
        {
            var criticId = await this._criticService.GetCriticIdAsync(this.GetUserId());
            if (criticId != null)
            {
                await this._movieService.EditReviewAsync(model, criticId);
            }
        }
        catch
        {
            return this.InvalidDataError("review ot movie id");
        }

        TempData[SuccessMessage] = EditReviewSuccess;
        return this.RedirectToAction("Details", "Movie", new { movieId });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteReview(string reviewId)
    {
        try
        {
            var criticId = await this._criticService.GetCriticIdAsync(this.GetUserId());
            if (criticId != null)
            {
                await this._movieService.DeleteReviewAsync(reviewId, criticId);
            }

            TempData[SuccessMessage] = DeleteReviewSuccess;

            return this.RedirectToAction(this.TempData["LastAction"]!.ToString(),
                this.TempData["LastController"]!.ToString());
        }
        catch (Exception e)
        {
            return InvalidDataError(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditComment(EditCommentViewModel model, int movieId)
    {
        try
        {
            await this._movieService.EditCommentAsync(model, this.GetUserId());
        }
        catch
        {
            return this.InvalidDataError("user or review id");
        }

        TempData[SuccessMessage] = EditCommentSuccess;
        return this.RedirectToAction("Details", "Movie", new { movieId });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteComment(string commentId)
    {
        try
        {
            await this._movieService.DeleteCommentAsync(commentId, this.GetUserId());

            TempData[SuccessMessage] = DeleteCommentSuccess;

            return this.RedirectToAction(this.TempData["LastAction"]!.ToString(),
                this.TempData["LastController"]!.ToString());
        }
        catch (Exception e)
        {
            return InvalidDataError(e.Message);
        }
    }
}

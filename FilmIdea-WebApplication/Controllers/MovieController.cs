namespace FilmIdea.Web.Controllers;

using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Infrastructure.Extensions;
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
    //TODO: Add not authenticated where redirect to login ??
    //TODO: View component
    //TODO: Check all using
    //TODO: Check all collections if empty (message?)
    //TODO: Check site like user,critic,admin and un logged if btn don't work add asp-area=""
    //TODO: Check if admin work when get project from github with new db
    //TODO: Check IServices for unused methods(IMovieService last)
    //TODO: Check all views and js for more todo
    //TODO: Check for buttons to hide from users that don't need to see them
    //TODO: Add [HttpGet] on every action
    //TODO: Remove all un used classes and methods Like databaseSeeder
    //TODO: Move all js in files

    //TODO: Add Update Picture on all EDIT pages
    //TODO: Add cache is need somewhere(adding something, displayCollections), also remove cache is update(last(1:30))
    //TODO: Edit movie detail footer
    //TODO: Edit views to be more beautiful
    //TODO: Everywhere where Critic is Write something use criticName in blue,  Everywhere where user write something use UserName in green, to be clear that this is critic
    //TODO: Edit Swipe View and Add Link to details on movie pic in swipe
    //TODO: Set cultureInfo.Invariant to every date
    //TODO: Fix bug with reload page in details Deleting Edit Like don't reload properly 
    //TODO: Add button for becoming admin in user all
    //TODO: In Admin add logic for adding photos and videos to movies actors and directors
    //TODO: In Admin add logic for adding actors and genres to movies 
    //TODO: Add buttons for adding actors and Genres to movie

    //TODO: Implement receiving messages with SignalR

    private readonly IMovieService _movieService;
    private readonly ICriticService _criticService;

    private readonly IHtmlSanitizer _sanitizer;

    public MovieController(IMovieService movieService, ICriticService criticService)
    {
        this._movieService = movieService;
        this._criticService = criticService;
        this._sanitizer = new HtmlSanitizer();
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
            return this.NotFound();
        }

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
        if (!this.IsAuthenticated())
        {
            this.TempData[ErrorMessage] = NotAuthenticated;

            return this.RedirectToAction("Login", "User");

        }
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
        if (!this.IsAuthenticated())
        {
            this.TempData[ErrorMessage] = NotAuthenticated;

            return this.RedirectToAction("Login", "User");

        }
        bool isCritic =
            await this._criticService.CriticExistByUserIdAsync(this.GetUserId());

        var sanitizedModel = new AddReviewViewModel
        {
            Title = this._sanitizer.Sanitize(model.Title),
            Rating = 0,
            Content = this._sanitizer.Sanitize(model.Content)
        };
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
                await this._movieService.AddReviewAsync(sanitizedModel, movieId, criticId!);
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
        var sanitizedModel = new AddCommentViewModel
        {
            ReviewId = this._sanitizer.Sanitize(model.ReviewId),
            Content = this._sanitizer.Sanitize(model.Content)
        };
        if (this.ModelState.IsValid)
        {
            if (!this.IsAuthenticated())
            {
                this.TempData[ErrorMessage] = NotAuthenticated;

                return this.RedirectToAction("Login", "User");

            }
            try
            {
                await this._movieService.AddCommentAsync(sanitizedModel, this.GetUserId());
            }
            catch
            {
                this.TempData[ErrorMessage] = InvalidInputErrorMessage("review id");
            }

        }

        return this.RedirectToAction("Details", "Movie", new { movieId });
    }

    [HttpPost]
    [Route("/Movie/AddRating")]
    public async Task<IActionResult> AddRating(int movieId, int ratingValue)
    {
        if (!this.IsAuthenticated())
        {
            this.TempData[ErrorMessage] = NotAuthenticated;

            return this.RedirectToAction("Login", "User");

        }
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
    [Route("/Movie/AddToUserWatchlist")]
    public async Task<IActionResult> AddToUserWatchlist(int movieId)
    {
        if (!this.IsAuthenticated())
        {
            this.TempData[ErrorMessage] = NotAuthenticated;

            return this.RedirectToAction("Login", "User");

        }
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
    [Route("/Movie/RemoveFromUserWatchlist")]

    public async Task<IActionResult> RemoveFromUserWatchlist(int movieId)
    {
        if (!this.IsAuthenticated())
        {
            this.TempData[ErrorMessage] = NotAuthenticated;

            return this.RedirectToAction("Login", "User");

        }
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
    public async Task<IActionResult> AddRemoveLike(string reviewId)
    {
        if (!this.IsAuthenticated())
        {
            this.TempData[ErrorMessage] = NotAuthenticated;

            return this.RedirectToAction("Login", "User");

        }
        try
        {
            var likes = await this._movieService.AddRemoveLikeAsync(reviewId, this.GetUserId());

            return Json(new { success = true, likes });
        }
        catch
        {
            return this.GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddRemoveDislike(string reviewId)
    {
        if (!this.IsAuthenticated())
        {
            this.TempData[ErrorMessage] = NotAuthenticated;

            return this.RedirectToAction("Login", "User");

        }
        try
        {
            var dislikes = await this._movieService.AddRemoveDislikeAsync(reviewId, this.GetUserId());

            return Json(new { success = true, likes = dislikes });
        }
        catch
        {
            return this.GeneralError();
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditReview(EditReviewViewModel model, int movieId)
    {
        if (!this.IsAuthenticated())
        {
            this.TempData[ErrorMessage] = NotAuthenticated;

            return this.RedirectToAction("Login", "User");

        }
        var sanitizedModel = new EditReviewViewModel
        {
            ReviewId = this._sanitizer.Sanitize(model.ReviewId),
            Title = this._sanitizer.Sanitize(model.Title),
            Rating = 0,
            Content = this._sanitizer.Sanitize(model.Content),
        };
        try
        {
            var criticId = await this._criticService.GetCriticIdAsync(this.GetUserId());
            if (criticId != null)
            {
                await this._movieService.EditReviewAsync(sanitizedModel, criticId);
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
        if (!this.IsAuthenticated())
        {
            this.TempData[ErrorMessage] = NotAuthenticated;

            return this.RedirectToAction("Login", "User");

        }
        try
        {
            var criticId = await this._criticService.GetCriticIdAsync(this.GetUserId());
            var isOwner = await this._movieService.IsCriticOwnerOfReview(criticId, reviewId);
            if (isOwner || this.User.IsAdmin())
            {
                await this._movieService.DeleteReviewAsync(reviewId);
            }

        }
        catch
        {
            return this.GeneralError();
        }
        TempData[SuccessMessage] = DeleteReviewSuccess;

        return this.RedirectToAction(this.TempData["LastAction"]!.ToString(),
            this.TempData["LastController"]!.ToString());
    }

    [HttpPost]
    public async Task<IActionResult> EditComment(EditCommentViewModel model, int movieId)
    {
        var sanitizedModel = new EditCommentViewModel
        {
            CommentId = this._sanitizer.Sanitize(model.CommentId),
            MovieId = 0,
            Content = this._sanitizer.Sanitize(model.Content),
        };
        try
        {
            await this._movieService.EditCommentAsync(sanitizedModel, this.GetUserId());
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
        if (!this.IsAuthenticated())
        {
            this.TempData[ErrorMessage] = NotAuthenticated;

            return this.RedirectToAction("Login", "User");

        }
        try
        {
            var userId = this.User.GetId();
            var isOwner = await  this._movieService.IsUserOwnerOfComment(userId!, commentId);

            if (isOwner || this.User.IsAdmin())
            {
                await this._movieService.DeleteCommentAsync(commentId); 
            }

            TempData[SuccessMessage] = DeleteCommentSuccess;

            return this.RedirectToAction(this.TempData["LastAction"]!.ToString(),
                this.TempData["LastController"]!.ToString());
        }
        catch
        {
            return this.GeneralError();
        }
    }
}

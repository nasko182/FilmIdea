namespace FilmIdea.Services.Tests;

using Microsoft.EntityFrameworkCore;

using Data;
using FilmIdea.Data;
using Data.Interfaces;
using Web.ViewModels.Comment;
using Web.ViewModels.Review;

[TestFixture]
public class MovieServiceTest
{
    private DbContextOptions<FilmIdeaDbContext> _dbOptions;
    private FilmIdeaDbContext _dbContext;

    private IMovieService _movieService;

    [SetUp]
    public void SetUp()
    {
        this._dbOptions = new DbContextOptionsBuilder<FilmIdeaDbContext>()
            .UseInMemoryDatabase("FilmIdeaInMemory" + Guid.NewGuid())
            .Options;

        this._dbContext = new FilmIdeaDbContext(this._dbOptions);

        this._dbContext.Database.EnsureCreated();

        this._movieService = new MovieService(this._dbContext);
    }

    [TearDown]
    public void TearDown()
    {
        this._dbContext.Database.EnsureDeleted();
    }

    [Test]
    public async Task IsGenreIdValidAsyncShouldReturnTrueIfIdIsValid()
    {
        var result = await this._movieService.IsGenreIdValidAsync(2);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task IsGenreIdValidAsyncShouldReturnFalseIfIdIsNotValid()
    {
        var result = await this._movieService.IsGenreIdValidAsync(200);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task GetGenreNameByIdAsyncShouldReturnValidGenreNameIfIdIsValid()
    {
        var result = await this._movieService.GetGenreNameByIdAsync(2);

        var expected = "Adventure";

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetGenreNameByIdAsyncShouldReturnNullIfIdIsNotValid()
    {
        var result = await this._movieService.GetGenreNameByIdAsync(200);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetNewMoviesAsyncShouldReturnCorrectMoviesIfUserIdIsValid()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";

        var result = await this._movieService.GetNewMoviesAsync(userId);

        var expected = "The Flash";

        Assert.That(result.Movies.First().Title, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetNewMoviesAsyncShouldReturnCorrectMoviesIfUserIdIsValidWithUserRating()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";

        var result = await this._movieService.GetNewMoviesAsync(userId);

        var expected = 0;

        Assert.That(result.Movies.First().UserRating, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetNewMoviesAsyncShouldReturnCorrectMoviesIfUserIdIsInValid()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333263";

        var result = await this._movieService.GetNewMoviesAsync(userId);

        var expected = "The Flash";

        Assert.That(result.Movies.First().Title, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetNewMoviesAsyncShouldReturnCorrectMoviesIfUserIdIsInValidWithOutUserRating()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333263";

        var result = await this._movieService.GetNewMoviesAsync(userId);

        var expected = 0;

        Assert.That(result.Movies.First().UserRating, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetNewMoviesAsyncShouldNotReturnUserRatingIfUserIdIsNull()
    {

        var result = await this._movieService.GetNewMoviesAsync(null);


        Assert.That(result.Movies.First().UserRating, Is.Null);
    }

    [Test]
    public async Task GetMoviesByGenreAsyncShouldReturnCorrectMoviesIfUserIdIsValid()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";

        var genreId = 2;

        var result = await this._movieService.GetMoviesByGenreAsync(userId, genreId);

        var expected = "Indiana Jones and the Dial of Destiny";

        Assert.That(result.Movies.First().Title, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetMoviesByGenreAsyncShouldReturnCorrectMoviesIfUserIdIsValidWithUserRating()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";

        var genreId = 2;

        var result = await this._movieService.GetMoviesByGenreAsync(userId, genreId);

        var expected = 0;

        Assert.That(result.Movies.First().UserRating, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetMoviesByGenreAsyncShouldReturnCorrectMoviesIfUserIdIsInValid()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333263";

        var genreId = 1;

        var result = await this._movieService.GetMoviesByGenreAsync(userId, genreId);

        var expected = "The Flash";

        Assert.That(result.Movies.First().Title, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetMoviesByGenreAsyncShouldReturnCorrectMoviesIfUserIdIsInValidWithOutUserRating()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333263";

        var genreId = 2;

        var result = await this._movieService.GetMoviesByGenreAsync(userId, genreId);

        var expected = 0;

        Assert.That(result.Movies.First().UserRating, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetMoviesByGenreAsyncShouldReturnCorrectMoviesIfUserIdIsNull()
    {
        var genreId = 1;

        var result = await this._movieService.GetMoviesByGenreAsync(null, genreId);

        var expected = "The Flash";

        Assert.That(result.Movies.First().Title, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetMoviesByGenreAsyncShouldNotReturnUserRatingIfUserIdIsNull()
    {
        var genreId = 2;

        var result = await this._movieService.GetMoviesByGenreAsync(null, genreId);


        Assert.That(result.Movies.First().UserRating, Is.Null);
    }

    [Test]
    public async Task GetMovieDetailsAsyncShouldReturnCorrectMovie()
    {
        var movieId = 1;

        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";

        var result = await this._movieService.GetMovieDetailsAsync(movieId, userId);

        var expected = "The Flash";

        Assert.That(result!.Title, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetMovieDetailsAsyncShouldReturnNullIfMovieIdIsInvalid()
    {
        var movieId = 0;

        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";

        var result = await this._movieService.GetMovieDetailsAsync(movieId, userId);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetRouletteMovieAsyncShouldReturnMovieWithUserRatingIfUserIdIsValid()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";

        var resultMovie = await this._movieService.GetRouletteMovieAsync(userId);

        Assert.That(resultMovie.UserRating, Is.EqualTo(0));
    }

    [Test]
    public async Task GetRouletteMovieAsyncShouldReturnMovieWithNullUserRatingIfUserIdIsNull()
    {
        var resultMovie = await this._movieService.GetRouletteMovieAsync(null);

        Assert.That(resultMovie.UserRating, Is.Null);
    }

    [Test]
    public async Task GetAllGenresAsyncShouldReturnListWithGenres()
    {
        var result = await this._movieService.GetAllGenresAsync();

        var expected = 1;

        Assert.That(result.First().Id, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetAllGenresAsyncShouldReturnListWithAllGenres()
    {
        var result = await this._movieService.GetAllGenresAsync();

        var expected = 23;

        Assert.That(result.Count, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetWatchlistMoviesAsyncShouldReturnListWithUserMovies()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";

        var movieId = 2;

        await this._movieService.AddToUserWatchlistAsync(userId, movieId);

        var result = await this._movieService.GetWatchlistMoviesAsync(userId);

        Assert.That(result.Any(m => m.Id == movieId), Is.True);
    }

    [Test]
    public async Task GetWatchlistMoviesAsyncShouldReturnListWithAllUserMovies()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";

        var movieId = 2;

        await this._movieService.AddToUserWatchlistAsync(userId, movieId);

        var result = await this._movieService.GetWatchlistMoviesAsync(userId);

        var expected = 1;

        Assert.That(result.Count, Is.EqualTo(expected));
    }

    [Test]
    public async Task AddRatingAsyncShouldAddRatingToMovie()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";

        var movieId = 2;

        var rating = 10;

        await this._movieService.AddRatingAsync(movieId, rating, userId);

        var movie = await this._movieService.GetMovieDetailsAsync(movieId, userId);

        Assert.That(movie!.Rating, Is.EqualTo(rating));
    }

    [Test]
    public async Task AddReviewAsyncShouldAddReviewToMovieIfMovieExist()
    {
        var criticId = "bf595423-7323-4e40-bd43-0f876c1beece";

        var userId = "15EB7825-840B-4528-71CC-08DB8C333233";

        var movieId = 2;

        var review = new AddReviewViewModel()
        {
            Title = "Title",
            Rating = 10,
            Content = "Content",
        };

        await this._movieService.AddReviewAsync(review, movieId, criticId);

        var movie = await this._movieService.GetMovieDetailsAsync(movieId, userId);

        var expected = "Title";

        Assert.That(movie!.Reviews.First().Title, Is.EqualTo(expected));
    }

    [Test]
    public async Task EditReviewAsyncShouldEditReviewToMovieIfMovieExist()
    {
        var criticId = "bf595423-7323-4e40-bd43-0f876c1beece";

        var userId = "15EB7825-840B-4528-71CC-08DB8C333233";

        var movieId = 2;

        var model = new EditReviewViewModel()
        {
            ReviewId = "7DCC5BD6-1133-432B-B6A6-F6C27DA75948".ToLower(),
            Title = "Title2",
            Rating = 10,
            Content = "Content",
        };

        await this._movieService.EditReviewAsync(model, criticId);

        var movie = await this._movieService.GetMovieDetailsAsync(movieId, userId);

        var expected = "Title2";

        Assert.That(movie!.Reviews.First().Title, Is.EqualTo(expected));
    }

    [Test]
    public async Task AddCommentAsyncShouldAddCommentToReview()
    {
        var userId = "15EB7825-840B-4528-71CC-08DB8C333233";

        var movieId = 2;

        var model = new AddCommentViewModel()
        {
            ReviewId = "7DCC5BD6-1133-432B-B6A6-F6C27DA75948".ToLower(),
            Content = "Content",
        };

        await this._movieService.AddCommentAsync(model, userId);

        var movie = await this._movieService.GetMovieDetailsAsync(movieId, userId);

        var expected = "Content";

        Assert.That(movie!.Reviews.First().Comments.First().Content, Is.EqualTo(expected));
    }

    [Test]
    public async Task EditCommentAsyncShouldEditComment()
    {
        var userId = "2532ddaa-63f0-4de8-71cb-08db8c333233";

        var movieId = 2;

        var commentId = this._dbContext.Movies
            .Where(m => m.Id == movieId)
            .Include(m => m.Reviews)
            .ThenInclude(r => r.Comments)
            .Select(m => m.Reviews.First().Comments.First().Id)
            .First()
            .ToString();

        var model = new EditCommentViewModel()
        {
            Content = "Content2",
            CommentId = commentId,
            MovieId = movieId
        };

        await this._movieService.EditCommentAsync(model, userId);

        var movie = await this._movieService.GetMovieDetailsAsync(movieId, userId);

        var expected = "Content2";

        Assert.That(movie!.Reviews.First().Comments.First().Content, Is.EqualTo(expected));
    }

    [Test]
    public void EditCommentAsyncShouldThrowsErrorIfUserIdIsInValid()
    {
        var userId = "2532ddaa-63f0-4de8-71cb-08db8c333234";

        var movieId = 2;

        var commentId = this._dbContext.Movies
            .Where(m => m.Id == movieId)
            .Include(m => m.Reviews)
            .ThenInclude(r => r.Comments)
            .Select(m => m.Reviews.First().Comments.First().Id)
            .First()
            .ToString();

        var model = new EditCommentViewModel()
        {
            Content = "Content2",
            CommentId = commentId,
            MovieId = movieId
        };

        Assert.ThrowsAsync<ArgumentException>(async ()=> await this._movieService.EditCommentAsync(model, userId));
    }

    [Test]
    public void EditCommentAsyncShouldThrowsErrorIfCommentIdIsInValid()
    {
        var userId = "2532ddaa-63f0-4de8-71cb-08db8c333233";

        var movieId = 2;

        var commentId = "2532ddaa-63f0-4de8-71cb-08db8c333263";

        var model = new EditCommentViewModel()
        {
            Content = "Content2",
            CommentId = commentId,
            MovieId = movieId
        };

        Assert.ThrowsAsync<ArgumentException>(async () => await this._movieService.EditCommentAsync(model, userId));
    }


    [Test]
    public async Task RemoveFromUserWatchlistAsyncShouldRemoveFromUserWatchlist()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";

        var movieId = 2;

        await this._movieService.AddToUserWatchlistAsync(userId, movieId);

        var result = await this._movieService.GetWatchlistMoviesAsync(userId);

        Assert.That(result.Any(m => m.Id == movieId), Is.True);

        await this._movieService.RemoveFromUserWatchlistAsync(userId, movieId);

        result = await this._movieService.GetWatchlistMoviesAsync(userId);

        Assert.That(result.Count,Is.EqualTo(0));
    }

    [Test]
    public async Task IsCriticOwnerOfReviewShouldReturnTrueIfCriticIsOwner()
    {
        var criticId = "bf595423-7323-4e40-bd43-0f876c1beece";

        var reviewId = "7DCC5BD6-1133-432B-B6A6-F6C27DA75948".ToLower();

        var result = await this._movieService.IsCriticOwnerOfReview(criticId, reviewId);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task IsCriticOwnerOfReviewShouldReturnFalseIfCriticIsNotOwner()
    {
        var criticId = "bf595423-7323-4e40-bd43-0f876c1beeca";

        var reviewId = "7DCC5BD6-1133-432B-B6A6-F6C27DA75948".ToLower();

        var result = await this._movieService.IsCriticOwnerOfReview(criticId, reviewId);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task IsCriticOwnerOfReviewShouldReturnFalseIfReviewIdIsNotValid()
    {
        var criticId = "bf595423-7323-4e40-bd43-0f876c1beeca";

        var reviewId = "7DCC5BD6-1133-432B-B6A6-F6C27DA75948";

        var result = await this._movieService.IsCriticOwnerOfReview(criticId, reviewId);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task IsCriticOwnerOfReviewShouldReturnFalseIfCriticIdIsNull()
    {
        var reviewId = "7DCC5BD6-1133-432B-B6A6-F6C27DA75948".ToLower();

        var result = await this._movieService.IsCriticOwnerOfReview(null, reviewId);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task IsCriticOwnerOfReviewShouldReturnFalseIfReviewIdIsNull()
    {
        var criticId = "bf595423-7323-4e40-bd43-0f876c1beeca";

        var result = await this._movieService.IsCriticOwnerOfReview(criticId, null);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task IsUserOwnerOfCommentShouldReturnTrueIfUserIsOwner()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233".ToLower();

        var movieId = 2;

        var commentId = this._dbContext.Movies
            .Where(m => m.Id == movieId)
            .Include(m => m.Reviews)
            .ThenInclude(r => r.Comments)
            .Select(m => m.Reviews.First().Comments.First().Id)
            .First()
            .ToString();

        var result = await this._movieService.IsUserOwnerOfComment(userId, commentId);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task IsUserOwnerOfCommentShouldReturnFalseIfUserIsNotOwner()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";

        var movieId = 2;

        var commentId = this._dbContext.Movies
            .Where(m => m.Id == movieId)
            .Include(m => m.Reviews)
            .ThenInclude(r => r.Comments)
            .Select(m => m.Reviews.First().Comments.First().Id)
            .First()
            .ToString();

        var result = await this._movieService.IsUserOwnerOfComment(userId, commentId);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task IsUserOwnerOfCommentShouldReturnFalseIfCommentIdIsNotValid()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233".ToLower();

        var commentId = "2532DDAA-63F0-4DE8-71CB-08DB8C333253".ToLower();

        var result = await this._movieService.IsUserOwnerOfComment(userId, commentId);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task IsUserOwnerOfCommentShouldReturnFalseIfUserIdIsNull()
    {
        var movieId = 2;

        var commentId = this._dbContext.Movies
            .Where(m => m.Id == movieId)
            .Include(m => m.Reviews)
            .ThenInclude(r => r.Comments)
            .Select(m => m.Reviews.First().Comments.First().Id)
            .First()
            .ToString();

        var result = await this._movieService.IsUserOwnerOfComment(null, commentId);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task IsUserOwnerOfCommentShouldReturnFalseIfCommentIdIsNull()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233".ToLower();

        var result = await this._movieService.IsUserOwnerOfComment(userId, null);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task GetMovieForEditByIdAsyncShouldReturnModel()
    {
        var movieId = 1;

        var expected = "The Flash";

        var result = await this._movieService.GetMovieForEditByIdAsync(movieId);

        Assert.That(result.Title,Is.EqualTo(expected));
    }

    [Test]
    public async Task EditMovieByIdAndModelAsyncShouldEditMovie()
    {
        var movieId = 1;

        var model = await this._movieService.GetMovieForEditByIdAsync(movieId);

        var expectedName = "The Flash2";

        model.Title= expectedName;

        await this._movieService.EditMovieByIdAndModelAsync(movieId, model);

        var result = await this._movieService.GetMovieForEditByIdAsync(movieId);

        Assert.That(result.Title,Is.EqualTo(expectedName));

    }

    [Test]
    public async Task GetMovieIdByReviewIdShouldReturnCorrectMovieId()
    {
        var reviewId = "7DCC5BD6-1133-432B-B6A6-F6C27DA75948".ToLower();

        var result = await this._movieService.GetMovieIdByReviewId(reviewId);

        var expected = 2;

        Assert.That(result,Is.EqualTo(expected));
    }
    [Test]
    public async Task GetMovieIdByReviewIdShouldReturnNegativeIfReviewIdIsInvalid()
    {
        var reviewId = "7DCC5BD6-1133-432B-B6A6-F6C27DA75948";

        var result = await this._movieService.GetMovieIdByReviewId(reviewId);

        Assert.That(result, Is.Negative);
    }
}
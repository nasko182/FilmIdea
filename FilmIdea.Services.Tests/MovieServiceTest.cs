namespace FilmIdea.Services.Tests;

using Microsoft.EntityFrameworkCore;

using Data;
using FilmIdea.Data;
using Data.Interfaces;
using Web.ViewModels.Comment;
using Web.ViewModels.Review;
using FilmIdea.Data.Models;
using FilmIdea.Web.ViewModels.Movie;

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
    public async Task GetRouletteMovieAsyncShouldReturnMovie()
    {
        var resultMovie = await this._movieService.GetRouletteMovieAsync();

        Assert.That(resultMovie, Is.Not.Null);
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

        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task IsCriticOwnerOfReviewShouldReturnFalseIfCriticIsNotOwner()
    {
        var criticId = "bf595423-7323-4e40-bd43-0f876c1beeca";

        var reviewId = "7DCC5BD6-1133-432B-B6A6-F6C27DA75948".ToLower();

        var result = await this._movieService.IsCriticOwnerOfReviewAsync(criticId, reviewId);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task IsCriticOwnerOfReviewShouldReturnFalseIfReviewIdIsNotValid()
    {
        var criticId = "bf595423-7323-4e40-bd43-0f876c1beeca";

        var reviewId = "7DCC5BD6-1133-432B-B6A6-F6C27DA75948";

        var result = await this._movieService.IsCriticOwnerOfReviewAsync(criticId, reviewId);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task IsCriticOwnerOfReviewShouldReturnFalseIfCriticIdIsNull()
    {
        var reviewId = "7DCC5BD6-1133-432B-B6A6-F6C27DA75948".ToLower();

        var result = await this._movieService.IsCriticOwnerOfReviewAsync(null, reviewId);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task IsCriticOwnerOfReviewShouldReturnFalseIfReviewIdIsNull()
    {
        var criticId = "bf595423-7323-4e40-bd43-0f876c1beeca";

        var result = await this._movieService.IsCriticOwnerOfReviewAsync(criticId, null);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task IsUserOwnerOfCommentShouldReturnFalseIfCommentIdIsNotValid()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233".ToLower();

        var commentId = "2532DDAA-63F0-4DE8-71CB-08DB8C333253".ToLower();

        var result = await this._movieService.IsUserOwnerOfCommentAsync(userId, commentId);

        Assert.That(result, Is.False);
    }


    [Test]
    public async Task IsUserOwnerOfCommentShouldReturnFalseIfCommentIdIsNull()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233".ToLower();

        var result = await this._movieService.IsUserOwnerOfCommentAsync(userId, null);

        Assert.That(result, Is.False);
    }

    [Test]
    public async Task GetMovieForEditByIdAsyncShouldReturnModel()
    {
        var movieId = 1;

        var expected = "The Flash";

        var result = await this._movieService.GetMovieForEditByIdAsync(movieId);

        Assert.That(result.Title, Is.EqualTo(expected));
    }

    [Test]
    public async Task EditMovieByIdAndModelAsyncShouldEditMovie()
    {
        var movieId = 1;

        var model = await this._movieService.GetMovieForEditByIdAsync(movieId);

        var expectedName = "The Flash2";

        model.Title = expectedName;

        await this._movieService.EditMovieByIdAndModelAsync(movieId, model);

        var result = await this._movieService.GetMovieForEditByIdAsync(movieId);

        Assert.That(result.Title, Is.EqualTo(expectedName));

    }

    [Test]
    public async Task GetMovieIdByReviewIdShouldReturnNegativeIfReviewIdIsInvalid()
    {
        var reviewId = "7DCC5BD6-1133-432B-B6A6-F6C27DA75948";

        var result = await this._movieService.GetMovieIdByReviewIdAsync(reviewId);

        Assert.That(result, Is.Negative);
    }

    [Test]
    public async Task GetAllGenresAsyncShouldReturnCollection()
    {
        var result = await this._movieService.GetAllGenresAsync();

        Assert.That(result.First().Id, Is.EqualTo(1));
    }

    [Test]
    public async Task GetAllGenresAsyncShouldReturnCollection23Genres()
    {
        var result = await this._movieService.GetAllGenresAsync();

        Assert.That(result.Count, Is.EqualTo(23));
    }

    [Test]
    public async Task EditMovieGenresShouldEditMovie()
    {
        var movie = this._dbContext.Movies
            .Include(m => m.Genres)
            .First();

        var genresIds = new List<int>() { 1, 2, 3 };

        await this._movieService.EditMovieGenresAsync(genresIds, movie.Id);

        Assert.That(movie.Genres.First().GenreId, Is.EqualTo(1));
    }

    [Test]
    public async Task AddPhotoShouldAddPhotoToMovie()
    {
        var movie = this._dbContext.Movies
            .Include(m => m.Photos)
            .First();

        string photoUrl = "Url";

        await this._movieService.AddPhotoAsync(movie.Id, photoUrl);

        Assert.That(movie.Photos.Any(p => p.Url == photoUrl), Is.True);
    }

    [Test]
    public async Task AddVideoShouldAddVideoToMovie()
    {
        var movie = this._dbContext.Movies
            .Include(m => m.Videos)
            .First();

        string videoUrl = "Url";

        await this._movieService.AddVideoAsync(movie.Id, videoUrl);

        Assert.That(movie.Videos.Any(p => p.Url == videoUrl), Is.True);
    }

    [Test]
    public async Task AddRemoveLikeAsyncShouldAddLike_WhenNotLikedBefore()
    {
        var model = new AddReviewViewModel
        {
            Title = "Title",
            Rating = 6,
            Content = "Anu content"
        };

        var movieId = 2;
        var criticId = "93372a0a-b9f4-4bc5-8f53-e8da0bce2bfe";

        await this._movieService.AddReviewAsync(model, movieId, criticId);

        var reviewId = this._dbContext.Reviews.First().Id.ToString();

        var userId = "15EB7825-840B-4528-71CC-08DB8C333233".ToLower();

        var likesBefore = await this._dbContext.Likes.CountAsync();
        var result = await this._movieService.AddRemoveLikeAsync(reviewId, userId);
        var likesAfter = await this._dbContext.Likes.CountAsync();

        Assert.AreEqual(likesBefore + 1, likesAfter);
        Assert.AreEqual(1, result);
    }

    [Test]
    public async Task AddRemoveLikeAsyncShouldRemoveLike_WhenLikedBefore()
    {
        var model = new AddReviewViewModel
        {
            Title = "Title",
            Rating = 6,
            Content = "Anu content"
        };

        var movieId = 2;
        var criticId = "93372a0a-b9f4-4bc5-8f53-e8da0bce2bfe";

        await this._movieService.AddReviewAsync(model, movieId, criticId);

        var reviewId = this._dbContext.Reviews.First().Id.ToString();

        var userId = "15EB7825-840B-4528-71CC-08DB8C333233".ToLower();
        await this._movieService.AddRemoveLikeAsync(reviewId, userId);

        var likesBefore = await this._dbContext.Likes.CountAsync();
        var result = await this._movieService.AddRemoveLikeAsync(reviewId, userId);
        var likesAfter = await this._dbContext.Likes.CountAsync();

        // Assert
        Assert.AreEqual(likesBefore - 1, likesAfter);
        Assert.AreEqual(0, result);
    }

    [Test]
    public async Task AddRemoveDislikeAsyncShouldAddDislike_WhenNotDislikedBefore()
    {
        var model = new AddReviewViewModel
        {
            Title = "Title",
            Rating = 6,
            Content = "Anu content"
        };

        var movieId = 2;
        var criticId = "93372a0a-b9f4-4bc5-8f53-e8da0bce2bfe";

        await this._movieService.AddReviewAsync(model, movieId, criticId);

        var reviewId = this._dbContext.Reviews.First().Id.ToString();

        var userId = "15EB7825-840B-4528-71CC-08DB8C333233".ToLower();

        var dislikesBefore = await this._dbContext.Dislikes.CountAsync();
        var result = await this._movieService.AddRemoveDislikeAsync(reviewId, userId);
        var dislikesAfter = await this._dbContext.Dislikes.CountAsync();

        Assert.AreEqual(dislikesBefore + 1, dislikesAfter);
        Assert.AreEqual(1, result);
    }

    [Test]
    public async Task AddRemoveDislikeAsyncShouldRemoveDislike_WhenDislikedBefore()
    {
        var model = new AddReviewViewModel
        {
            Title = "Title",
            Rating = 6,
            Content = "Anu content"
        };

        var movieId = 2;
        var criticId = "93372a0a-b9f4-4bc5-8f53-e8da0bce2bfe";

        await this._movieService.AddReviewAsync(model, movieId, criticId);

        var reviewId = this._dbContext.Reviews.First().Id.ToString();

        var userId = "15EB7825-840B-4528-71CC-08DB8C333233".ToLower();

        await this._movieService.AddRemoveDislikeAsync(reviewId, userId);

        var dislikesBefore = await this._dbContext.Dislikes.CountAsync();
        var result = await this._movieService.AddRemoveDislikeAsync(reviewId, userId);
        var dislikesAfter = await this._dbContext.Dislikes.CountAsync();

        Assert.AreEqual(dislikesBefore - 1, dislikesAfter);
        Assert.AreEqual(0, result);
    }

    [Test]
    public async Task DeleteReviewAsyncShouldDeleteReviewAndRelatedComments()
    {
        var model = new AddReviewViewModel
        {
            Title = "Title",
            Rating = 6,
            Content = "Anu content"
        };

        var movieId = 2;
        var criticId = "93372a0a-b9f4-4bc5-8f53-e8da0bce2bfe";

        await this._movieService.AddReviewAsync(model, movieId, criticId);

        var reviewId = this._dbContext.Reviews.First().Id.ToString();

        await this._movieService.DeleteReviewAsync(reviewId);

        Assert.AreEqual(0, await this._dbContext.Reviews.CountAsync());
        Assert.AreEqual(0, await this._dbContext.Comments.CountAsync());
    }

    [Test]
    public async Task GetAllGenresAsyncShouldReturnCollectionOfGenres()
    {
        var movieId = 2;

        Assert.DoesNotThrowAsync(async () => await this._movieService.GetAllGenresAsync(movieId));
    }

    [Test]
    public async Task CreateAsyncShouldCreateMovie()
    {
        var model = new AddMovieViewModel
        {
            Title = "title",
            Description = "description",
            ReleaseDate = DateTime.UtcNow,
            Duration = 143,
            DirectorId = 2
        };
        string photoUrl = "url";
        string videoUrl = "url";

        var result = this._movieService.CreateAsync(model, photoUrl, videoUrl);

        Assert.IsNotNull(result);
    }
}
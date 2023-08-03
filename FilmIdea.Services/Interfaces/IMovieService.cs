namespace FilmIdea.Services.Data.Interfaces;

using FilmIdea.Data.Models;
using Web.ViewModels.Comment;
using Web.ViewModels.Movie;
using Web.ViewModels.Review;

public interface IMovieService
{
    Task<bool> IsGenreIdValid(int genreId);

    Task<string?> GetGenreNameByIdAsync(int genreId);

    Task<AllMoviesViewModel> GetAllMoviesAsync(string? userId);

    Task<MoviesAndTopViewModel> GetMoviesByGenreAsync(string userId,int genreId);

    Task<MoviesAndTopViewModel> GetNewMoviesAsync(string userId);

    Task<MoviesAndTopViewModel> GetTop250MoviesAsync(string userId);

    Task<MovieDetailsViewModel?> GetMovieAsync(int movieId, string userId);

    Task<MovieViewModel> GetRouletteMovieAsync(string? userId);

    Task<List<GenreViewModel>> GetGenresAsync();

    Task<List<MovieViewModel>> GetWatchlistMoviesAsync(string userId);

    Task AddRatingAsync(int movieId, int ratingValue, string userId);

    Task AddReviewAsync(AddReviewViewModel model, int movieId,string userId);

    Task AddCommentAsync(AddCommentViewModel model, string reviewId,string userId);

    Task AddToUserWatchlist(string userId, int movieId);

    Task RemoveFromUserWatchlist(string userId, int movieId);

    Task AddRemoveLike(string reviewId, string userId);

    Task AddRemoveDislike(string reviewId, string userId);

}
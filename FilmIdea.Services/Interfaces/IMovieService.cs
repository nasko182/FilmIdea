namespace FilmIdea.Services.Data.Interfaces;

using Web.ViewModels.Movie;

public interface IMovieService 
{
    Task<AllMoviesViewModel> GetAllMoviesAsync(string? userId);

    Task<MoviesAndTopViewModel> GetMoviesByGenreAsync(string userId,int genreId);

    Task<MoviesAndTopViewModel> GetNewMoviesAsync(string userId);

    Task<MoviesAndTopViewModel> GetTop250MoviesAsync(string userId);

    Task<MovieDetailsViewModel?> GetMovieAsync(int movieId, string userId);

    Task<MovieViewModel> GetRouletteMovieAsync(string? userId);

    Task<List<GenreViewModel>> GetGenresAsync();

    Task AddRatingAsync(int movieId, int ratingValue, string userId);

    Task AddToUserWatchlist(string userId, int movieId);

    Task RemoveFromUserWatchlist(string userId, int movieId);

    Task<List<MovieViewModel>> GetWatchlistMoviesAsync(string userId);
}
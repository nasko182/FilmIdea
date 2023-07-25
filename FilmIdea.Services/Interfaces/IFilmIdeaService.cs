namespace FilmIdea.Services.Data.Interfaces;

using Web.ViewModels.Movie;

public interface IFilmIdeaService
{
    Task<AllMoviesViewModel> GetAllMoviesAsync(string userId);

    Task<MovieDetailsViewModel> GetMovieAsync(int movieId, string userId);

    Task AddRatingAsync(int movieId, int ratingValue, string userId);
}
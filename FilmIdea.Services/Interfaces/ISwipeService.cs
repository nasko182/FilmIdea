namespace FilmIdea.Services.Data.Interfaces;

using Web.ViewModels.Swipe;

public interface ISwipeService
{
    Task<List<SwipeMovieViewModel>> GetMoviesAsync(string userId);

    Task AddMovieToUserPassedList(string userId, int movieId);

    Task ResetPassedList(string userId);

}

namespace FilmIdea.Services.Data.Interfaces;

using Web.ViewModels.Swipe;

public interface ISwipeService 
{
    Task<List<SwipeMovieViewModel>> GetMoviesAsync(string userId);

    Task AddMovieToUserPassedListAsync(string userId, int movieId);

    Task ResetPassedListAsync(string userId);

}

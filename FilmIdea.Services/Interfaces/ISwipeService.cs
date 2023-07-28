namespace FilmIdea.Services.Data.Interfaces;

using Web.ViewModels.Swipe;

public interface ISwipeService
{
    Task<List<SwipeMovieViewModel>> GetMoviesAsync();
}

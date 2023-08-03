namespace FilmIdea.Services.Data.Interfaces;

using Models.Movies;
using Web.ViewModels.Comment;
using Web.ViewModels.Genre;
using Web.ViewModels.Movie;
using Web.ViewModels.Review;

public interface IMovieService
{
    Task<MoviesFilteredAndPagesServiceModel> AllAsync(MoviesQueryModel queryModel,string userId);

    Task<ICollection<TopSectionMovieViewModel>> GetMoviesForTopSectionAsync();

    Task<bool> IsGenreIdValidAsync(int genreId);

    Task<string?> GetGenreNameByIdAsync(int genreId);

    Task<MoviesAndTopViewModel> GetMoviesByGenreAsync(string userId,int genreId);

    Task<MoviesAndTopViewModel> GetNewMoviesAsync(string userId);

    Task<MoviesAndTopViewModel> GetTop250MoviesAsync(string userId);

    Task<MovieDetailsViewModel?> GetMovieAsync(int movieId, string userId);

    Task<MovieViewModel> GetRouletteMovieAsync(string? userId);

    Task<List<GenreViewModel>> GetAllGenresAsync();

    Task<List<MovieViewModel>> GetWatchlistMoviesAsync(string userId);

    Task AddRatingAsync(int movieId, int ratingValue, string userId);

    Task AddReviewAsync(AddReviewViewModel model, int movieId,string userId);

    Task EditReviewAsync(EditReviewViewModel model, string criticId);

    Task AddCommentAsync(AddCommentViewModel model,string userId);

    Task EditCommentAsync(EditCommentViewModel model,string userId);

    Task AddToUserWatchlistAsync(string userId, int movieId);

    Task RemoveFromUserWatchlistAsync(string userId, int movieId);

    Task AddRemoveLikeAsync(string reviewId, string userId);

    Task AddRemoveDislikeAsync(string reviewId, string userId);

    Task DeleteReviewAsync(string reviewId,string criticId);

    Task DeleteCommentAsync(string commentId,string userId);

}
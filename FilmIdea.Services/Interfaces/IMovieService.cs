﻿namespace FilmIdea.Services.Data.Interfaces;

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

    Task<MoviesAndTopViewModel> GetMoviesByGenreAsync(string? userId,int genreId);

    Task<MoviesAndTopViewModel> GetNewMoviesAsync(string? userId);

    Task<MoviesAndTopViewModel> GetTop250MoviesAsync(string userId);

    Task<MovieDetailsViewModel?> GetMovieDetailsAsync(int movieId, string userId);

    Task<MovieViewModel> GetRouletteMovieAsync();

    Task<List<GenreViewModel>> GetAllGenresAsync();

    Task<List<MovieViewModel>> GetWatchlistMoviesAsync(string userId);

    Task AddRatingAsync(int movieId, int ratingValue, string userId);

    Task AddReviewAsync(AddReviewViewModel model, int movieId,string criticId);

    Task EditReviewAsync(EditReviewViewModel model, string criticId);

    Task AddCommentAsync(AddCommentViewModel model,string userId);

    Task EditCommentAsync(EditCommentViewModel model,string userId);

    Task AddToUserWatchlistAsync(string userId, int movieId);

    Task RemoveFromUserWatchlistAsync(string userId, int movieId);

    Task<int> AddRemoveLikeAsync(string reviewId, string userId);

    Task<int> AddRemoveDislikeAsync(string reviewId, string userId);

    Task DeleteReviewAsync(string reviewId);

    Task<int> DeleteCommentAsync(string commentId);

    Task<bool> IsCriticOwnerOfReviewAsync(string? criticId, string? reviewId);

    Task<bool> IsUserOwnerOfCommentAsync(string? userId, string? commentId);

    Task<int> CreateAsync(AddMovieViewModel model, string photoUrl, string videoUrl);

    Task<EditMovieViewModel> GetMovieForEditByIdAsync(int id);

    Task EditMovieByIdAndModelAsync(int id,EditMovieViewModel model);

    Task DeleteMovieByIdAsync(int id);

    Task<int> GetMovieIdByReviewIdAsync(string? reviewId);

    Task<ICollection<EditMovieGenres>> GetAllGenresAsync(int movieId);

    Task EditMovieGenresAsync(List<int> genresIds, int movieId);

    Task AddPhotoAsync(int movieId, string photoUrl);

    Task AddVideoAsync(int movieId, string videoUrl);
}
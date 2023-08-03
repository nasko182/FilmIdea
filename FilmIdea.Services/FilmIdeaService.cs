namespace FilmIdea.Services.Data;

using FilmIdea.Data.Models;
using Interfaces;

public class FilmIdeaService : IFilmIdeaService
{
    public static int GetRating(ICollection<UserRating> userRating, int movieId)
    {
        if (userRating.Any())
        {
            var movieRating = userRating.FirstOrDefault(ur => ur.MovieId == movieId);
            if (movieRating != null)
            {
                return movieRating.Rating;
            }
        }
        return 0;

    }

    public static bool HasMovieInUserWatchlist(string userId, Movie movie)
    {
        if (userId == null)
        {
            return false;
        }
        return movie.UsersWatchlists.Any(um => um.MovieId == movie.Id && um.UserId == Guid.Parse(userId));
    }
}

namespace FilmIdea.Services.Data;

using FilmIdea.Data;
using FilmIdea.Data.Models;
using FilmIdea.Services.Data.Interfaces;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.Actor;
using Web.ViewModels.Movie;

public class ActorService : FilmIdeaService, IActorService
{
    private readonly FilmIdeaDbContext _dbContext;

    public ActorService(FilmIdeaDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<ActorDetailsViewModel?> GetActorDetailsAsync(int actorId,string? userId)
    {
        if (userId == null)
        {
            return await this._dbContext.Actors
                .Where(a => a.Id == actorId)
                .Include(a => a.Movies)
                .ThenInclude(ma=>ma.Movie)
                .ThenInclude(m=>m.Ratings)
                .Select(a => new ActorDetailsViewModel()
                {
                    Id = a.Id,
                    Bio = a.Bio,
                    DateOfBirth = a.DateOfBirth.ToString("MMMM dd,yyyy"),
                    Name = a.Name,
                    ProfileImageUrl = a.ProfileImageUrl,
                    Photos = a.Photos.Select(p => p.Url).ToList(),
                    Videos = a.Videos.Select(v => v.Url).ToList(),
                    Movies = a.Movies.Select(ma => new MovieViewModel()
                    {
                        Id = ma.MovieId,
                        Title = ma.Movie.Title,
                        Rating = ma.Movie.CalculateUserRating(),
                        CoverPhotoUrl = ma.Movie.CoverImageUrl,
                        Duration = ma.Movie.Duration,
                        ReleaseYear = ma.Movie.ReleaseDate.Year
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }
        else
        {
            var userRatings = await this._dbContext.UserRatings
                .Where(r => r.UserId.ToString() == userId)
                .Select(r => new UserRating()
                {
                    Rating = r.Rating,
                    MovieId = r.MovieId
                })
                .ToListAsync();

            return await this._dbContext.Actors
                .Where(a => a.Id == actorId)
                .Include(a=>a.Movies)
                .ThenInclude(ma=>ma.Movie)
                .ThenInclude(m=>m.Ratings)
                .Include(a => a.Movies)
                .ThenInclude(ma => ma.Movie)
                .ThenInclude(m => m.UsersWatchlists)
                .Select(a => new ActorDetailsViewModel()
                {
                    Id = a.Id,
                    Bio = a.Bio,
                    DateOfBirth = a.DateOfBirth.ToString("MMMM dd,yyyy"),
                    Name = a.Name,
                    ProfileImageUrl = a.ProfileImageUrl,
                    Photos = a.Photos.Select(p => p.Url).ToList(),
                    Videos = a.Videos.Select(v => v.Url).ToList(),
                    Movies = a.Movies.Select(ma => new MovieViewModel()
                    {
                        Id = ma.MovieId,
                        Title = ma.Movie.Title,
                        Rating = ma.Movie.CalculateUserRating(),
                        CoverPhotoUrl = ma.Movie.CoverImageUrl,
                        Duration = ma.Movie.Duration,
                        ReleaseYear = ma.Movie.ReleaseDate.Year,
                        HasMovieInWatchlist = HasMovieInUserWatchlist(userId,ma.Movie),
                        UserRating = GetRating(userRatings, ma.MovieId)

                    }).ToList()

                }).FirstOrDefaultAsync();
        }
    }

    
}

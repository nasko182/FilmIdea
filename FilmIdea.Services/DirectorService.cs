namespace FilmIdea.Services.Data;

using FilmIdea.Data;
using FilmIdea.Data.Models;
using Web.ViewModels.Director;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.Movie;

public class DirectorService : FilmIdeaService, IDirectorService
{
    private readonly FilmIdeaDbContext _dbContext;

    public DirectorService(FilmIdeaDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<DirectorDetailsViewModel?> GetDirectorDetailsAsync(int diretorId,string? userId)
    {
        if (userId == null)
        {
            return await this._dbContext.Directors
                .Where(a => a.Id == diretorId)
                .Select(a => new DirectorDetailsViewModel()
                {
                    Id = a.Id,
                    Bio = a.Bio,
                    DateOfBirth = a.DateOfBirth.ToString("MMMM dd,yyyy"),
                    Name = a.Name,
                    ProfileImageUrl = a.ProfileImageUrl,
                    Photos = a.Photos.Select(p => p.Url).ToList(),
                    Videos = a.Videos.Select(v => v.Url).ToList(),
                    Movies = a.Movies.Select(m => new MovieViewModel()
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Rating = m.CalculateUserRating(),
                        CoverPhotoUrl = m.CoverImageUrl,
                        Duration = m.Duration,
                        ReleaseYear = m.ReleaseDate.Year
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

            return await this._dbContext.Directors
                .Where(a => a.Id == diretorId)
                .Include(a=>a.Movies)
                .ThenInclude(m=>m.Ratings)
                .Include(a => a.Movies)
                .ThenInclude(m => m.UsersWatchlists)
                .Select(a => new DirectorDetailsViewModel()
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
                        Id = ma.Id,
                        Title = ma.Title,
                        Rating = ma.CalculateUserRating(),
                        CoverPhotoUrl = ma.CoverImageUrl,
                        Duration = ma.Duration,
                        ReleaseYear = ma.ReleaseDate.Year,
                        HasMovieInWatchlist = HasMovieInUserWatchlist(userId,ma),
                        UserRating = GetRating(userRatings, ma.Id)

                    }).ToList()

                }).FirstOrDefaultAsync();
        }
    }

    
}

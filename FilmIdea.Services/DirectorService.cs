namespace FilmIdea.Services.Data;

using FilmIdea.Data;
using FilmIdea.Data.Models;
using Web.ViewModels.Director;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels.Movie;
using FilmIdea.Web.ViewModels.Actor;

public class DirectorService : FilmIdeaService, IDirectorService
{
    private readonly FilmIdeaDbContext _dbContext;
    private readonly IMovieService _movieService;

    public DirectorService(FilmIdeaDbContext dbContext, IMovieService movieService)
    {
        this._dbContext = dbContext;
        this._movieService = movieService;
    }

    public async Task<DirectorDetailsViewModel?> GetDirectorDetailsAsync(int directorId,string? userId)
    {
        if (userId == null)
        {
            return await this._dbContext.Directors
                .Where(a => a.Id == directorId)
                .Include(d=>d.Movies)
                .ThenInclude(m=>m.Ratings)
                .Select(d => new DirectorDetailsViewModel()
                {
                    Id = d.Id,
                    Bio = d.Bio,
                    DateOfBirth = d.DateOfBirth.ToString("MMMM dd,yyyy"),
                    Name = d.Name,
                    ProfileImageUrl = d.ProfileImageUrl,
                    Photos = d.Photos.Select(p => p.Url).ToList(),
                    Videos = d.Videos.Select(v => v.Url).ToList(),
                    Movies = d.Movies.Select(m => new MovieViewModel()
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
                .Where(a => a.Id == directorId)
                .Include(a=>a.Movies)
                .ThenInclude(m=>m.Ratings)
                .Include(a => a.Movies)
                .ThenInclude(m => m.UsersWatchlists)
                .Select(d => new DirectorDetailsViewModel()
                {
                    Id = d.Id,
                    Bio = d.Bio,
                    DateOfBirth = d.DateOfBirth.ToString("MMMM dd,yyyy"),
                    Name = d.Name,
                    ProfileImageUrl = d.ProfileImageUrl,
                    Photos = d.Photos.Select(p => p.Url).ToList(),
                    Videos = d.Videos.Select(v => v.Url).ToList(),
                    Movies = d.Movies.Select(ma => new MovieViewModel()
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

    public async Task<int> CreateAsync(AddDirectorViewModel model, string photoUrl)
    {
        var director = new Director()
        {
            Name = model.Name,
            Bio = model.Bio,
            ProfileImageUrl = photoUrl,
            DateOfBirth = model.DateOfBirth
        };

        await this._dbContext.Directors.AddAsync(director);

        try
        {
            await this._dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(e.Message);
        }

        return director.Id;
    }

    public async Task<EditDirectorViewModel> GetDirectorForEditByIdAsync(int id)
    {
        var director = await this._dbContext.Directors
            .Where(d => d.Id == id)
            .FirstAsync();

        return new EditDirectorViewModel()
        {
            Name = director.Name,
            Bio = director.Bio,
            DateOfBirth = director.DateOfBirth,
            ProfileImageUrl = director.ProfileImageUrl
        };
    }

    public async Task EditDirectorByIdAndModelAsync(int id, EditDirectorViewModel model)
    {
        var director = await this._dbContext.Directors
            .Where(d => d.Id == id)
            .FirstAsync();

        director.Name = model.Name;
        director.Bio = model.Bio;
        director.DateOfBirth = model.DateOfBirth;
        director.ProfileImageUrl = model.ProfileImageUrl;

        try
        {
            await this._dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException();
        }
    }

    public async Task DeleteDirectorByIdAsync(int id)
    {
        var director = await _dbContext
            .Directors
            .Include(d=>d.Movies)
            .ThenInclude(m => m.Reviews)
            .ThenInclude(r => r.Comments)
            .Include(d=>d.Movies)
            .ThenInclude(m => m.Actors)
            .Include(d=>d.Movies)
            .ThenInclude(m => m.Genres)
            .Include(d=>d.Movies)
            .ThenInclude(m => m.PassedUsers)
            .Include(d=>d.Movies)
            .ThenInclude(m => m.Ratings)
            .Include(d=>d.Movies)
            .ThenInclude(m => m.UsersWatchlists)
            .Include(d=>d.Movies)
            .ThenInclude(m => m.Photos)
            .Include(d=>d.Movies)
            .ThenInclude(m => m.Videos)
            .FirstAsync(d => d.Id == id);

        foreach (var movie in director.Movies)
        {
            await this._movieService.DeleteMovieByIdAsync(movie.Id);
        }

        this._dbContext.Photos.RemoveRange(director.Photos);
        this._dbContext.Videos.RemoveRange(director.Videos);

        try
        {
            _dbContext.Directors.Remove(director);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch
        {
            throw new InvalidOperationException();
        }
    }
}

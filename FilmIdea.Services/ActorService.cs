namespace FilmIdea.Services.Data;

using Microsoft.EntityFrameworkCore;

using FilmIdea.Data;
using FilmIdea.Data.Models;
using FilmIdea.Data.Models.Join_Tables;
using Interfaces;
using Web.ViewModels.Actor;
using Web.ViewModels.Movie;

public class ActorService : FilmIdeaService, IActorService
{
    private readonly FilmIdeaDbContext _dbContext;

    public ActorService(FilmIdeaDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<ActorDetailsViewModel?> GetActorDetailsAsync(int actorId, string? userId)
    {
        if (userId == null)
        {
            return await this._dbContext.Actors
                .Where(a => a.Id == actorId)
                .Include(a => a.Movies)
                .ThenInclude(ma => ma.Movie)
                .ThenInclude(m => m.Ratings)
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
                .Include(a => a.Movies)
                .ThenInclude(ma => ma.Movie)
                .ThenInclude(m => m.Ratings)
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
                        HasMovieInWatchlist = HasMovieInUserWatchlist(userId, ma.Movie),
                        UserRating = GetRating(userRatings, ma.MovieId)

                    }).ToList()

                }).FirstOrDefaultAsync();
        }
    }

    public async Task<int> CreateAsync(AddActorViewModel model, string photoUrl)
    {
        var actor = new Actor
        {
            Name = model.Name,
            Bio = model.Bio,
            ProfileImageUrl = photoUrl,
            DateOfBirth = model.DateOfBirth
        };

        await this._dbContext.Actors.AddAsync(actor);

        try
        {
            await this._dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(e.Message);
        }

        return actor.Id;
    }

    public async Task<EditActorViewModel> GetActorForEditByIdAsync(int id)
    {
        var actor = await this._dbContext.Actors
            .Where(a => a.Id == id)
            .FirstAsync();

        return new EditActorViewModel
        {
            Name = actor.Name,
            Bio = actor.Bio,
            DateOfBirth = actor.DateOfBirth,
            ProfileImageUrl = actor.ProfileImageUrl
        };
    }

    public async Task EditActorByIdAndModelAsync(int id, EditActorViewModel model)
    {
        var actor = await this._dbContext.Actors
            .Where(a => a.Id == id)
            .FirstAsync();

        actor.Name = model.Name;
        actor.Bio = model.Bio;
        actor.DateOfBirth = model.DateOfBirth;
        actor.ProfileImageUrl = model.ProfileImageUrl;

        try
        {
            await this._dbContext.SaveChangesAsync();
        }
        catch
        {
            throw new InvalidOperationException();
        }
    }

    public async Task DeleteActorByIdAsync(int id)
    {
        var actor = await _dbContext
            .Actors
            .FirstAsync(a => a.Id == id);

        this._dbContext.MoviesActors.RemoveRange(actor.Movies);
        this._dbContext.Photos.RemoveRange(actor.Photos);
        this._dbContext.Videos.RemoveRange(actor.Videos);

        _dbContext.Remove(actor);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<EditMovieActors>> GetAllActorsAsync(int movieId)
    {
        var actors = new List<EditMovieActors>();

        var actorsInMovie = await this._dbContext.Actors
            .Include(a => a.Movies)
            .Where(a => a.Movies.Any(m => m.MovieId == movieId))
            .Select(a => new EditMovieActors
            {
                Id = a.Id,
                Name = a.Name,
                IsInMovie = true,
                MovieId = movieId
            })
            .ToListAsync();

        actors.AddRange(actorsInMovie);

        var actorsNotInMovie = await this._dbContext.Actors
            .Include(a => a.Movies)
            .Where(a => !a.Movies.Any(m => m.MovieId == movieId))
            .Select(a => new EditMovieActors
            {
                Id = a.Id,
                Name = a.Name,
                IsInMovie = false,
                MovieId = movieId
            })
            .ToListAsync();

        actors.AddRange(actorsNotInMovie);

        return actors;
    }

    public async Task EditMovieActorsAsync(List<int> actorsIds, int movieId)
    {
        var movie = await this._dbContext.Movies
            .Include(m => m.Actors)
            .FirstAsync(m => m.Id == movieId);

        var actorsToRemove = movie.Actors
            .Where(ma => !actorsIds.Contains(ma.ActorId))
            .ToList();

        var existingUserIds = movie.Actors.Select(ma => ma.ActorId).ToList();

        var newActorIdsToAdd = actorsIds
            .Except(existingUserIds).ToList();

        foreach (var id in newActorIdsToAdd)
        {
            var newActor = new MovieActor()
            {
                ActorId = id,
                MovieId = movieId
            };
            movie.Actors.Add(newActor);
        }

        _dbContext.MoviesActors.RemoveRange(actorsToRemove);


        try
        {
            await this._dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public async Task AddPhotoAsync(int actorId, string photoUrl)
    {

        await this._dbContext.Photos.AddAsync(new Photo
        {
            Url = photoUrl,
            ActorId = actorId
        });

        try
        {
            await this._dbContext.SaveChangesAsync();
        }
        catch
        {
            throw new InvalidOperationException();
        }
    }

    public async Task AddVideoAsync(int actorId, string videoUrl)
    {
        await this._dbContext.Videos.AddAsync(new Video
        {
            Url = videoUrl,
            ActorId = actorId
        });

        try
        {
            await this._dbContext.SaveChangesAsync();
        }
        catch
        {
            throw new InvalidOperationException();
        }
    }
}

﻿namespace FilmIdea.Services.Data;

using FilmIdea.Data;
using Microsoft.EntityFrameworkCore;

using Interfaces;
using Web.ViewModels.Swipe;
using FilmIdea.Data.Models.Join_Tables;
using FilmIdea.Data.Models;
using Web.ViewModels.Genre;

public class SwipeService : FilmIdeaService, ISwipeService
{
    private readonly FilmIdeaDbContext _dbContext;

    public SwipeService(FilmIdeaDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    public async Task<List<SwipeMovieViewModel>> GetMoviesAsync(string userId)
    {
        return await this._dbContext.Movies
            .Where(m=> !m.PassedUsers.Any(pm=> pm.MovieId == m.Id && pm.UserId == Guid.Parse(userId)))
            .OrderBy(m=>m.CoverImageUrl)
            .Include(m=>m.Ratings)
            .Include(m=>m.Genres)
            .Select(m=> new SwipeMovieViewModel()
            {
                Id = m.Id,
                CoverImageUrl = m.CoverImageUrl,
                Genres = m.Genres.Select(g => new GenreViewModel()
                {
                    Id = g.GenreId,
                    Name = g.Genre.Name
                }).ToList(),
                Description = m.Description,
                Duration = m.Duration,
                ReleaseYear = m.ReleaseDate.Year,
                Title = m.Title,
                TrailerUrl = m.TrailerUrl,
                Rating = m.Ratings.Average(r => r.Rating)
            })
            .ToListAsync();
    }

    public async Task AddMovieToUserPassedListAsync(string userId, int movieId)
    {
        var movie = await this._dbContext.Movies
            .Include(m => m.PassedUsers)
            .FirstOrDefaultAsync(m => m.Id == movieId);

        if (movie != null)
        {

            if (!HasUserInPassedUser(userId, movie))
            {
                movie.PassedUsers.Add(new PassedMovie()
                {
                    UserId = Guid.Parse(userId),
                    MovieId = movieId
                });

                try
                {
                    await this._dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }

    public async Task ResetPassedListAsync(string userId)
    {
        var passedMovies = await this._dbContext.PassedMovies
            .Where(pm => pm.UserId == Guid.Parse(userId))
            .ToListAsync();

        foreach (var passedMovie in passedMovies)
        {
            this._dbContext.PassedMovies.Remove(passedMovie);
        }

        await this._dbContext.SaveChangesAsync();
    }


    private static bool HasUserInPassedUser(string userId, Movie movie)
    {
        return movie.PassedUsers.Any(pm => pm.MovieId == movie.Id && pm.UserId == Guid.Parse(userId));
    }
}

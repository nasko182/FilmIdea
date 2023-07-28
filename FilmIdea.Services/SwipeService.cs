namespace FilmIdea.Services.Data;

using FilmIdea.Data;
using Microsoft.EntityFrameworkCore;

using Interfaces;
using Web.ViewModels.Movie;
using Web.ViewModels.Swipe;

public class SwipeService : ISwipeService
{
    private readonly FilmIdeaDbContext _dbContext;

    public SwipeService(FilmIdeaDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    public async Task<List<SwipeMovieViewModel>> GetMoviesAsync()
    {
        return await this._dbContext.Movies
            .OrderBy(m=>m.CoverImageUrl)
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
                TrailerUrl = m.TrailerUrl
            })
            .ToListAsync();
    }
}

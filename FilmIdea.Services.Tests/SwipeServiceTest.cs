namespace FilmIdea.Services.Tests;

using Microsoft.EntityFrameworkCore;

using Data;
using FilmIdea.Data;
using Data.Interfaces;
using FilmIdea.Data.Models.Join_Tables;

[TestFixture]
public class SwipeServiceTest
{
    private DbContextOptions<FilmIdeaDbContext> _dbOptions;
    private FilmIdeaDbContext _dbContext;

    private ISwipeService _swipeService;

    [SetUp]
    public void SetUp()
    {
        this._dbOptions = new DbContextOptionsBuilder<FilmIdeaDbContext>()
            .UseInMemoryDatabase("FilmIdeaInMemory" + Guid.NewGuid())
            .Options;

        this._dbContext = new FilmIdeaDbContext(this._dbOptions);

         this._dbContext.Database.EnsureCreated();

        this._swipeService = new SwipeService(this._dbContext); 
    }

    [TearDown]
    public void TearDown()
    {
        this._dbContext.Database.EnsureDeleted();
    }

    [Test]
    public async Task AddMovieToUserPassedListAsyncShouldAddMovieToPassedList()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";
        var movieId = 1; 

        await this._swipeService.AddMovieToUserPassedListAsync(userId, movieId);

        var passedMovie = await this._dbContext.PassedMovies
            .FirstOrDefaultAsync(pm => pm.UserId == Guid.Parse(userId) && pm.MovieId == movieId);

        Assert.IsNotNull(passedMovie);
    }

    [Test]
    public async Task ResetPassedListAsyncShouldRemoveAllPassedMoviesForUser()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";

        var passedMovies = new List<PassedMovie>
        {
            new PassedMovie { UserId = Guid.Parse(userId), MovieId = 1 },
            new PassedMovie { UserId = Guid.Parse(userId), MovieId = 2 }
        };
        await this._dbContext.PassedMovies.AddRangeAsync(passedMovies);
        await this._dbContext.SaveChangesAsync();

        await this._swipeService.ResetPassedListAsync(userId);

        var passedMoviesCount = await this._dbContext.PassedMovies
            .Where(pm => pm.UserId == Guid.Parse(userId))
            .CountAsync();

        Assert.That(passedMoviesCount, Is.Zero);
    }
}

namespace FilmIdea.Services.Tests;

using Microsoft.EntityFrameworkCore;

using Data;
using FilmIdea.Data;
using Data.Interfaces;

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
    public async Task GetMoviesAsyncShouldGetCollectionOfMovies()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";

        var result = await this._swipeService.GetMoviesAsync(userId.ToLower());

        Assert.That(result, Is.Not.Null);
    }
}
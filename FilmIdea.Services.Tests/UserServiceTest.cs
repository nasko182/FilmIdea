namespace FilmIdea.Services.Tests;

using Microsoft.EntityFrameworkCore;

using Data;
using FilmIdea.Data;
using Data.Interfaces;
using FilmIdea.Data.Models.Join_Tables;

[TestFixture]
public class UserServiceTest
{
    private DbContextOptions<FilmIdeaDbContext> _dbOptions;
    private FilmIdeaDbContext _dbContext;

    private IUserService _userService;

    [SetUp]
    public void SetUp()
    {
        this._dbOptions = new DbContextOptionsBuilder<FilmIdeaDbContext>()
            .UseInMemoryDatabase("FilmIdeaInMemory" + Guid.NewGuid())
            .Options;

        this._dbContext = new FilmIdeaDbContext(this._dbOptions);

         this._dbContext.Database.EnsureCreated();

        this._userService = new UserService(this._dbContext); 
    }

    [TearDown]
    public void TearDown()
    {
        this._dbContext.Database.EnsureDeleted();
    }

    [Test]
    public async Task AllAsyncShouldNotThrowErrors()
    {
        Assert.DoesNotThrowAsync(async ()=> await this._userService.AllAsync());
    }

    [Test]
    public async Task AllAsyncShouldReturnCollection()
    {
        var result = await this._userService.AllAsync();

        var expected = 3;
        
        Assert.That(result.Count(), Is.EqualTo(expected));
    }
}

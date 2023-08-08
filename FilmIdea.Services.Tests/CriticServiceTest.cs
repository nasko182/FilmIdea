namespace FilmIdea.Services.Tests;

using Data;
using Microsoft.EntityFrameworkCore;

using FilmIdea.Data;
using Data.Interfaces;

[TestFixture]
public class CriticServiceTest
{
    private DbContextOptions<FilmIdeaDbContext> _dbOptions;
    private FilmIdeaDbContext _dbContext;

    private ICriticService _criticService;

    [SetUp]
    public void SetUp()
    {
        this._dbOptions = new DbContextOptionsBuilder<FilmIdeaDbContext>()
            .UseInMemoryDatabase("FilmIdeaInMemory" + Guid.NewGuid())
            .Options;

        this._dbContext = new FilmIdeaDbContext(this._dbOptions);

         this._dbContext.Database.EnsureCreated();

        this._criticService = new CriticService(this._dbContext); 
    }

    [TearDown]
    public void TearDown()
    {
        this._dbContext.Database.EnsureDeleted();
    }

    [Test]
    public async Task CriticExistByUserIdAsyncShouldReturnTrueWhenExists()
    {
        var existingCriticUserId = this._dbContext
            .ApplicationUsers
            .First(u => u.CriticId != null)
            .Id.ToString();

        var result = await this._criticService.CriticExistByUserIdAsync(existingCriticUserId);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task CriticExistByUserIdAsyncShouldReturnFalseWhenNotExists()
    {
        var existingNotCriticUserId = this._dbContext
            .ApplicationUsers
            .First(u => u.CriticId == null)
            .Id.ToString();

        var result = await this._criticService.CriticExistByUserIdAsync(existingNotCriticUserId);

        Assert.That(result,Is.False);
    }

    [Test]
    public async Task GetCriticIdAsyncShouldReturnCriticIdWhenUserIsCritic()
    {
        var existingCriticUserId = this._dbContext
            .ApplicationUsers
            .First(u => u.CriticId != null)
            .Id;

        var expected = this._dbContext
            .ApplicationUsers
            .First(u => u.Id == existingCriticUserId)
            .CriticId.ToString();

        var result = await this._criticService.GetCriticIdAsync(existingCriticUserId.ToString());

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetCriticIdAsyncShouldReturnNullWhenUserIdNotCritic()
    {
        var existingNotCriticUserId = this._dbContext
            .ApplicationUsers
            .First(u => u.CriticId == null)
            .Id;

        var result = await this._criticService.GetCriticIdAsync(existingNotCriticUserId.ToString());

        Assert.That(result,Is.Null);
    }

    [Test]
    public async Task GetCriticIdAsyncShouldReturnNullWhenUserIdDontExist()
    {
        var notExistingUserId = Guid.NewGuid();

        var result = await this._criticService.GetCriticIdAsync(notExistingUserId.ToString());

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetCriticIdAsyncShouldReturnNullWhenUserIdIsNull()
    {
        var result = await this._criticService.GetCriticIdAsync(null);

        Assert.That(result, Is.Null);
    }

}
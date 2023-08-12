namespace FilmIdea.Services.Tests;

using Data;
using Microsoft.EntityFrameworkCore;

using FilmIdea.Data;
using Data.Interfaces;
using FilmIdea.Data.Models;
using FilmIdea.Web.ViewModels.Critic;

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

    [Test]
    public async Task CreateCriticAsyncShouldCreateNewCritic()
    {
        var userId ="2532DDAA-63F0-4DE8-71CB-08DB8C333233".ToLower();
        var model = new BecomeCriticViewModel
        {
            Name = "Test Critic",
            Bio = "Test Bio",
            DateOfBirth = new DateTime(1990, 1, 1)
        };
        var photoUrl = "test_photo_url";

        await this._criticService.CreateCriticAsync(userId, model, photoUrl);

        var createdCritic = await this._dbContext.Critics.FirstOrDefaultAsync(c=>c.UserId.ToString()==userId);

        Assert.NotNull(createdCritic);

        Assert.AreEqual(model.Name, createdCritic.Name);
        Assert.AreEqual(model.Bio, createdCritic.Bio);
        Assert.AreEqual(model.DateOfBirth, createdCritic.DateOfBirth);
        Assert.AreEqual(userId, createdCritic.UserId.ToString());
    }

    [Test]
    public async Task GetCriticNameShouldReturnCriticNameIfExists()
    {
        var userId = "15EB7825-840B-4528-71CC-08DB8C333233".ToLower();
        
        // Act
        var result = await this._criticService.GetCriticName(userId);

        var expected = "Critic Criticov";
        // Assert
        Assert.AreEqual(expected, result);
    }

    [Test]
    public async Task GetCriticDetailsByIdAsyncShouldGetDetails()
    {
        var criticId = "93372a0a-b9f4-4bc5-8f53-e8da0bce2bfe";

        Assert.DoesNotThrowAsync(async () => await this._criticService.GetCriticDetailsByIdAsync(criticId));
    }

    [Test]
    public async Task GetCriticForEditByIdAsyncShouldGetDetails()
    {
        var criticId = "93372a0a-b9f4-4bc5-8f53-e8da0bce2bfe";

        Assert.DoesNotThrowAsync(async () => await this._criticService.GetCriticForEditByIdAsync(criticId));
    }

    [Test]
    public async Task DeleteCriticAsyncShouldRemoveCritic()
    {
        var criticId = "93372a0a-b9f4-4bc5-8f53-e8da0bce2bfe";

        Assert.DoesNotThrowAsync(async () => await this._criticService.DeleteCriticAsync(criticId));
    }

    [Test]
    public async Task EditCriticByIdAndModelAsyncShouldEditCritic()
    {
        var criticId = "93372a0a-b9f4-4bc5-8f53-e8da0bce2bfe";

        var model = await this._criticService.GetCriticForEditByIdAsync(criticId);

        Assert.DoesNotThrowAsync(async () => await this._criticService.EditCriticByIdAndModelAsync(criticId,model));
    }
}
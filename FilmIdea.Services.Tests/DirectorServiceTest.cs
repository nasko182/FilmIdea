namespace FilmIdea.Services.Tests;

using Microsoft.EntityFrameworkCore;

using Data;
using FilmIdea.Data;
using Data.Interfaces;
using Web.ViewModels.Director;

[TestFixture]
public class DirectorServiceTest
{
    private DbContextOptions<FilmIdeaDbContext> _dbOptions;
    private FilmIdeaDbContext _dbContext;

    private IMovieService _movieService;
    private IDirectorService _directorService;

    [SetUp]
    public void SetUp()
    {
        this._dbOptions = new DbContextOptionsBuilder<FilmIdeaDbContext>()
            .UseInMemoryDatabase("FilmIdeaInMemory" + Guid.NewGuid())
            .Options;

        this._dbContext = new FilmIdeaDbContext(this._dbOptions);

         this._dbContext.Database.EnsureCreated();

         this._movieService = new MovieService(this._dbContext);

        this._directorService = new DirectorService(this._dbContext,this._movieService); 
    }

    [TearDown]
    public void TearDown()
    {
        this._dbContext.Database.EnsureDeleted();
    }

    [Test]
    public async Task GetDirectorDetailsAsyncShouldReturnDirectorWithoutMovieUserRatingsIfDirectorIdIsValidAndUserIdIsNull()
    {
        var directorId = this._dbContext.Directors.First().Id;

        string? userId = null;

        var result = await this._directorService.GetDirectorDetailsAsync(directorId,userId);

        Assert.That(result!.Movies.First().UserRating,Is.Null);
    }

    [Test]
    public async Task GetDirectorDetailsAsyncShouldReturnDirectorWithoutMovieUserRatingsIfDirectorIdIsValidAndUserIdIsInvalid()
    {
        var directorId = this._dbContext.Directors.First().Id;

        string userId = Guid.NewGuid().ToString();

        var result = await this._directorService.GetDirectorDetailsAsync(directorId, userId);

        Assert.That(result!.Movies.First().UserRating, Is.EqualTo(0));
    }

    [Test]
    public async Task GetDirectorDetailsAsyncShouldReturnDirectorWithMovieUserRatingsIfDirectorIdIsValidAndUserIdIsValidToo()
    {
        var directorId = this._dbContext.Directors.First().Id;

        var userId = this._dbContext
            .ApplicationUsers
            .First(u => u.CriticId != null)
            .Id.ToString();

        var result = await this._directorService.GetDirectorDetailsAsync(directorId, userId);

        Assert.That(result!.Movies.First().UserRating, Is.EqualTo(0));
    }

    [Test]
    public async Task GetDirectorDetailsAsyncShouldReturnNullIfDirectorIdIsInvalidAndUserIdIsValid()
    {
        var directorId = 100;

        var userId = this._dbContext
            .ApplicationUsers
            .First(u => u.CriticId != null)
            .Id.ToString();

        var result = await this._directorService.GetDirectorDetailsAsync(directorId, userId);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetDirectorDetailsAsyncShouldReturnNullIfDirectorIdIsInvalidAndUserIdIsInvalid()
    {
        var directorId = 100;

        var userId = Guid.NewGuid().ToString();

        var result = await this._directorService.GetDirectorDetailsAsync(directorId, userId);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetDirectorDetailsAsyncShouldReturnNullIfDirectorIdIsInvalidAndUserIdIsNull()
    {
        var directorId = 100;

        string? userId = null;

        var result = await this._directorService.GetDirectorDetailsAsync(directorId, userId);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task CreateAsyncShouldCreateNewDirector()
    {
        var director = new AddDirectorViewModel
        {
            Name = "Tom Cruise",
            Bio = "Some Bio",
            DateOfBirth = DateTime.UtcNow,
        };

        var directorId = await this._directorService.CreateAsync(director, "someUrl");

        var result = this._dbContext.Directors.First(a => a.Id == directorId);

        Assert.IsNotNull(result);


    }

    [Test]
    public async Task CreateAsyncShouldReturnDirectorId()
    {
        var director = new AddDirectorViewModel
        {
            Name = "Tom Cruise",
            Bio = "Some Bio",
            DateOfBirth = DateTime.UtcNow,
        };

        var result = await this._directorService.CreateAsync(director, "someUrl");

        Assert.That(result, Is.EqualTo(1013));


    }

    [Test]
    public async Task GetDirectorForEditByIdAsyncShouldGetCorrectDirector()
    {
        var director = new EditDirectorViewModel
        {
            Name = "James Mangold",
            Bio =
                "James Mangold is an American film and television director, screenwriter and producer. Films he has directed include Луди години (1999), Да преминеш границата (2005), which he also co-wrote, the 2007 remake Ескорт до затвора (2007), Върколакът (2013), and Логан: Върколакът (2017).\r\n\r\nMangold also wrote and directed Копланд (1997), starring Sylvester Stallone, Robert De Niro, Harvey Keitel, and Ray Liotta.",
            DateOfBirth = new DateTime(1963, 12, 16),
            ProfileImageUrl =
                "https://dl.dropboxusercontent.com/s/491y3cpjoc4ulzm/MV5BNDI3MzgwMmYtY2JjYy00ZWQ2LTgzN_profile_image.jpg"
        };

        var result = await this._directorService.GetDirectorForEditByIdAsync(2);

        Assert.That(result.Name, Is.EqualTo(director.Name));
    }

    [Test]
    public void GetDirectorForEditByIdAsyncShouldThrowsErrorIfDirectorIdIsInvalid()
    {
        Assert.ThrowsAsync<InvalidOperationException>(async () => await this._directorService.GetDirectorForEditByIdAsync(6));
    }

    [Test]
    public async Task EditDirectorByIdAndModelAsyncShouldEditDirector()
    {
        var model = new EditDirectorViewModel
        {
            Name = "James Mangoldi",
            Bio =
                "James Mangold is an American film and television director, screenwriter and producer. Films he has directed include Луди години (1999), Да преминеш границата (2005), which he also co-wrote, the 2007 remake Ескорт до затвора (2007), Върколакът (2013), and Логан: Върколакът (2017).\r\n\r\nMangold also wrote and directed Копланд (1997), starring Sylvester Stallone, Robert De Niro, Harvey Keitel, and Ray Liotta.",
            DateOfBirth = new DateTime(1963, 12, 16),
            ProfileImageUrl =
                "https://dl.dropboxusercontent.com/s/491y3cpjoc4ulzm/MV5BNDI3MzgwMmYtY2JjYy00ZWQ2LTgzN_profile_image.jpg"
        };

        await this._directorService.EditDirectorByIdAndModelAsync(2, model);

        var result = await this._directorService.GetDirectorForEditByIdAsync(2);

        Assert.That(result.Name, Is.EqualTo(model.Name));
    }

    [Test]
    public void EditDirectorByIdAndModelAsyncShouldThrowsErrorIfDirectorIdIsInvalid()
    {
        var model = new EditDirectorViewModel
        {
            Name = "James Mangoldi",
            Bio =
                "James Mangold is an American film and television director, screenwriter and producer. Films he has directed include Луди години (1999), Да преминеш границата (2005), which he also co-wrote, the 2007 remake Ескорт до затвора (2007), Върколакът (2013), and Логан: Върколакът (2017).\r\n\r\nMangold also wrote and directed Копланд (1997), starring Sylvester Stallone, Robert De Niro, Harvey Keitel, and Ray Liotta.",
            DateOfBirth = new DateTime(1963, 12, 16),
            ProfileImageUrl =
                "https://dl.dropboxusercontent.com/s/491y3cpjoc4ulzm/MV5BNDI3MzgwMmYtY2JjYy00ZWQ2LTgzN_profile_image.jpg"
        };

        Assert.ThrowsAsync<InvalidOperationException>(async () => await this._directorService.EditDirectorByIdAndModelAsync(6, model));
    }

    [Test]
    public async Task DeleteDirectorByIdAsyncShouldDeleteDirector()
    {

        await this._directorService.DeleteDirectorByIdAsync(2);

        var result = await this._dbContext.Directors.FirstOrDefaultAsync(a => a.Id == 2);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task AddPhotoShouldAddPhotoForActor()
    {
        var actorId = 1;
        var photoUrl = "https://example.com/photo.jpg";

        Assert.DoesNotThrowAsync(async () => await this._directorService.AddPhoto(actorId, photoUrl));
    }

    [Test]
    public async Task AddVideoShouldAddVideoForActor()
    {
        var actorId = 1;
        var videoUrl = "https://example.com/video.mp4";

        Assert.DoesNotThrowAsync(async () => await this._directorService.AddVideo(actorId, videoUrl));
    }
}
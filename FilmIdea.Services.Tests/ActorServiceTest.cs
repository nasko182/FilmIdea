namespace FilmIdea.Services.Tests;

using Microsoft.EntityFrameworkCore;

using Data;
using FilmIdea.Data;
using Data.Interfaces;
using Web.ViewModels.Actor;

[TestFixture]
public class ActorServiceTest
{
    private DbContextOptions<FilmIdeaDbContext> _dbOptions;
    private FilmIdeaDbContext _dbContext;

    private IActorService _actorService;

    [SetUp]
    public void SetUp()
    {
        this._dbOptions = new DbContextOptionsBuilder<FilmIdeaDbContext>()
            .UseInMemoryDatabase("FilmIdeaInMemory" + Guid.NewGuid())
            .Options;

        this._dbContext = new FilmIdeaDbContext(this._dbOptions);

         this._dbContext.Database.EnsureCreated();

        this._actorService = new ActorService(this._dbContext); 
    }

    [TearDown]
    public void TearDown()
    {
        this._dbContext.Database.EnsureDeleted();
    }

    [Test]
    public async Task GetActorDetailsAsyncShouldReturnActorWithoutMovieUserRatingsIfActorIdIsValidAndUserIdIsNull()
    {
        var actorId = this._dbContext.Actors.First().Id;

        string? userId = null;

        var result = await this._actorService.GetActorDetailsAsync(actorId,userId);

        Assert.That(result!.Movies.First().UserRating,Is.Null);
    }

    [Test]
    public async Task GetActorDetailsAsyncShouldReturnActorWithoutMovieUserRatingsIfActorIdIsValidAndUserIdIsInvalid()
    {
        var actorId = this._dbContext.Actors.First().Id;

        string userId = Guid.NewGuid().ToString();

        var result = await this._actorService.GetActorDetailsAsync(actorId, userId);

        Assert.That(result!.Movies.First().UserRating, Is.EqualTo(0));
    }

    [Test]
    public async Task GetActorDetailsAsyncShouldReturnActorWithMovieUserRatingsIfActorIdIsValidAndUserIdIsValidToo()
    {
        var actorId = this._dbContext.Actors.First().Id;

        var userId = this._dbContext
            .ApplicationUsers
            .First(u => u.CriticId != null)
            .Id.ToString();

        var result = await this._actorService.GetActorDetailsAsync(actorId, userId);

        Assert.That(result!.Movies.First().UserRating, Is.EqualTo(0));
    }

    [Test]
    public async Task GetActorDetailsAsyncShouldReturnNullIfActorIdIsInvalidAndUserIdIsValid()
    {
        var actorId = 100;

        var userId = this._dbContext
            .ApplicationUsers
            .First(u => u.CriticId != null)
            .Id.ToString();

        var result = await this._actorService.GetActorDetailsAsync(actorId, userId);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetActorDetailsAsyncShouldReturnNullIfActorIdIsInvalidAndUserIdIsInvalid()
    {
        var actorId = 100;

        var userId = Guid.NewGuid().ToString();

        var result = await this._actorService.GetActorDetailsAsync(actorId, userId);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetActorDetailsAsyncShouldReturnNullIfActorIdIsInvalidAndUserIdIsNull()
    {
        var actorId = 100;

        string? userId = null;

        var result = await this._actorService.GetActorDetailsAsync(actorId, userId);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task CreateAsyncShouldCreateNewActor()
    {
        var actor = new AddActorViewModel
        {
            Name = "Tom Cruise",
            Bio = "Some Bio",
            DateOfBirth = DateTime.UtcNow,
        };

        var actorId = await this._actorService.CreateAsync(actor, "someUrl");

        var result = this._dbContext.Actors.First(a => a.Id == actorId);

        Assert.IsNotNull(result);


    }

    [Test]
    public async Task CreateAsyncShouldReturnActorId()
    {
        var actor = new AddActorViewModel
        {
            Name = "Tom Cruise",
            Bio = "Some Bio",
            DateOfBirth = DateTime.UtcNow,
        };

        var result = await this._actorService.CreateAsync(actor, "someUrl");

        Assert.That(result,Is.EqualTo(1020));


    }

    [Test]
    public async Task GetActorForEditByIdAsyncShouldGetCorrectActor()
    {
        var actor = new EditActorViewModel
        {
            Name = "Harrison Ford",
            Bio = "Harrison Ford was born on July 13, 1942 in Chicago, Illinois, to Dorothy (Nidelman), a radio actress, and Christopher Ford (born John William Ford), an actor turned advertising executive. His father was of Irish and German ancestry, while his maternal grandparents were Jewish emigrants from Minsk, Belarus. Harrison was a lackluster student at Maine Township High School East in Park Ridge Illinois (no athletic star, never above a C average). After dropping out of Ripon College in Wisconsin, where he did some acting and later summer stock, he signed a Hollywood contract with Columbia and later Universal. His roles in movies and television (Ironside (1967), The Virginian (1962)) remained secondary and, discouraged, he turned to a career in professional carpentry. He came back big four years later, however, as Bob Falfa in Американски графити (1973). Four years after that, he hit colossal with the role of Han Solo in Междузвездни войни: Епизод IV - Нова Надежда (1977). Another four years and Ford was Indiana Jones in Похитители на изчезналия кивот (1981).\r\n\r\nFour years later and he received Academy Award and Golden Globe nominations for his role as John Book in Свидетел (1985). All he managed four years after that was his third starring success as Indiana Jones; in fact, many of his earlier successful roles led to sequels as did his more recent portrayal of Jack Ryan in Патриотични игри (1992). Another Golden Globe nomination came his way for the part of Dr. Richard Kimble in Беглецът (1993). He is clearly a well-established Hollywood superstar. He also maintains an 800-acre ranch in Jackson Hole, Wyoming.\r\n\r\nFord is a private pilot of both fixed-wing aircraft and helicopters, and owns an 800-acre (3.2 km2) ranch in Jackson, Wyoming, approximately half of which he has donated as a nature reserve. On several occasions, Ford has personally provided emergency helicopter services at the request of local authorities, in one instance rescuing a hiker overcome by dehydration. Ford began flight training in the 1960s at Wild Rose Idlewild Airport in Wild Rose, Wisconsin, flying in a Piper PA-22 Tri-Pacer, but at $15 an hour, he could not afford to continue the training. In the mid-1990s, he bought a used Gulfstream II and asked one of his pilots, Terry Bender, to give him flying lessons. They started flying a Cessna 182 out of Jackson, Wyoming, later switching to Teterboro, New Jersey, flying a Cessna 206, the aircraft he soloed in. Ford is an honorary board member of the humanitarian aviation organization Wings of Hope.\r\n\r\nOn March 5, 2015, Ford's plane, believed to be a Ryan PT-22 Recruit, made an emergency landing on the Penmar Golf Course in Venice, California. Ford had radioed in to report that the plane had suffered engine failure. He was taken to Ronald Reagan UCLA Medical Center, where he was reported to be in fair to moderate condition. Ford suffered a broken pelvis and broken ankle during the accident, as well as other injuries.",
            DateOfBirth = new DateTime(1942, 07, 13),
            ProfileImageUrl = "https://dl.dropboxusercontent.com/s/aync8kuoks5ii3f/Something_profile_image.jpg",
        };

        var result = await this._actorService.GetActorForEditByIdAsync(2);

        Assert.That(result.Name, Is.EqualTo(actor.Name));
    }

    [Test]
    public void GetActorForEditByIdAsyncShouldThrowsErrorIfActorIdIsInvalid()
    {
        var model = new EditActorViewModel
        {
            Name = "Harrison Bord",
            Bio = "Harrison Ford was born on July 13, 1942 in Chicago, Illinois, to Dorothy (Nidelman), a radio actress, and Christopher Ford (born John William Ford), an actor turned advertising executive. His father was of Irish and German ancestry, while his maternal grandparents were Jewish emigrants from Minsk, Belarus. Harrison was a lackluster student at Maine Township High School East in Park Ridge Illinois (no athletic star, never above a C average). After dropping out of Ripon College in Wisconsin, where he did some acting and later summer stock, he signed a Hollywood contract with Columbia and later Universal. His roles in movies and television (Ironside (1967), The Virginian (1962)) remained secondary and, discouraged, he turned to a career in professional carpentry. He came back big four years later, however, as Bob Falfa in Американски графити (1973). Four years after that, he hit colossal with the role of Han Solo in Междузвездни войни: Епизод IV - Нова Надежда (1977). Another four years and Ford was Indiana Jones in Похитители на изчезналия кивот (1981).\r\n\r\nFour years later and he received Academy Award and Golden Globe nominations for his role as John Book in Свидетел (1985). All he managed four years after that was his third starring success as Indiana Jones; in fact, many of his earlier successful roles led to sequels as did his more recent portrayal of Jack Ryan in Патриотични игри (1992). Another Golden Globe nomination came his way for the part of Dr. Richard Kimble in Беглецът (1993). He is clearly a well-established Hollywood superstar. He also maintains an 800-acre ranch in Jackson Hole, Wyoming.\r\n\r\nFord is a private pilot of both fixed-wing aircraft and helicopters, and owns an 800-acre (3.2 km2) ranch in Jackson, Wyoming, approximately half of which he has donated as a nature reserve. On several occasions, Ford has personally provided emergency helicopter services at the request of local authorities, in one instance rescuing a hiker overcome by dehydration. Ford began flight training in the 1960s at Wild Rose Idlewild Airport in Wild Rose, Wisconsin, flying in a Piper PA-22 Tri-Pacer, but at $15 an hour, he could not afford to continue the training. In the mid-1990s, he bought a used Gulfstream II and asked one of his pilots, Terry Bender, to give him flying lessons. They started flying a Cessna 182 out of Jackson, Wyoming, later switching to Teterboro, New Jersey, flying a Cessna 206, the aircraft he soloed in. Ford is an honorary board member of the humanitarian aviation organization Wings of Hope.\r\n\r\nOn March 5, 2015, Ford's plane, believed to be a Ryan PT-22 Recruit, made an emergency landing on the Penmar Golf Course in Venice, California. Ford had radioed in to report that the plane had suffered engine failure. He was taken to Ronald Reagan UCLA Medical Center, where he was reported to be in fair to moderate condition. Ford suffered a broken pelvis and broken ankle during the accident, as well as other injuries.",
            DateOfBirth = new DateTime(1942, 07, 13),
            ProfileImageUrl = "https://dl.dropboxusercontent.com/s/aync8kuoks5ii3f/Something_profile_image.jpg",
        };

        Assert.ThrowsAsync<InvalidOperationException>(async () => await this._actorService.GetActorForEditByIdAsync(6));
    }

    [Test]
    public async Task EditActorByIdAndModelAsyncShouldEditActor()
    {
        var model = new EditActorViewModel
        {
            Name = "Harrison Bord",
            Bio = "Harrison Ford was born on July 13, 1942 in Chicago, Illinois, to Dorothy (Nidelman), a radio actress, and Christopher Ford (born John William Ford), an actor turned advertising executive. His father was of Irish and German ancestry, while his maternal grandparents were Jewish emigrants from Minsk, Belarus. Harrison was a lackluster student at Maine Township High School East in Park Ridge Illinois (no athletic star, never above a C average). After dropping out of Ripon College in Wisconsin, where he did some acting and later summer stock, he signed a Hollywood contract with Columbia and later Universal. His roles in movies and television (Ironside (1967), The Virginian (1962)) remained secondary and, discouraged, he turned to a career in professional carpentry. He came back big four years later, however, as Bob Falfa in Американски графити (1973). Four years after that, he hit colossal with the role of Han Solo in Междузвездни войни: Епизод IV - Нова Надежда (1977). Another four years and Ford was Indiana Jones in Похитители на изчезналия кивот (1981).\r\n\r\nFour years later and he received Academy Award and Golden Globe nominations for his role as John Book in Свидетел (1985). All he managed four years after that was his third starring success as Indiana Jones; in fact, many of his earlier successful roles led to sequels as did his more recent portrayal of Jack Ryan in Патриотични игри (1992). Another Golden Globe nomination came his way for the part of Dr. Richard Kimble in Беглецът (1993). He is clearly a well-established Hollywood superstar. He also maintains an 800-acre ranch in Jackson Hole, Wyoming.\r\n\r\nFord is a private pilot of both fixed-wing aircraft and helicopters, and owns an 800-acre (3.2 km2) ranch in Jackson, Wyoming, approximately half of which he has donated as a nature reserve. On several occasions, Ford has personally provided emergency helicopter services at the request of local authorities, in one instance rescuing a hiker overcome by dehydration. Ford began flight training in the 1960s at Wild Rose Idlewild Airport in Wild Rose, Wisconsin, flying in a Piper PA-22 Tri-Pacer, but at $15 an hour, he could not afford to continue the training. In the mid-1990s, he bought a used Gulfstream II and asked one of his pilots, Terry Bender, to give him flying lessons. They started flying a Cessna 182 out of Jackson, Wyoming, later switching to Teterboro, New Jersey, flying a Cessna 206, the aircraft he soloed in. Ford is an honorary board member of the humanitarian aviation organization Wings of Hope.\r\n\r\nOn March 5, 2015, Ford's plane, believed to be a Ryan PT-22 Recruit, made an emergency landing on the Penmar Golf Course in Venice, California. Ford had radioed in to report that the plane had suffered engine failure. He was taken to Ronald Reagan UCLA Medical Center, where he was reported to be in fair to moderate condition. Ford suffered a broken pelvis and broken ankle during the accident, as well as other injuries.",
            DateOfBirth = new DateTime(1942, 07, 13),
            ProfileImageUrl = "https://dl.dropboxusercontent.com/s/aync8kuoks5ii3f/Something_profile_image.jpg",
        };

        await this._actorService.EditActorByIdAndModelAsync(2, model);

        var result = await this._actorService.GetActorForEditByIdAsync(2);

        Assert.That(result.Name, Is.EqualTo(model.Name));
    }

    [Test]
    public void EditActorByIdAndModelAsyncShouldThrowsErrorIfActorIdIsInvalid()
    {
        var model = new EditActorViewModel
        {
            Name = "Harrison Bord",
            Bio = "Harrison Ford was born on July 13, 1942 in Chicago, Illinois, to Dorothy (Nidelman), a radio actress, and Christopher Ford (born John William Ford), an actor turned advertising executive. His father was of Irish and German ancestry, while his maternal grandparents were Jewish emigrants from Minsk, Belarus. Harrison was a lackluster student at Maine Township High School East in Park Ridge Illinois (no athletic star, never above a C average). After dropping out of Ripon College in Wisconsin, where he did some acting and later summer stock, he signed a Hollywood contract with Columbia and later Universal. His roles in movies and television (Ironside (1967), The Virginian (1962)) remained secondary and, discouraged, he turned to a career in professional carpentry. He came back big four years later, however, as Bob Falfa in Американски графити (1973). Four years after that, he hit colossal with the role of Han Solo in Междузвездни войни: Епизод IV - Нова Надежда (1977). Another four years and Ford was Indiana Jones in Похитители на изчезналия кивот (1981).\r\n\r\nFour years later and he received Academy Award and Golden Globe nominations for his role as John Book in Свидетел (1985). All he managed four years after that was his third starring success as Indiana Jones; in fact, many of his earlier successful roles led to sequels as did his more recent portrayal of Jack Ryan in Патриотични игри (1992). Another Golden Globe nomination came his way for the part of Dr. Richard Kimble in Беглецът (1993). He is clearly a well-established Hollywood superstar. He also maintains an 800-acre ranch in Jackson Hole, Wyoming.\r\n\r\nFord is a private pilot of both fixed-wing aircraft and helicopters, and owns an 800-acre (3.2 km2) ranch in Jackson, Wyoming, approximately half of which he has donated as a nature reserve. On several occasions, Ford has personally provided emergency helicopter services at the request of local authorities, in one instance rescuing a hiker overcome by dehydration. Ford began flight training in the 1960s at Wild Rose Idlewild Airport in Wild Rose, Wisconsin, flying in a Piper PA-22 Tri-Pacer, but at $15 an hour, he could not afford to continue the training. In the mid-1990s, he bought a used Gulfstream II and asked one of his pilots, Terry Bender, to give him flying lessons. They started flying a Cessna 182 out of Jackson, Wyoming, later switching to Teterboro, New Jersey, flying a Cessna 206, the aircraft he soloed in. Ford is an honorary board member of the humanitarian aviation organization Wings of Hope.\r\n\r\nOn March 5, 2015, Ford's plane, believed to be a Ryan PT-22 Recruit, made an emergency landing on the Penmar Golf Course in Venice, California. Ford had radioed in to report that the plane had suffered engine failure. He was taken to Ronald Reagan UCLA Medical Center, where he was reported to be in fair to moderate condition. Ford suffered a broken pelvis and broken ankle during the accident, as well as other injuries.",
            DateOfBirth = new DateTime(1942, 07, 13),
            ProfileImageUrl = "https://dl.dropboxusercontent.com/s/aync8kuoks5ii3f/Something_profile_image.jpg",
        };

        Assert.ThrowsAsync<InvalidOperationException>(async ()=> await this._actorService.EditActorByIdAndModelAsync(6, model));
    }

    [Test]
    public async Task DeleteActorByIdAsyncShouldDeleteActor()
    {

        await this._actorService.DeleteActorByIdAsync(2);

        var result = await this._dbContext.Actors.FirstOrDefaultAsync(a => a.Id == 2);

        Assert.That(result, Is.Null);
    }
}
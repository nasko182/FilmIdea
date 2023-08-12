namespace FilmIdea.Services.Tests;

using Microsoft.EntityFrameworkCore;

using Data;
using FilmIdea.Data;
using Data.Interfaces;
using FilmIdea.Web.ViewModels.Group;

[TestFixture]
public class GroupServiceTest
{
    private DbContextOptions<FilmIdeaDbContext> _dbOptions;
    private FilmIdeaDbContext _dbContext;

    private IGroupService _groupService;

    [SetUp]
    public void SetUp()
    {
        this._dbOptions = new DbContextOptionsBuilder<FilmIdeaDbContext>()
            .UseInMemoryDatabase("FilmIdeaInMemory" + Guid.NewGuid())
            .Options;

        this._dbContext = new FilmIdeaDbContext(this._dbOptions);

         this._dbContext.Database.EnsureCreated();

        this._groupService = new GroupService(this._dbContext); 
    }

    [TearDown]
    public void TearDown()
    {
        this._dbContext.Database.EnsureDeleted();
    }

    [Test]
    public async Task GetAllGroupsAsyncShouldReturnGroupsForUser()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233".ToLower();
        var criticId = "15EB7825-840B-4528-71CC-08DB8C333233".ToLower();


        var model = new AddGroupViewModel
        {
            Name = "Test Group",
            Icon = "🏠",
            UsersIds = new List<string> { criticId }
        };

        await this._groupService.CreateGroupAsync(model, userId);

        var result = await this._groupService.GetAllGroupsAsync(criticId);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.GreaterThan(0));
    }

    [Test]
    public async Task CreateGroupAsyncShouldCreateNewGroup()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";
        var criticId = "15EB7825-840B-4528-71CC-08DB8C333233";


        var model = new AddGroupViewModel
        {
            Name = "Test Group",
            Icon = "🏠",
            UsersIds = new List<string> { criticId } 
        };

        var result = await this._groupService.CreateGroupAsync(model, userId);

        Assert.IsNotNull(result);
    }

    [Test]
    public async Task CreateGroupModelAsyncShouldReturnViewModel()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";

        var result = await this._groupService.CreateGroupModelAsync(userId);

        Assert.IsNotNull(result);
    }

    [Test]
    public async Task EditGroupAsyncShouldEditGroup()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";
        var criticId = "15EB7825-840B-4528-71CC-08DB8C333233";

        var groupId = Guid.NewGuid().ToString();

        var model = new EditGroupViewModel
        {
            Name = "Updated Group Name",
            Icon = "🔥",
            UsersIds = new List<string> { criticId }
        };

        Assert.DoesNotThrowAsync(async () => await this._groupService.EditGroupAsync(model, userId, groupId));
    }

    
    [Test]
    public async Task CreateEditGroupModelAsyncShouldReturnViewModel()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233";
        var criticId = "15EB7825-840B-4528-71CC-08DB8C333233";

        var groupId = Guid.NewGuid().ToString(); 

        var model = new EditGroupViewModel
        {
            Name = "Updated Group Name",
            Icon = "🔥",
            UsersIds = new List<string> { criticId } 
        };

        var result = await this._groupService.CreateEditGroupModelAsync(userId, model, groupId);

        Assert.IsNotNull(result);
    }

    [Test]
    public async Task GetGroupDetailsAsyncShouldReturnViewModel()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233".ToLower();
        var criticId = "15EB7825-840B-4528-71CC-08DB8C333233".ToLower();

        var model = new AddGroupViewModel
        {
            Name = "Test Group",
            Icon = "🏠",
            UsersIds = new List<string> { criticId }
        };

        var groupId = await this._groupService.CreateGroupAsync(model, userId);

        var result = this._groupService.GetGroupDetailsAsync(groupId,userId);

        Assert.IsNotNull(result);
    }

    [Test]
    public async Task LeaveGroupAsyncAsyncShouldRemoveUserFromGroup()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233".ToLower();
        var criticId = "15EB7825-840B-4528-71CC-08DB8C333233".ToLower();

        var model = new AddGroupViewModel
        {
            Name = "Test Group",
            Icon = "🏠",
            UsersIds = new List<string> { criticId }
        };

        var groupId = await this._groupService.CreateGroupAsync(model, userId);
        

        await this._groupService.LeaveGroupAsync(groupId, userId);

        var group = await this._groupService.GetGroupDetailsAsync(groupId, criticId);

        Assert.That(group.Users.Any(u=>u.Id.ToString()==userId),Is.False);
    }

    [Test]
    public async Task SendMessageAsyncShouldSendMessage()
    {
        var userId = "2532DDAA-63F0-4DE8-71CB-08DB8C333233".ToLower();
        var criticId = "15EB7825-840B-4528-71CC-08DB8C333233".ToLower();

        var model = new AddGroupViewModel
        {
            Name = "Test Group",
            Icon = "🏠",
            UsersIds = new List<string> { criticId }
        };

        var groupId = await this._groupService.CreateGroupAsync(model, userId);
        var content = "Hello";

        await this._groupService.SendMessageAsync(content, groupId, userId);
        var group = await this._groupService.GetGroupDetailsAsync(groupId, userId);

        Assert.That(group.Chat.Messages.Any(m => m.Content == content));
    }
}

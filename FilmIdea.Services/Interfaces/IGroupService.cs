namespace FilmIdea.Services.Data.Interfaces;

using Web.ViewModels.Group;

public interface IGroupService
{
    Task<List<AllGroupViewModel>> GetAllGroupsAsync();

    Task CreateGroupAsync(AddGroupViewModel model);

    Task<CreateGroupViewModel> CreateGroupModelAsync(string userId);
}

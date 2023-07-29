namespace FilmIdea.Services.Data.Interfaces;

using Web.ViewModels.Group;
using static Dropbox.Api.Team.GroupSelector;

public interface IGroupService
{
    Task<List<AllGroupViewModel>> GetAllGroupsAsync();

    Task CreateGroupAsync(AddGroupViewModel model);

    Task<CreateGroupViewModel> CreateGroupModelAsync(string userId);

    Task<DetailsGroupModel> GetGroupDetailsAsync(string groupId);
}

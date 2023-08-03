namespace FilmIdea.Services.Data.Interfaces;

using Web.ViewModels.Group;

public interface IGroupService 
{
    Task<List<AllGroupViewModel>> GetAllGroupsAsync();

    Task<int> CreateGroupAsync(AddGroupViewModel model,string userId);

    Task<CreateGroupViewModel> CreateGroupModelAsync(string userId);

    Task EditGroupAsync(EditGroupViewModel model,string userId,string groupId);

    Task<CreateEditGroupViewModel> CreateEditGroupModelAsync(string userId,EditGroupViewModel model,string groupId);

    Task<DetailsGroupModel> GetGroupDetailsAsync(string groupId,string userId);

    Task SendMessageAsync(string content,string groupId, string userId);

    Task LeaveGroupAsync(string groupId, string userId);
}

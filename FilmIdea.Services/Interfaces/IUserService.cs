namespace FilmIdea.Services.Data.Interfaces;

using Web.ViewModels.User;

public interface IUserService
{
    Task<string> GetUserNameByIdAsync(string userId);

    Task<IEnumerable<FullUserViewModel>> AllAsync();

}

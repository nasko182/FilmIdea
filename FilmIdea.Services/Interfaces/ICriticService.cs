namespace FilmIdea.Services.Data.Interfaces;

using Microsoft.AspNetCore.Http;
using Web.ViewModels.Critic;

public interface ICriticService
{
    Task<bool> CriticExistByUserIdAsync(string userId);

    Task<string> UploadPhotoAsync(IFormFile imageFile, string userName);

    Task CreateCriticAsync(string userId, BecomeCriticViewModel model,string photoUrl);

}

namespace FilmIdea.Services.Data.Interfaces;

using FilmIdea.Data.Models;
using Web.ViewModels.Critic;
using static Dropbox.Api.Files.ListRevisionsMode;

public interface ICriticService 
{
    Task<bool> CriticExistByUserIdAsync(string? userId);

    Task<string?> GetCriticIdAsync(string? userid);

    Task CreateCriticAsync(string userId, BecomeCriticViewModel model,string photoUrl);

    Task<string?> GetCriticName(string userId);

    Task<CriticDetailsViewModel> GetCriticDetailsByIdAsync(string criticId);

    Task<EditCriticViewModel> GetCriticForEditByIdAsync(string id);

    Task EditCriticByIdAndModelAsync(string criticId,EditCriticViewModel model);
}

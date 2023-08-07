namespace FilmIdea.Services.Data.Interfaces;

using Web.ViewModels.Critic;

public interface ICriticService 
{
    Task<bool> CriticExistByUserIdAsync(string? userId);

    Task<string?> GetCriticIdAsync(string userid);

    Task CreateCriticAsync(string userId, BecomeCriticViewModel model,string photoUrl);
}

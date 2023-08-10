namespace FilmIdea.Services.Data.Interfaces;

using Web.ViewModels.Director;

public interface IDirectorService 
{
    Task<DirectorDetailsViewModel?> GetDirectorDetailsAsync(int directorId,string? userId);

    Task<int> CreateAsync(AddDirectorViewModel model, string photoUrl);

    Task<EditDirectorViewModel> GetDirectorForEditByIdAsync(int id);

    Task EditDirectorByIdAndModelAsync(int id, EditDirectorViewModel model);

    Task DeleteDirectorByIdAsync(int id);

    Task AddPhoto(int directorId, string photoUrl);

    Task AddVideo(int directorId, string videoUrl);
}

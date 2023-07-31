namespace FilmIdea.Services.Data.Interfaces;

using Web.ViewModels.Director;

public interface IDirectorService 
{
    Task<DirectorDetailsViewModel?> GetDirectorDetailsAsync(int directorId,string? userId);
}

namespace FilmIdea.Services.Data.Interfaces;

using FilmIdea.Data.Models;
using Web.ViewModels.Actor;

public interface IActorService 
{
    Task<ActorDetailsViewModel?> GetActorDetailsAsync(int actorId,string? userId);

    Task<int> CreateAsync(AddActorViewModel model, string photoUrl);

    Task<EditActorViewModel> GetActorForEditByIdAsync(int id);

    Task EditActorByIdAndModelAsync(int id,EditActorViewModel model);

    Task DeleteActorByIdAsync(int id);

    Task<ICollection<EditMovieActors>> GetAllActorsAsync(int movieId);

    Task EditMovieActors(List<int> actorsIds,int movieId);

    Task AddPhoto(int actorId,string photoUrl);

    Task AddVideo(int actorId,string videoUrl);
}

namespace FilmIdea.Services.Data.Interfaces;

using Web.ViewModels.Actor;

public interface IActorService 
{
    Task<ActorDetailsViewModel?> GetActorDetailsAsync(int actorId,string? userId);

    Task<int> CreateAsync(AddActorViewModel model, string photoUrl);

    Task<EditActorViewModel> GetActorForEditByIdAsync(int id);

    Task EditActorByIdAndModelAsync(int id,EditActorViewModel model);

    Task DeleteActorByIdAsync(int id);

    Task<ICollection<EditMovieActors>> GetAllActorsAsync(int movieId);

    Task EditMovieActorsAsync(List<int> actorsIds,int movieId);

    Task AddPhotoAsync(int actorId,string photoUrl);

    Task AddVideoAsync(int actorId,string videoUrl);
}

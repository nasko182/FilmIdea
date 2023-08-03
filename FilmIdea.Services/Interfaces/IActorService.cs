namespace FilmIdea.Services.Data.Interfaces;

using Web.ViewModels.Actor;

public interface IActorService 
{
    Task<ActorDetailsViewModel?> GetActorDetailsAsync(int actorId,string? userId);
}

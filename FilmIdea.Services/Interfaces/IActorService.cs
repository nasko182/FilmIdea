namespace FilmIdea.Services.Data.Interfaces;

using FilmIdea.Web.ViewModels.Director;
using Web.ViewModels.Actor;

public interface IActorService 
{
    Task<ActorDetailsViewModel?> GetActorDetailsAsync(int actorId,string? userId);
}

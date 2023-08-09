namespace FilmIdea.Web.ViewModels.Actor;

using Interfaces;

public class ActorNameAndIdViewModel : IActorDetailsModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}

namespace FilmIdea.Web.ViewModels.Actor;

using Movie;

public class ActorDetailsViewModel 
{
    public ActorDetailsViewModel()
    {
        this.Movies = new HashSet<MovieViewModel>();
        this.Photos = new HashSet<string>();
        this.Videos = new HashSet<string>();
    }
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Bio { get; set; } = null!;

    public string ProfileImageUrl { get; set; } = null!;

    public string DateOfBirth { get; set; } = null!;

    public ICollection<MovieViewModel> Movies { get; set; }

    public ICollection<string> Videos { get; set; }

    public ICollection<string> Photos { get; set; }
}
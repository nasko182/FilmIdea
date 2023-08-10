namespace FilmIdea.Web.ViewModels.Actor;

public class EditMovieActors
{
    public int Id { get; set;}

    public string Name { get; set; } = null!;

    public bool IsInMovie { get; set; }

    public int MovieId { get; set; }
}

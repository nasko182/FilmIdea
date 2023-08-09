namespace FilmIdea.Web.ViewModels.Director;

using Interfaces;

public class DirectorNameAndIdViewModel : IDirectorDetailsModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}

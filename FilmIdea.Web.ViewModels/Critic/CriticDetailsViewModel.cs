namespace FilmIdea.Web.ViewModels.Critic;

using Review;

public class CriticDetailsViewModel
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Bio { get; set; } = null!;

    public string ProfileImageUrl { get; set; } = null!;

    public string DateOfBirth { get; set; } = null!;
}

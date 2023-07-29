namespace FilmIdea.Web.ViewModels.Group;

public class MessageViewModel
{
    public string Content { get; set; } = null!;

    public string Sender { get; set; } = null!;

    public DateTime SendAt { get; set; }
}

namespace FilmIdea.Web.ViewModels.Message;

public class MessageViewModel
{
    public string Content { get; set; } = null!;

    public string SenderName { get; set; } = null!;

    public string SenderId = null!;

    public DateTime SendAt { get; set; }
}

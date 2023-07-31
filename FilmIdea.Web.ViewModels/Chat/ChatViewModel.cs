namespace FilmIdea.Web.ViewModels.Chat;

using Message;

public class ChatViewModel
{
    public ChatViewModel()
    {
        Messages = new HashSet<MessageViewModel>();
    }
    public DateTime CreatedAt { get; set; }

    public ICollection<MessageViewModel> Messages { get; set; }
}

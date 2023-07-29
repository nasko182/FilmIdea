namespace FilmIdea.Web.ViewModels.Group;

public class ChatViewModel
{
    public ChatViewModel()
    {
        this.Messages = new HashSet<MessageViewModel>();
    }
    public DateTime CreatedAt { get; set; }

    public ICollection<MessageViewModel> Messages { get; set; }
}

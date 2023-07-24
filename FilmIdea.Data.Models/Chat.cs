namespace FilmIdea.Data.Models;

public class Chat
{
    public Chat()
    {
        this.Id = Guid.NewGuid();

        this.Messages = new HashSet<Message>();
    }

    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid GroupId { get; set; }

    public Group Group { get; set; } = null!;

    public ICollection<Message> Messages { get; set; }
}

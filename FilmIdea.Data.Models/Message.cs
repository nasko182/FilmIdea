namespace FilmIdea.Data.Models;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.MessageValidations;

public class Message
{
    public Message()
    {
        this.Id = Guid.NewGuid();
        this.SentAt = DateTime.UtcNow;
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(ContentMaxLength)]
    public string Content { get; set; } = null!;

    [Required]
    public DateTime SentAt { get; set; }

    public Guid ChatId { get; set; }

    public Chat Chat { get; set; } = null!;

    public Guid SenderId { get; set; }

    public ApplicationUser Sender { get; set; } = null!;
}
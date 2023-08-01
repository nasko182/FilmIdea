namespace FilmIdea.Data.Models;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.ReviewValidations;

public class Review
{
    public Review()
    {
        this.Id = Guid.NewGuid();
        this.ReviewDate = DateTime.UtcNow;

        this.Comments = new HashSet<Comment>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    public int Rating { get; set; }

    [Required]
    [MaxLength(TitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    public DateTime ReviewDate { get; set; }

    [Required]
    [MaxLength(ContentMaxLength)]
    public string Content { get; set; } = null!;

    public int MovieId { get; set; }

    public Movie Movie { get; set; } = null!;

    public Guid CriticId { get; set; }

    public Critic Critic { get; set; } = null!;

    public ICollection<Comment> Comments { get; set; }
}
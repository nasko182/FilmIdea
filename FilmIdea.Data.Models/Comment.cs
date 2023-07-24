namespace FilmIdea.Data.Models;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.CommentValidations;

public class Comment
{
    public Comment()
    {
        this.Id = Guid.NewGuid();
        this.CommentDate = DateTime.UtcNow;
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(ContentMaxLength)]
    public string Content { get; set; } = null!;

    [Required]
    public DateTime CommentDate { get; set; }

    public Guid WriterId { get; set; }

    public ApplicationUser Writer { get; set; } = null!;

    public Guid ReviewId { get; set; }

    public Review Review { get; set; } = null!;
}

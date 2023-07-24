namespace FilmIdea.Data.Models;

using System.ComponentModel.DataAnnotations;

public class UserRating
{
    public UserRating()
    {
        this.Id = Guid.NewGuid();
    }
    [Key]
    public Guid Id { get; set; }

    [Required]
    public int Rating { get; set; }

    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;

    public int MovieId { get; set; }

    public Movie Movie { get; set; } = null!;
}
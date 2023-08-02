namespace FilmIdea.Data.Models;

using System.ComponentModel.DataAnnotations;

public class Like
{
    public Like()
    {
        this.Id= Guid.NewGuid();
    }
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;

    public Guid ReviewId { get; set; }

    public Review Review { get; set; } = null!;

}


namespace FilmIdea.Data.Models.Join_Tables;

public class UserMovie
{
    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;

    public int MovieId { get; set; }

    public Movie Movie { get; set; } = null!;
}
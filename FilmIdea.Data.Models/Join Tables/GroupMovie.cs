namespace FilmIdea.Data.Models.Join_Tables;

public class GroupMovie
{
    public Guid GroupId { get; set; }

    public Group Group { get; set; } = null!;

    public int MovieId { get; set; }

    public Movie Movie { get; set; } = null!;
}
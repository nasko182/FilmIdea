namespace FilmIdea.Data.Models.Join_Tables;
public class GroupUser
{
    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; } = null!;

    public Guid GroupId { get; set; }

    public Group Group { get; set; } = null!;
}

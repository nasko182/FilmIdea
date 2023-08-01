namespace FilmIdea.Data.Models;

using System.ComponentModel.DataAnnotations;

using Join_Tables;

using static Common.EntityValidationConstants.GroupValidations;

public class Group
{
    public Group()
    {
        this.Id = Guid.NewGuid();
        this.Chat = new Chat();

        this.GroupUsers = new HashSet<GroupUser>();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    public string Icon { get; set; } = null!;

    public Guid ChatId { get; set; }

    public Chat Chat { get; set; }

    public ICollection<GroupUser> GroupUsers { get; set; }
    
}

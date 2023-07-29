namespace FilmIdea.Web.ViewModels.Group;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.GroupValidations;

public class AddGroupViewModel
{
    public AddGroupViewModel()
    {
        this.UsersIds = new HashSet<string>();
    }
    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

    public string Icon { get; set; } = null!;

    public ICollection<string> UsersIds { get; set; }
}

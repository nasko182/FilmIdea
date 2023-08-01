namespace FilmIdea.Web.ViewModels.Group;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.GroupValidations;

public class EditGroupViewModel
{
    public EditGroupViewModel()
    {
        this.UsersIds = new HashSet<string>();
    }

    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; } = null!;

    [Required]
    public string Icon { get; set; } = null!;

    [Required]
    public ICollection<string> UsersIds { get; set; }
}

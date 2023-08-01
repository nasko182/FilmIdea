namespace FilmIdea.Web.ViewModels.Group;

using User;

public class CreateEditGroupViewModel
{
    public CreateEditGroupViewModel()
    {
        this.AllUsers = new HashSet<UserViewModel>();
        this.Icons = new HashSet<IconViewModel>();
    }

    public string Id { get; set; } = null!;

    public EditGroupViewModel GroupData { get; set; } = null!;

    public ICollection<UserViewModel> AllUsers { get; set; }

    public ICollection<IconViewModel> Icons { get; set; }
}

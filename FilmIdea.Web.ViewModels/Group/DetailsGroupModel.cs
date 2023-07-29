using FilmIdea.Web.ViewModels.User;

namespace FilmIdea.Web.ViewModels.Group;

public class DetailsGroupModel
{
    public DetailsGroupModel()
    {
        this.Users = new HashSet<UserViewModel>();
        this.Watchlist = new HashSet<GroupMovieViewModel>();
    }
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Icon { get; set; } = null!;

    public ChatViewModel  Chat { get; set; } = null!;

    public ICollection<UserViewModel> Users { get; set; }

    public ICollection<GroupMovieViewModel> Watchlist { get; set; }
}

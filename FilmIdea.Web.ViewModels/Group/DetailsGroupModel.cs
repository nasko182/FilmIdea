using FilmIdea.Web.ViewModels.Chat;
using FilmIdea.Web.ViewModels.User;

namespace FilmIdea.Web.ViewModels.Group;

using Movie;

public class DetailsGroupModel
{
    public DetailsGroupModel()
    {
        this.Users = new HashSet<UserViewModel>();
        this.Watchlist = new HashSet<MovieViewModel>();
    }
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Icon { get; set; } = null!;

    public ChatViewModel  Chat { get; set; } = null!;

    public ICollection<UserViewModel> Users { get; set; }

    public ICollection<MovieViewModel> Watchlist { get; set; }
}

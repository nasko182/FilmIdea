namespace FilmIdea.Web.ViewModels.Movie;

using Actor;
using Director;

public class WatchlistMovieViewModel
{
    public WatchlistMovieViewModel()
    {
        this.Actors = new HashSet<ActorNameAndIdViewModel>();
    }

    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string CoverPhotoUrl { get; set; } = null!;

    public int ReleaseYear { get; set; }

    public int? UserRating { get; set; }

    public int Duration { get; set; }

    public DirectorNameAndIdViewModel Director { get; set; } = null!;

    public decimal Rating { get; set; }

    public bool HasMovieInWatchlist { get; set; }

    public ICollection<ActorNameAndIdViewModel> Actors { get; set; }
}
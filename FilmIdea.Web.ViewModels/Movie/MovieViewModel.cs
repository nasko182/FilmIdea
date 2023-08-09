 namespace FilmIdea.Web.ViewModels.Movie;

 using Actor.Interfaces;
 using Interfaces;

public class MovieViewModel : IMovieDetailsModel
{
    public MovieViewModel()
    {
        this.Movies = new HashSet<TopSectionMovieViewModel>();
    }

    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string CoverPhotoUrl { get; set; } = null!;

    public int ReleaseYear { get; set; }

    public int? UserRating { get; set; }

    public int Duration { get; set; }

    public decimal Rating { get; set; }

    public bool HasMovieInWatchlist { get; set; }

    public IEnumerable<TopSectionMovieViewModel> Movies { get; set; }
}
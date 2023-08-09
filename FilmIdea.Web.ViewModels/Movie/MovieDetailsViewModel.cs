namespace FilmIdea.Web.ViewModels.Movie;

using Actor;
using Director;
using Genre;
using Review;

public class MovieDetailsViewModel 
{
    public MovieDetailsViewModel()
    {
        this.Actors = new HashSet<ActorNameAndIdViewModel>();
        this.Genres = new HashSet<GenreViewModel>();
        this.Reviews = new HashSet<ReviewViewModel>();
        this.Photos = new HashSet<string>();
        this.Videos = new HashSet<string>();
    }
    public int  Id { get; set; }

    public string Title { get; set; } = null!;

    public string CoverImageUrl { get; set; } = null!;

    public int ReleaseYear { get; set; }

    public string Description { get; set; } = null!;
 
    public int Duration { get; set; }

    public decimal Rating { get; set; }

    public int UserRating { get; set; }

    public string TrailerUrl { get; set; } = null!;

    public DirectorNameAndIdViewModel Director { get; set; } = null!;

    public ICollection<ActorNameAndIdViewModel> Actors { get; set; }

    public ICollection<GenreViewModel> Genres { get; set; }

    public ICollection<ReviewViewModel> Reviews { get; set; }

    public ICollection<string> Photos { get; set; }

    public ICollection<string> Videos { get; set; }
}
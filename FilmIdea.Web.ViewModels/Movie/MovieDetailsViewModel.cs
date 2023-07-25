namespace FilmIdea.Web.ViewModels.Movie;

using Actor;
using Data.Models;
using Director;

public class MovieDetailsViewModel
{
    public MovieDetailsViewModel()
    {
        this.Actors = new HashSet<ActorNameAndIdViewModel>();
        this.Genres = new HashSet<Genre>();
        this.Reviews = new HashSet<Review>();
        this.Photos = new HashSet<Photo>();
        this.Videos = new HashSet<Video>();
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

    public ICollection<Genre> Genres { get; set; }

    public ICollection<Review> Reviews { get; set; }

    public ICollection<Photo> Photos { get; set; }

    public ICollection<Video> Videos { get; set; }
}
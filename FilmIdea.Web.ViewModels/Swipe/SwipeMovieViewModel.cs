namespace FilmIdea.Web.ViewModels.Swipe;

using Genre;

public class SwipeMovieViewModel
{
    public SwipeMovieViewModel()
    {
        this.Genres = new HashSet<GenreViewModel>();
    }
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int ReleaseYear { get; set; } 
    public int Duration { get; set; }
    public double? Rating { get; set; }
    public string CoverImageUrl { get; set; } = null!;
    public string TrailerUrl { get; set;} = null!;
    public ICollection<GenreViewModel> Genres { get; set; }

}

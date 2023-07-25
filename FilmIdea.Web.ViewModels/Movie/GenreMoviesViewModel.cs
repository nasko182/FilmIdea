namespace FilmIdea.Web.ViewModels.Movie;
public class GenreMoviesViewModel
{
    public GenreMoviesViewModel()
    {
        this.TopMovies = new HashSet<TopSectionMovieViewModel>();
        this.Movies = new HashSet<MovieViewModel>();
    }
    public IEnumerable<TopSectionMovieViewModel> TopMovies { get; set; }

    public IEnumerable<MovieViewModel> Movies { get; set; }
}

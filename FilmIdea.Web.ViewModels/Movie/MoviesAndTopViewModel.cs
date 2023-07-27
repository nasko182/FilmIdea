namespace FilmIdea.Web.ViewModels.Movie;
public class MoviesAndTopViewModel
{
    public MoviesAndTopViewModel()
    {
        this.TopMovies = new HashSet<TopSectionMovieViewModel>();
        this.Movies = new HashSet<MovieViewModel>();
    }
    public IEnumerable<TopSectionMovieViewModel> TopMovies { get; set; }

    public IEnumerable<MovieViewModel> Movies { get; set; }
}

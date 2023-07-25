namespace FilmIdea.Web.ViewModels.Movie;
public class NewMoviesViewModel
{
    public NewMoviesViewModel()
    {
        this.TopMovies = new HashSet<TopSectionMovieViewModel>();
        this.Movies = new HashSet<MovieViewModel>();
    }
    public IEnumerable<TopSectionMovieViewModel> TopMovies { get; set; }

    public IEnumerable<MovieViewModel> Movies { get; set; }
}

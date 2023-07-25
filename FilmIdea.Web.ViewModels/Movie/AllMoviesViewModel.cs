namespace FilmIdea.Web.ViewModels.Movie;
public class AllMoviesViewModel
{
    public AllMoviesViewModel()
    {
        this.TopMovies = new HashSet<TopSectionMovieViewModel>();
        this.Movies = new HashSet<MoviesViewModel>();
    }
    public IEnumerable<TopSectionMovieViewModel> TopMovies { get; set; }

    public IEnumerable<MoviesViewModel> Movies { get; set; }
}

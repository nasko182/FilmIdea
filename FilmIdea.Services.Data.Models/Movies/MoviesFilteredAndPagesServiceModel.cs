namespace FilmIdea.Services.Data.Models.Movies;

using Web.ViewModels.Movie;

public class MoviesFilteredAndPagesServiceModel
{
    public MoviesFilteredAndPagesServiceModel()
    {
        this.Movies = new HashSet<MovieViewModel>();
        this.TopMovies = new HashSet<TopSectionMovieViewModel>();
    }
    public int TotalMoviesCount { get; set; }

    public ICollection<MovieViewModel> Movies { get; set; }

    public ICollection<TopSectionMovieViewModel> TopMovies{ get; set; }
}

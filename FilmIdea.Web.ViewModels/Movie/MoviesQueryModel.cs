namespace FilmIdea.Web.ViewModels.Movie;

using System.ComponentModel.DataAnnotations;

using Enums;
using Genre;

using static Common.GeneralApplicationConstants;

public class MoviesQueryModel
{
    public MoviesQueryModel()
    {
        this.CurrentPage = DefaultPage;
        this.MoviesPerPage = EntitiesPerPage;

        this.Genres = new HashSet<GenreViewModel>();
        this.Movies = new HashSet<MovieViewModel>();
        this.TopSectionMovies = new HashSet<TopSectionMovieViewModel>();
    }
    public string? Genre { get; set; }

    [Display(Name = "Search by text")]
    public string? SearchString { get; set; }

    [Display(Name = "Sort by")]
    public MovieSorting MovieSorting { get; set; }

    public int CurrentPage { get; set; }

    public int TotalMovies { get; set; }

    [Display(Name = "Movies Per Page")]
    public int MoviesPerPage { get; set; }

    public ICollection<GenreViewModel> Genres { get; set; }

    public ICollection<MovieViewModel> Movies { get; set; }

    public ICollection<TopSectionMovieViewModel> TopSectionMovies { get; set; }
}

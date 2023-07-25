namespace FilmIdea.Web.ViewModels.Movie;

using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.MovieValidations;
public class TopSectionMovieViewModel
{
    public int Id { get; set; }

    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; } = null!;

    public string PictureUrl { get; set; } = null!;
}

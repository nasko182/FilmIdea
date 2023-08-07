namespace FilmIdea.Web.ViewModels.Movie;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using static Common.EntityValidationConstants.MovieValidations;

public class AddMovieViewModel
{
    [Required]
    [StringLength(TitleMaxLength,MinimumLength = TitleMinLength)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(DescriptionMaxLength,MinimumLength = DescriptionMinLength)]
    public string Description { get; set; } = null!;

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Release Date")]
    public DateTime ReleaseDate { get; set; }

    [Required]
    [Display(Name = "Duration in minutes")]
    public int Duration { get; set; }

    [Required]
    public IFormFile CoverImage { get; set; } = null!;

    [Required]
    public IFormFile Trailer { get; set; } = null!;

    [Required]
    [Display(Name = "Director Id")]
    public int DirectorId { get; set; }
}

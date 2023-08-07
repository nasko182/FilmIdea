namespace FilmIdea.Web.ViewModels.Movie;

using System;
using System.ComponentModel.DataAnnotations;

using static Common.EntityValidationConstants.MovieValidations;

public class EditMovieViewModel
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
    [MaxLength(CoverImageUrlMaxLength)]
    [Display(Name = "Cover Image URL")]
    public string CoverImageUrl { get; set; } = null!;

    [Required]
    [MaxLength(TrailerUrlMaxLength)]
    [Display(Name = "Trailer URL")]
    public string TrailerUrl { get; set; } = null!;

    [Required]
    [Display(Name = "Director Id")]
    public int DirectorId { get; set; }
}

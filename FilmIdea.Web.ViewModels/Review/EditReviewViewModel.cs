namespace FilmIdea.Web.ViewModels.Review;

using System.ComponentModel.DataAnnotations;
using static Common.EntityValidationConstants.ReviewValidations;

public class EditReviewViewModel
{
    [Required]
    public string ReviewId { get; set; } = null!;

    [Required]
    [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
    public string Title { get; set; } = null!;

    [Required]
    [Range(typeof(int),RatingMinValue,RatingMaxValue)]
    public int Rating { get; set; }

    [Required]
    [StringLength(ContentMaxLength,MinimumLength = ContentMinLength)]
    public string Content { get; set; } = null!;
}

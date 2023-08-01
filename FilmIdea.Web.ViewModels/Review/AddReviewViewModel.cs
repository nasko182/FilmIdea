namespace FilmIdea.Web.ViewModels.Review;

using System.ComponentModel.DataAnnotations;
using static Common.EntityValidationConstants.ReviewValidations;

public class AddReviewViewModel
{
    [Required]
    [StringLength(RatingMaxValue,MinimumLength = RatingMinValue)]
    public int Rating { get; set; }

    [Required]
    [StringLength(ContentMaxLength,MinimumLength = ContentMinLength)]
    public string Content { get; set; } = null!;
}

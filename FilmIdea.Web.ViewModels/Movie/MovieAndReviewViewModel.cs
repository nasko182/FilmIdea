namespace FilmIdea.Web.ViewModels.Movie;

using Review;

public class MovieAndReviewViewModel
{
    public AddReviewViewModel AddReview { get; set; } = null!;

    public MovieDetailsViewModel MovieDetails { get; set; } = null!;
}

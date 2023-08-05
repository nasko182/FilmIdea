namespace FilmIdea.Data.Seed;

using Models;

public class ReviewSeeder
{
    public ICollection<Review> GenerateReviews()
    {
        return new HashSet<Review>()
        {
            new Review()
            {
                Id=Guid.Parse("7DCC5BD6-1133-432B-B6A6-F6C27DA75948"),
                Title = "About the movie",
                MovieId = 2,
                Content = "I like the movie",
                CriticId = Guid.Parse("BCC5C503-128B-4BA6-A736-6EFBEDA1EE34"),
                Rating = 9
            }
        };
    }
}

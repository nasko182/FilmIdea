namespace FilmIdea.Data.Seed;

using Models;

public class CriticSeeder
{
    public ICollection<Critic> GenerateCritics()
    {
        return new HashSet<Critic>()
        {
            new Critic
            {
                Id = Guid.Parse("bf595423-7323-4e40-bd43-0f876c1beece"),
                Name = "Critic Criticov",
                Bio = "Critic Criticov bio",
                ProfileImageUrl = "https://dl.dropboxusercontent.com/scl/fi/aolsn1cqh30yds8e6fj66/CriticTestov_ProfileImage.jpg",
                DateOfBirth = new DateTime(2023,08,05),
                UserId = Guid.Parse("15EB7825-840B-4528-71CC-08DB8C333233")
            },new Critic
            {
                Id = Guid.Parse("93372a0a-b9f4-4bc5-8f53-e8da0bce2bfe"),
                Name = "Administrator",
                Bio = "Admin bio",
                ProfileImageUrl = "https://dl.dropboxusercontent.com/scl/fi/jv513momlbuc69iycnqzx/Administrator_ProfileImage.jpg",
                DateOfBirth = new DateTime(2023,08,05),
                UserId = Guid.Parse("94756cdf-566e-4bf8-b03b-416a118ad53b")
            }
        };
    }
}

namespace FilmIdea.Data.Seed;

using Models;

public class ApplicationUserSeeder
{
    public ICollection<ApplicationUser> GenerateUsers()
    {
        return new HashSet<ApplicationUser>()
        {
            new ApplicationUser()
            {
            Id = Guid.Parse("2532DDAA-63F0-4DE8-71CB-08DB8C333233"),
            Email = "user@user.bg",
            UserName = "user182",
            PasswordHash = "AQAAAAEAACcQAAAAEAsQ9sg0mW31vlM2DKquhykexBxdIKzD8YMSV5aAVT9ii4TazrF0Ep9t4AwwalAV8w=="
            },
            new ApplicationUser()
            {
                Id = Guid.Parse("15EB7825-840B-4528-71CC-08DB8C333233"),
                Email = "critic@critic.bg",
                UserName = "CriticTestov",
                PasswordHash = "AQAAAAEAACcQAAAAED8AiAX7LsAuELS3lYisOFNJVOGpUD+yNBUxLCVvKeE0nRH/nHDdWjTTbUk9Pt6efA=="
            },
        };
    }
}

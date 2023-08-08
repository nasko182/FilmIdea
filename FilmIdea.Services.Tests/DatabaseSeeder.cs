namespace FilmIdea.Services.Tests;

using FilmIdea.Data;
using FilmIdea.Data.Models;

public static class DatabaseSeeder
{
    public static ApplicationUser User;
    public static ApplicationUser CriticUser;
    public static Critic Critic;
    
    public static void SeedDataBase(FilmIdeaDbContext dbContext)
    {
        User = new ApplicationUser()
        {
            Id = Guid.Parse("2532DDAA-63F0-4DE8-71CB-08DB8C333233"),
            Email = "user@user.bg",
            UserName = "user182",
            PasswordHash = "AQAAAAEAACcQAAAAEAsQ9sg0mW31vlM2DKquhykexBxdIKzD8YMSV5aAVT9ii4TazrF0Ep9t4AwwalAV8w==",
            NormalizedEmail = "USER@USER.BG",
            NormalizedUserName = "USER182",
            LockoutEnabled = true,
            SecurityStamp = "CPHRH5Z35EEOSFSFA6UQV2ZDTJGKCIKX"
        };
        CriticUser = new ApplicationUser()
        {
            Id = Guid.Parse("15EB7825-840B-4528-71CC-08DB8C333233"),
            Email = "critic@critic.bg",
            UserName = "CriticCriticov",
            PasswordHash = "AQAAAAEAACcQAAAAED8AiAX7LsAuELS3lYisOFNJVOGpUD+yNBUxLCVvKeE0nRH/nHDdWjTTbUk9Pt6efA==",
            CriticId = Guid.Parse("bf595423-7323-4e40-bd43-0f876c1beece"),
            NormalizedUserName = "CRITICCRITICOV",
            NormalizedEmail = "CRITIC@CRITIC.BG",
            LockoutEnabled = true,
            SecurityStamp = "RCRRYTFZIKPBOLEU7PJ4QZDD7PL3EJTX"
        };
        Critic = new Critic
        {
            Id = Guid.Parse("bf595423-7323-4e40-bd43-0f876c1beece"),
            Name = "Critic Criticov",
            Bio = "Critic Criticov bio",
            ProfileImageUrl =
                "https://dl.dropboxusercontent.com/scl/fi/aolsn1cqh30yds8e6fj66/CriticTestov_ProfileImage.jpg",
            DateOfBirth = new DateTime(2023, 08, 05),
            UserId = Guid.Parse("15EB7825-840B-4528-71CC-08DB8C333233")
        };

        dbContext.ApplicationUsers.Add(User);
        dbContext.ApplicationUsers.Add(CriticUser);

        dbContext.Critics.Add(Critic);

        dbContext.SaveChanges();
    }
}

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
                Bio = "Critic Criticov is  an American film critic, film historian, journalist, essayist, screenwriter, and author. He was a film critic for the Chicago Sun-Times from 1967 until his death in 2013. In 1975, Ebert became the first film critic to win the Pulitzer Prize for Criticism. Neil Steinberg of the Chicago Sun-Times said Ebert \"was without question the nation's most prominent and influential film critic,\"[1] and Kenneth Turan of the Los Angeles Times called him \"the best-known film critic in America.\"[2]\r\n\r\nEbert was known for his intimate, Midwestern writing voice and critical views informed by values of populism and humanism.[3] Writing in a prose style intended to be entertaining and direct, he made sophisticated cinematic and analytical ideas more accessible to non-specialist audiences.[4] While a populist, Ebert frequently endorsed foreign and independent films he believed would be appreciated by mainstream viewers, which often resulted in such films receiving greater exposure.[5] Critic A. O. Scott wrote that Ebert's prose had a \"plain-spoken Midwestern clarity\" and a \"genial, conversational presence on the page...his criticism shows a nearly unequaled grasp of film history and technique, and formidable intellectual range, but he rarely seems to be showing off. He's just trying to tell you what he thinks, and to provoke some thought on your part about how movies work and what they can do\".[6]",
                ProfileImageUrl = "https://dl.dropboxusercontent.com/scl/fi/aolsn1cqh30yds8e6fj66/CriticTestov_ProfileImage.jpg",
                DateOfBirth = new DateTime(2023,08,05),
                UserId = Guid.Parse("15EB7825-840B-4528-71CC-08DB8C333233")
            },
            new Critic
            {
                Id = Guid.Parse("93372a0a-b9f4-4bc5-8f53-e8da0bce2bfe"),
                Name = "Administrator",
                Bio = "Administrators do NOT need bio :)",
                ProfileImageUrl = "https://dl.dropboxusercontent.com/scl/fi/jv513momlbuc69iycnqzx/Administrator_ProfileImage.jpg",
                DateOfBirth = new DateTime(2023,08,05),
                UserId = Guid.Parse("94756cdf-566e-4bf8-b03b-416a118ad53b")
            }
        };
    }
}

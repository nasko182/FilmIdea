namespace FilmIdea.Data.Seed;

using Models;

public class CommentSeeder
{
    public ICollection<Comment> GenerateComments()
    {
        return new HashSet<Comment>()
        {
            new Comment()
            {
                Content = "I like the movie",
                ReviewId = Guid.Parse("7DCC5BD6-1133-432B-B6A6-F6C27DA75948"),
                WriterId = Guid.Parse("2532DDAA-63F0-4DE8-71CB-08DB8C333233"),
            }
        };
    }
}

namespace FilmIdea.Data.Seed;

using Models.Join_Tables;

public class MovieActorSeeder
{
    public ICollection<MovieActor> GenerateMovieActors()
    {
        return new HashSet<MovieActor>()
        {
            new MovieActor()
            {
                ActorId = 2,
                MovieId = 2
            },
            new MovieActor()
            {
                ActorId = 1,
                MovieId = 1
            }
        };
    }
}

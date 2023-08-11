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
            },
            new MovieActor()
            {
                ActorId = 1003,
                MovieId = 2
            },
            new MovieActor()
            {
                ActorId = 1020,
                MovieId =2
            },
            new MovieActor()
            {
                ActorId = 1007,
                MovieId =1001
            },
            new MovieActor()
            {
                ActorId = 1016,
                MovieId =1001
            },
            new MovieActor()
            {
                ActorId = 1017,
                MovieId =1001
            },
            new MovieActor()
            {
                ActorId = 1007,
                MovieId =1007
            },
            new MovieActor()
            {
                ActorId = 1014,
                MovieId =1007
            },
            new MovieActor()
            {
                ActorId = 1016,
                MovieId =1007
            },
            new MovieActor()
            {
                ActorId = 1003,
                MovieId =1009
            },
            new MovieActor()
            {
                ActorId = 1013,
                MovieId =1009
            },
            new MovieActor()
            {
                ActorId = 1015,
                MovieId =1009
            },
            new MovieActor()
            {
                ActorId = 1020,
                MovieId =1012
            },
            new MovieActor()
            {
                ActorId = 1013,
                MovieId =1013
            },
            new MovieActor()
            {
                ActorId = 1006,
                MovieId =1014
            },
            new MovieActor()
            {
                ActorId = 1007,
                MovieId =1014
            },
            new MovieActor()
            {
                ActorId = 1011,
                MovieId =1014
            },
            new MovieActor()
            {
                ActorId = 1016,
                MovieId =1014
            },
            new MovieActor()
            {
                ActorId = 1007,
                MovieId =1015
            },
            new MovieActor()
            {
                ActorId = 1008,
                MovieId =1015
            },
            new MovieActor()
            {
                ActorId = 1010,
                MovieId =1016
            },
            new MovieActor()
            {
                ActorId = 1011,
                MovieId =1016
            },
            new MovieActor()
            {
                ActorId = 1014,
                MovieId =1017
            },
            new MovieActor()
            {
                ActorId = 1009,
                MovieId =1018
            },
            new MovieActor()
            {
                ActorId = 1010,
                MovieId =1019
            },
        };
    }
}

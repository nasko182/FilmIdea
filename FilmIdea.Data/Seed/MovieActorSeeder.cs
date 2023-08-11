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
                ActorId = 1009,
                MovieId = 1003
            },
            new MovieActor()
            {
                ActorId = 1012,
                MovieId =1003
            },
            new MovieActor()
            {
                ActorId = 1014,
                MovieId =1006
            },
            new MovieActor()
            {
                ActorId = 1001,
                MovieId =1007
            },
            new MovieActor()
            {
                ActorId = 1007,
                MovieId =1007
            },
            new MovieActor()
            {
                ActorId = 1015,
                MovieId =1008
            },
            new MovieActor()
            {
                ActorId = 1018,
                MovieId =1009
            },
            new MovieActor()
            {
                ActorId = 1016,
                MovieId =1010
            },
            new MovieActor()
            {
                ActorId = 1014,
                MovieId =1011
            },
            new MovieActor()
            {
                ActorId = 1019,
                MovieId =1012
            },
            new MovieActor()
            {
                ActorId = 1013,
                MovieId =1013
            },
            new MovieActor()
            {
                ActorId = 1009,
                MovieId =1013
            },
            new MovieActor()
            {
                ActorId = 1017,
                MovieId =1014
            },
            new MovieActor()
            {
                ActorId = 1007,
                MovieId =1014
            },
            new MovieActor()
            {
                ActorId = 1009,
                MovieId =1015
            },
            new MovieActor()
            {
                ActorId = 1007,
                MovieId =1016
            },
            new MovieActor()
            {
                ActorId = 1001,
                MovieId =1016
            },
            new MovieActor()
            {
                ActorId = 1001,
                MovieId =1017
            },
            new MovieActor()
            {
                ActorId = 1012,
                MovieId =1020
            },
            new MovieActor()
            {
                ActorId = 2,
                MovieId =1020
            }
        };
    }
}

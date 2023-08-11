namespace FilmIdea.Data.Seed;

using Models.Join_Tables;

public class MovieGenreSeeder
{
    public ICollection<MovieGenre> GenerateMovieGenres()
    {
        return new HashSet<MovieGenre>()
        {
            new MovieGenre()
            {
                GenreId = 1,
                MovieId = 1
            },
            new MovieGenre()
            {
                GenreId = 2,
                MovieId = 2
            },
            new MovieGenre()
            {
                MovieId = 1006,
                GenreId = 1
            },
            new MovieGenre()
            {
                MovieId = 1007,
                GenreId =1
            },
            new MovieGenre()
            {
                MovieId = 1010,
                GenreId =1
            },
            new MovieGenre()
            {
                MovieId = 1014,
                GenreId =1
            },
            new MovieGenre()
            {
                MovieId = 2,
                GenreId =1
            },
            new MovieGenre()
            {
                MovieId = 1007,
                GenreId =2
            },
            new MovieGenre()
            {
                MovieId = 1017,
                GenreId =2
            },
            new MovieGenre()
            {
                MovieId = 1012,
                GenreId =4
            },
            new MovieGenre()
            {
                MovieId = 1013,
                GenreId =4
            },
            new MovieGenre()
            {
                MovieId = 1014,
                GenreId =4
            },
            new MovieGenre()
            {
                MovieId = 1003,
                GenreId =5
            },
            new MovieGenre()
            {
                MovieId = 1009,
                GenreId =5
            },
            new MovieGenre()
            {
                MovieId = 1013,
                GenreId =5
            },
            new MovieGenre()
            {
                MovieId = 1015,
                GenreId =5
            },
            new MovieGenre()
            {
                MovieId = 1012,
                GenreId =7
            },
            new MovieGenre()
            {
                MovieId = 1013,
                GenreId =7
            },
            new MovieGenre()
            {
                MovieId = 1010,
                GenreId =10
            },
            new MovieGenre()
            {
                MovieId = 1017,
                GenreId =10
            },
            new MovieGenre()
            {
                MovieId = 1006,
                GenreId =11
            },
            new MovieGenre()
            {
                MovieId = 1007,
                GenreId =11
            },
            new MovieGenre()
            {
                MovieId = 1008,
                GenreId =11
            },
            new MovieGenre()
            {
                MovieId = 1010,
                GenreId =11
            },
            new MovieGenre()
            {
                MovieId = 1012,
                GenreId =11
            },
            new MovieGenre()
            {
                MovieId = 1015,
                GenreId =11
            },
            new MovieGenre()
            {
                MovieId = 1016,
                GenreId =11
            },
            new MovieGenre()
            {
                MovieId = 1017,
                GenreId =11
            },
            new MovieGenre()
            {
                MovieId = 1020,
                GenreId =11
            },
            new MovieGenre()
            {
                MovieId = 1003,
                GenreId =14
            },
            new MovieGenre()
            {
                MovieId = 1011,
                GenreId =15
            },
            new MovieGenre()
            {
                MovieId = 1014,
                GenreId =15
            },
            new MovieGenre()
            {
                MovieId = 1003,
                GenreId =16
            },
            new MovieGenre()
            {
                MovieId = 1006,
                GenreId =17
            },
            new MovieGenre()
            {
                MovieId = 1011,
                GenreId =17
            },
            new MovieGenre()
            {
                MovieId = 1007,
                GenreId =19
            },
            new MovieGenre()
            {
                MovieId = 1008,
                GenreId =19
            },
            new MovieGenre()
            {
                MovieId = 1011,
                GenreId =19
            },
            new MovieGenre()
            {
                MovieId = 1015,
                GenreId =19
            },
            new MovieGenre()
            {
                MovieId = 1016,
                GenreId =19
            },
        };
    }
}

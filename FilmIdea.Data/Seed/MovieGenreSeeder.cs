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
                GenreId = 1006,
                MovieId = 1
            },
            new MovieGenre()
            {
                GenreId = 1007,
                MovieId =1
            },
            new MovieGenre()
            {
                GenreId = 1010,
                MovieId =1
            },
            new MovieGenre()
            {
                GenreId = 1014,
                MovieId =1
            },
            new MovieGenre()
            {
                GenreId = 2,
                MovieId =1
            },
            new MovieGenre()
            {
                GenreId = 1007,
                MovieId =2
            },
            new MovieGenre()
            {
                GenreId = 1017,
                MovieId =2
            },
            new MovieGenre()
            {
                GenreId = 1012,
                MovieId =4
            },
            new MovieGenre()
            {
                GenreId = 1013,
                MovieId =4
            },
            new MovieGenre()
            {
                GenreId = 1014,
                MovieId =4
            },
            new MovieGenre()
            {
                GenreId = 1003,
                MovieId =5
            },
            new MovieGenre()
            {
                GenreId = 1009,
                MovieId =5
            },
            new MovieGenre()
            {
                GenreId = 1013,
                MovieId =5
            },
            new MovieGenre()
            {
                GenreId = 1015,
                MovieId =5
            },
            new MovieGenre()
            {
                GenreId = 1012,
                MovieId =7
            },
            new MovieGenre()
            {
                GenreId = 1013,
                MovieId =7
            },
            new MovieGenre()
            {
                GenreId = 1010,
                MovieId =10
            },
            new MovieGenre()
            {
                GenreId = 1017,
                MovieId =10
            },
            new MovieGenre()
            {
                GenreId = 1006,
                MovieId =11
            },
            new MovieGenre()
            {
                GenreId = 1007,
                MovieId =11
            },
            new MovieGenre()
            {
                GenreId = 1008,
                MovieId =11
            },
            new MovieGenre()
            {
                GenreId = 1010,
                MovieId =11
            },
            new MovieGenre()
            {
                GenreId = 1012,
                MovieId =11
            },
            new MovieGenre()
            {
                GenreId = 1015,
                MovieId =11
            },
            new MovieGenre()
            {
                GenreId = 1016,
                MovieId =11
            },
            new MovieGenre()
            {
                GenreId = 1017,
                MovieId =11
            },
            new MovieGenre()
            {
                GenreId = 1020,
                MovieId =11
            },
            new MovieGenre()
            {
                GenreId = 1003,
                MovieId =14
            },
            new MovieGenre()
            {
                GenreId = 1011,
                MovieId =15
            },
            new MovieGenre()
            {
                GenreId = 1014,
                MovieId =15
            },
            new MovieGenre()
            {
                GenreId = 1003,
                MovieId =16
            },
            new MovieGenre()
            {
                GenreId = 1006,
                MovieId =17
            },
            new MovieGenre()
            {
                GenreId = 1011,
                MovieId =17
            },
            new MovieGenre()
            {
                GenreId = 1007,
                MovieId =19
            },
            new MovieGenre()
            {
                GenreId = 1008,
                MovieId =19
            },
            new MovieGenre()
            {
                GenreId = 1011,
                MovieId =19
            },
            new MovieGenre()
            {
                GenreId = 1015,
                MovieId =19
            },
            new MovieGenre()
            {
                GenreId = 1016,
                MovieId =19
            },
        };
    }
}

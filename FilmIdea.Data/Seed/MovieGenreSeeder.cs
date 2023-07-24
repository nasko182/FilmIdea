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
            }
        };
    }
}

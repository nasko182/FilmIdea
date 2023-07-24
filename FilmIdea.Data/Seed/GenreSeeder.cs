namespace FilmIdea.Data.Seed;

using Models;

public class GenreSeeder
{
    public ICollection<Genre> GenerateGenres()
    {
        var genres = new HashSet<Genre>
        {
            new Genre()
            {
                Id=1,
                Name = "Action"
            },
            new Genre()
            {
                Id=2,
                Name = "Adventure"
            },
            new Genre()
            {
                Id=3,
                Name = "Animation"
            },
            new Genre()
            {
                Id=4,
                Name = "Biographical"
            },
            new Genre()
            {
                Id=5,
                Name = "Comedy"
            },
            new Genre()
            {
                Id=6,
                Name = "Action"
            },
            new Genre()
            {
                Id=7,
                Name = "Crime"
            },
            new Genre()
            {
                Id=8,
                Name = "Documentary"
            },
            new Genre()
            {
                Id = 9,
                Name = "Drama"
            },
            new Genre()
            {
                Id = 10,
                Name = "Family"
            },
            new Genre()
            {
                Id = 11,
                Name = "Fantasy"
            },
            new Genre()
            {
                Id = 12,
                Name = "History"
            },
            new Genre()
            {
                Id = 13,
                Name = "Horror"
            },
            new Genre()
            {
                Id = 14,
                Name = "Musical"
            },
            new Genre()
            {
                Id = 15,
                Name = "Mystery"
            },
            new Genre()
            {
                Id = 16,
                Name = "Romance"
            },
            new Genre()
            {
                Id = 17,
                Name = "Sci-Fi"
            },
            new Genre()
            {
                Id = 18,
                Name = "Sport"
            },
            new Genre()
            {
                Id = 19,
                Name = "Superhero"
            },
            new Genre()
            {
                Id = 20,
                Name = "Short"
            },
            new Genre()
            {
                Id = 21,
                Name = "Thriller"
            },
            new Genre()
            {
                Id = 22,
                Name = "War"
            },
            new Genre()
            {
                Id = 23,
                Name = "Western"
            }
        };


        return genres.ToArray();
    }
}

namespace FilmIdea.Data.Seed;

using Models;
using Models.Join_Tables;
using static System.Net.WebRequestMethods;

public class MovieSeeder
{
    public ICollection<Movie> GenerateMovies()
    {
        var movies = new HashSet<Movie>();

        var movie = new Movie()
        {
            Id = 1,
            Title = "The Flash",
            Description =
                "Barry Allen uses his super speed to change the past, but his attempt to save his family creates a world without super heroes, forcing him to race for his life in order to save the future.",
            Duration = 144,
            ReleaseYear = 2023,
            CoverImageUrl =
                "https://dl.dropboxusercontent.com/scl/fi/lgvgll5jt71j0ad3202d4/The_Flash_cover_image.jpg?rlkey=ya84xsnh8dioy08iw497uyjdf",
            DirectorId = 1,
            TrailerUrl =
                "https://dl.dropboxusercontent.com/scl/fi/t44ljvld1q9ybw8oeqe8z/2023-_trailer.mp4?rlkey=wnapnzwvtfhk1x5nlwr7oa88x",
            
        };
        movie.Actors.Add(new MovieActor()
        {
            ActorId = 1,
            MovieId = 1
        });
        movie.Genres.Add(new MovieGenre()
        {
            GenreId = 1,
            MovieId = 1
        });

        movies.Add(movie);

        movie = new Movie()
        {
            Id = 2,
            Title = "Indiana Jones and the Dial of Destiny",
            Description =
                "Archaeologist Indiana Jones races against time to retrieve a legendary artifact that can change the course of history.",
            Duration = 154,
            ReleaseYear = 2023,
            CoverImageUrl =
                "https://dl.dropboxusercontent.com/s/kban35sz6tw0d0x/Indiana_Jones_and_the_Dial_of_Destiny_cover_image.jpg",
            DirectorId = 2,
            TrailerUrl =
                "https://dl.dropboxusercontent.com/s/evfvsgzlf9f4vok/1434659607842-pgv4ql-1680875129724_trailer.mp4",
            
        };
        movie.Actors.Add(new MovieActor()
        {
            ActorId = 2
        });

        movie.Genres.Add(new MovieGenre()
        {
            GenreId = 1,
            MovieId = 2
        });
        movie.Genres.Add(new MovieGenre()
        {
            GenreId = 2,
            MovieId = 2
        });

        movie.Photos.Add(new Photo()
        {
            MovieId = 2,
            Url = "https://dl.dropboxusercontent.com/s/kban35sz6tw0d0x/Indiana_Jones_and_the_Dial_of_Destiny_cover_image.jpg"
        });

        movie.Videos.Add(new Video()
        {
            MovieId = 2,
            Url = "https://dl.dropboxusercontent.com/s/evfvsgzlf9f4vok/1434659607842-pgv4ql-1680875129724_trailer.mp4"
        });

        var review = new Review()
        {
            MovieId = 2,
            Content = "I like the movie",
            CriticId = Guid.Parse("15EB7825-840B-4528-71CC-08DB8C333233"),
            Rating = 9
        };
        review.Comments.Add(new Comment()
        {
            Review = review,
            Content = "Best Review Ever",
            WriterId = Guid.Parse("2532DDAA-63F0-4DE8-71CB-08DB8C333233")
        });
        movie.Reviews.Add(review);

        movies.Add(movie);

        return movies.ToArray();
    }
}

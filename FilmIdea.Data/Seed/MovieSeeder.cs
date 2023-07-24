namespace FilmIdea.Data.Seed;

using Models;
using Models.Join_Tables;
using static System.Net.WebRequestMethods;

public class MovieSeeder
{
    public ICollection<Movie> GenerateMovies()
    {
        return new HashSet<Movie>()
        {
            new Movie()
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

            },
            new Movie()
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

            }
        };
    }
}

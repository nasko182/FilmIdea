namespace FilmIdea.Data.Seed;

using Models;

public class DirectorSeeder
{
    public ICollection<Director> GenerateDirectors()
    {
        var directors = new HashSet<Director>();

        var director = new Director()
        {
            Id = 1,
            Name = "Andy Muschietti",
            Bio = "Andy Muschietti was born on 26 August 1973 in Buenos Aires, Federal District, Argentina. He is a producer and director, known for Мама (2013), То (2017) and То: Част втора (2019).",
            DateOfBirth = new DateTime(1973, 08, 26),
            ProfileImageUrl =
                "https://dl.dropboxusercontent.com/s/xye9456bo6f9ld3/MV5BMTkwMDE0NTc0NF5B_profile_image._V1_.jpg"
        };
        directors.Add(director);

        director = new Director()
        {
            Id = 2,
            Name = "James Mangold",
            Bio =
                "James Mangold is an American film and television director, screenwriter and producer. Films he has directed include Луди години (1999), Да преминеш границата (2005), which he also co-wrote, the 2007 remake Ескорт до затвора (2007), Върколакът (2013), and Логан: Върколакът (2017).\r\n\r\nMangold also wrote and directed Копланд (1997), starring Sylvester Stallone, Robert De Niro, Harvey Keitel, and Ray Liotta.",
            DateOfBirth = new DateTime(1963, 12, 16),
            ProfileImageUrl =
                "https://dl.dropboxusercontent.com/s/491y3cpjoc4ulzm/MV5BNDI3MzgwMmYtY2JjYy00ZWQ2LTgzN_profile_image.jpg"
        };

        director.Photos.Add(new Photo()
        {
            Url = "https://dl.dropboxusercontent.com/s/491y3cpjoc4ulzm/MV5BNDI3MzgwMmYtY2JjYy00ZWQ2LTgzN_profile_image.jpg",
            DirectorId = 1
        });

        director.Videos.Add(new Video()
        {
            Url = "https://dl.dropboxusercontent.com/s/joz6wy1q4o55jz4/1434659607842-pgv4ql-1687560633004.mp4",
            DirectorId = 1
        });
        directors.Add(director);

        return directors.ToArray();
    }
}

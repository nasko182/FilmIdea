namespace FilmIdea.Data.Seed;

using Models;

public class PhotoSeeder
{
    public ICollection<Photo> GeneratePhotos()
    {
        return new HashSet<Photo>()
        {
            new Photo()
            {
                Url = "https://dl.dropboxusercontent.com/s/oxqc24aw3xb9n9z/MV5BMTY4Mjg0NjIxOV5BMl5Ban_profile_image.jpg",
                ActorId = 2
            },
            new Photo()
            {
                MovieId = 2,
                Url = "https://dl.dropboxusercontent.com/s/kban35sz6tw0d0x/Indiana_Jones_and_the_Dial_of_Destiny_cover_image.jpg"
            },
            new Photo()
            {
                Url = "https://dl.dropboxusercontent.com/s/491y3cpjoc4ulzm/MV5BNDI3MzgwMmYtY2JjYy00ZWQ2LTgzN_profile_image.jpg",
                DirectorId = 2
            }
        };
    }
}

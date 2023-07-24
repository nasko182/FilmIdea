namespace FilmIdea.Data.Seed;

using Models;

public class VideoSeeder
{
    public ICollection<Video> GenerateVideos()
    {
        return new HashSet<Video>()
        {
            new Video()
            {
                MovieId = 2,
                Url = "https://dl.dropboxusercontent.com/s/evfvsgzlf9f4vok/1434659607842-pgv4ql-1680875129724_trailer.mp4"
            },
            new Video()
            {
                Url = "https://dl.dropboxusercontent.com/s/joz6wy1q4o55jz4/1434659607842-pgv4ql-1687560633004.mp4",
                DirectorId = 2
            },
            new Video()
            {
                Url = "https://dl.dropboxusercontent.com/s/l6151myomc65wqc/1434659607842-pgv4ql-1687556762812.mp4",
                ActorId = 2
            }
        };
    }
}

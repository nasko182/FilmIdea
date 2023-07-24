namespace FilmIdea.Data.Seed;

using Models;
using System.Xml.Linq;

public class ActorSeeder
{
    public ICollection<Actor> GenerateActors()
    {
        var actors = new HashSet<Actor>();
        var actor = new Actor()
        {
            Id = 1,
            Name = "Ezra Miller",
            Bio = "Ezra Matthew Miller was born in Wyckoff, New Jersey, to Marta (Koch), a modern dancer, and Robert S. Miller, who has worked at Workman Publishing and as former senior V.P. for Hyperion Books. Ezra has two older sisters and is of Ashkenazi Jewish (father) and German-Dutch (mother) ancestry. Ezra has described themselves as Jewish and \"spiritual\".\r\n\r\nAs a child, Miller sang with the Metropolitan Opera and attended Rockland Country Day School and The Hudson School. Miller's first feature film was the independent Afterschool (2008), with subsequent appearances on the television series Секс до дупка (2007), Закон и ред: Специални разследвания (1999), and Лекар на повикване (2009), and in the films City Island (2009), Всеки ден (2010), Beware the Gonzo (2010), and Another Happy Day (2011).\r\n\r\nMiller drew critical praise playing Kevin Khatchadourian, the homicidal son of Tilda Swinton's character, in the dramatic thriller Трябва да поговорим за Кевин (2011). Miller subsequently played Patrick in the well-received teen drama Предимствата да бъдеш аутсайдер (2012), opposite Logan Lerman and Emma Watson.\r\n\r\nEzra's other roles include the period piece Мадам Бовари (2014), Judd Apatow's comedy Тотал щета (2015), and the psychological thriller The Stanford Prison Experiment (2015). Miller has been cast as superhero The Flash in Светкавицата (2023), scheduled for release in 2022.",
            DateOfBirth = new DateTime(1992, 09, 30),
            ProfileImageUrl = "https://dl.dropboxusercontent.com/s/aync8kuoks5ii3f/Something_profile_image.jpg",
        };
        actors.Add(actor);

        actor = new Actor()
        {
            Id = 2,
            Name = "Harrison Ford",
            Bio = "Harrison Ford was born on July 13, 1942 in Chicago, Illinois, to Dorothy (Nidelman), a radio actress, and Christopher Ford (born John William Ford), an actor turned advertising executive. His father was of Irish and German ancestry, while his maternal grandparents were Jewish emigrants from Minsk, Belarus. Harrison was a lackluster student at Maine Township High School East in Park Ridge Illinois (no athletic star, never above a C average). After dropping out of Ripon College in Wisconsin, where he did some acting and later summer stock, he signed a Hollywood contract with Columbia and later Universal. His roles in movies and television (Ironside (1967), The Virginian (1962)) remained secondary and, discouraged, he turned to a career in professional carpentry. He came back big four years later, however, as Bob Falfa in Американски графити (1973). Four years after that, he hit colossal with the role of Han Solo in Междузвездни войни: Епизод IV - Нова Надежда (1977). Another four years and Ford was Indiana Jones in Похитители на изчезналия кивот (1981).\r\n\r\nFour years later and he received Academy Award and Golden Globe nominations for his role as John Book in Свидетел (1985). All he managed four years after that was his third starring success as Indiana Jones; in fact, many of his earlier successful roles led to sequels as did his more recent portrayal of Jack Ryan in Патриотични игри (1992). Another Golden Globe nomination came his way for the part of Dr. Richard Kimble in Беглецът (1993). He is clearly a well-established Hollywood superstar. He also maintains an 800-acre ranch in Jackson Hole, Wyoming.\r\n\r\nFord is a private pilot of both fixed-wing aircraft and helicopters, and owns an 800-acre (3.2 km2) ranch in Jackson, Wyoming, approximately half of which he has donated as a nature reserve. On several occasions, Ford has personally provided emergency helicopter services at the request of local authorities, in one instance rescuing a hiker overcome by dehydration. Ford began flight training in the 1960s at Wild Rose Idlewild Airport in Wild Rose, Wisconsin, flying in a Piper PA-22 Tri-Pacer, but at $15 an hour, he could not afford to continue the training. In the mid-1990s, he bought a used Gulfstream II and asked one of his pilots, Terry Bender, to give him flying lessons. They started flying a Cessna 182 out of Jackson, Wyoming, later switching to Teterboro, New Jersey, flying a Cessna 206, the aircraft he soloed in. Ford is an honorary board member of the humanitarian aviation organization Wings of Hope.\r\n\r\nOn March 5, 2015, Ford's plane, believed to be a Ryan PT-22 Recruit, made an emergency landing on the Penmar Golf Course in Venice, California. Ford had radioed in to report that the plane had suffered engine failure. He was taken to Ronald Reagan UCLA Medical Center, where he was reported to be in fair to moderate condition. Ford suffered a broken pelvis and broken ankle during the accident, as well as other injuries.",
            DateOfBirth = new DateTime(1942, 07, 13),
            ProfileImageUrl = "https://dl.dropboxusercontent.com/s/aync8kuoks5ii3f/Something_profile_image.jpg",
        };
        actor.Photos.Add(new Photo()
        {
            Url = "https://dl.dropboxusercontent.com/s/oxqc24aw3xb9n9z/MV5BMTY4Mjg0NjIxOV5BMl5Ban_profile_image.jpg",
            ActorId = 1
        });

        actor.Videos.Add(new Video()
        {
            Url = "https://dl.dropboxusercontent.com/s/l6151myomc65wqc/1434659607842-pgv4ql-1687556762812.mp4",
            ActorId = 1
        });
        actors.Add(actor);

        return actors.ToArray();
    }
}

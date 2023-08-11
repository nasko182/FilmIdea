namespace FilmIdea.Data.Seed;

using Models;

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
                ReleaseDate = new DateTime(2023,3,15),
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
                ReleaseDate = new DateTime(2023, 5, 24),
                CoverImageUrl =
                    "https://dl.dropboxusercontent.com/s/kban35sz6tw0d0x/Indiana_Jones_and_the_Dial_of_Destiny_cover_image.jpg",
                DirectorId = 2,
                TrailerUrl =
                    "https://dl.dropboxusercontent.com/s/evfvsgzlf9f4vok/1434659607842-pgv4ql-1680875129724_trailer.mp4",

            },
            new Movie()
            {
                Id = 1003,
                Title = "Barbie",
                Description = "Barbie suffers a crisis that leads her to question her world and her existence",
                Duration = 114,
                ReleaseDate = new DateTime(2023,07,21),
                CoverImageUrl = "https://dl.dropboxusercontent.com/scl/fi/azbfvlt972dy4t5sygzii/Barbie_CoverImage.jpg?rlkey=vmf7122bseqsxeif5vigbyxb4",
                DirectorId = 1001,
                TrailerUrl = "https://dl.dropboxusercontent.com/scl/fi/c4uuqbg38lipejpdkj0s3/Barbie_Trailer.mp4?rlkey=hexi9dm3p97ujwglgkw1cn3ju",
            },
            new Movie()
            {
                Id = 1006,
                Title = "Jurassic World: Fallen Kingdom",
                Description = "When the island's dormant volcano begins roaring to life, Owen and Claire mount a campaign to rescue the remaining dinosaurs from this extinction-level event.",
                Duration = 128,
                ReleaseDate = new DateTime(2018,08,24),
                CoverImageUrl = "https://dl.dropboxusercontent.com/scl/fi/ksdursap46bdhr8ccfkm1/Jurassic-World-Fallen-Kingdom_CoverImage.jpg?rlkey=zpdumvgy9jbc25ppxvk7gtj6r",
                DirectorId = 1005,
                TrailerUrl = "https://dl.dropboxusercontent.com/scl/fi/har6gphbx8whg0onnm969/Jurassic-World-Fallen-Kingdom_Trailer.mp4?rlkey=7wzfd7c5cu1l90cqdij7cm4jv",
            },
            new Movie()
            {
                Id = 1007,
                Title = "Avengers: Endgame",
                Description = "After the devastating events of Advengers:  War(2018), the universe is in ruins. With the help of remaining allies, the Avengers assemble once more in order to reverse Thanos' actions and restore balance to the universe.",
                Duration = 181,
                ReleaseDate = new DateTime(2019,04,26),
                CoverImageUrl = "https://dl.dropboxusercontent.com/scl/fi/4e8kznbyb0r1ly2xadp2i/Avengers-Endgame_CoverImage.jpg?rlkey=unhpzjogw5izyod2xde36z0p9",
                DirectorId = 1004,
                TrailerUrl = "https://dl.dropboxusercontent.com/scl/fi/dbnf05zefybz86yeuk4fl/Avengers-Endgame_Trailer.mp4?rlkey=mxmy8lwsqnxb3u5hv2oti0ebk",
            },
            new Movie()
            {
                Id = 1008,
                Title = "Black Panther",
                Description = "T'Challa, heir to the hidden but advanced kingdom of Wakanda, must step forward to lead his people into a new future and must confront a challenger from his country's past.",
                Duration = 134,
                ReleaseDate = new DateTime(2018,02,16),
                CoverImageUrl = "https://dl.dropboxusercontent.com/scl/fi/qhgpdaqm91cf4urqmrrez/Black-Panther_CoverImage.jpg?rlkey=ch2mmht2bgxd388gt3ppq8f5t",
                DirectorId = 1005,
                TrailerUrl = "https://dl.dropboxusercontent.com/scl/fi/eo9zd6no7w6cptcbvb8sj/Black-Panther_Trailer.mp4?rlkey=4z29a87hxg3taxccso90jml6e",
            },
            new Movie()
            {
                Id = 1009,
                Title = "The Hangover II",
                Description = "Three buddies wake up from a bachelor party in Las Vegas, with no memory of the previous night and the bachelor missing. They make their way around the city in order to find their friend before his wedding.",
                Duration = 1006,
                ReleaseDate = new DateTime(2007,06,05),
                CoverImageUrl = "https://dl.dropboxusercontent.com/scl/fi/7bgm1wt5sxnk0wtzc3y2r/The-Hangover-II_CoverImage.jpg?rlkey=e80yfei74yh1qzyr9j9lxogfz",
                DirectorId = 10,
                TrailerUrl = "https://dl.dropboxusercontent.com/scl/fi/aboafm63fb89qirpszl44/The-Hangover-II_Trailer.mp4?rlkey=l23lend702b87v0ekri7bpnaz",
            },
            new Movie()
            {
                Id = 1010,
                Title = "Fast X",
                Description = "Dom Toretto and his family are targeted by the vengeful son of drug kingpin Hernan Reyes.",
                Duration = 141,
                ReleaseDate = new DateTime(2023,05,10),
                CoverImageUrl = "https://dl.dropboxusercontent.com/scl/fi/zeh9q21hihz4h7x3jaokz/Fast-X_CoverImage.jpg?rlkey=ujgvvdcq8bng4q08ap8u73x1c",
                DirectorId = 1007,
                TrailerUrl = "https://dl.dropboxusercontent.com/scl/fi/sl4s78qzqz108qrnlvw06/Fast-X_Trailer.mp4?rlkey=1dhdqwjlwt0bkwnfwb3qki3km",
            },
            new Movie()
            {
                Id = 1011,
                Title = "Guardians of the Galaxy Vol. 3",
                Description = "Still reeling from the loss of Gamora, Peter Quill rallies his team to defend the universe and one of their own - a mission that could mean the end of the Guardians if not successful.",
                Duration = 150,
                ReleaseDate = new DateTime(2020,05,05),
                CoverImageUrl = "https://dl.dropboxusercontent.com/scl/fi/wa981exkyfch3lek0mj09/Guardians-of-the-Galaxy-Vol.-3_CoverImage.jpg?rlkey=bthwfsde1atsepmy2w4hfvaj5",
                DirectorId = 1008,
                TrailerUrl = "https://dl.dropboxusercontent.com/scl/fi/6sg5jze46rsgm48zn6nn0/Guardians-of-the-Galaxy-Vol.-3_Trailer.mp4?rlkey=sf6jg0l9ba3ttgfmsqces26qi",
            },
            new Movie()
            {
                Id = 1012,
                Title = "Joker",
                Description = "The rise of Arthur Fleck, from aspiring stand-up comedian and pariah to Gotham's clown prince and leader of the revolution.",
                Duration = 122,
                ReleaseDate = new DateTime(2019,10,04),
                CoverImageUrl = "https://dl.dropboxusercontent.com/scl/fi/9osghrllfz8kuw2rx2qey/Joker_CoverImage.jpg?rlkey=rx89asfu5pjuzkofbg9u0xfpn",
                DirectorId = 1006,
                TrailerUrl = "https://dl.dropboxusercontent.com/scl/fi/g8af3e82qafqlqeqybobe/Joker_Trailer.mp4?rlkey=u3tjhbgizp5q9oyr09eumuk18",
            },
            new Movie()
            {
                Id = 1013,
                Title = "The Wolf of Wall Street",
                Description = "Based on the true story of Jordan Belfort, from his rise to a wealthy stock-broker living the high life to his fall involving crime, corruption and the federal government.",
                Duration = 187,
                ReleaseDate = new DateTime(2013,12,25),
                CoverImageUrl = "https://dl.dropboxusercontent.com/scl/fi/u1ew0gbr96hpkm828hpgu/The-Wolf-of-Wall-Street_CoverImage.jpg?rlkey=wzfv4mkzhtmrb2cz9eug8aeb6",
                DirectorId = 1010,
                TrailerUrl = "https://dl.dropboxusercontent.com/scl/fi/r38t5xjw1q8j17sv14xtb/The-Wolf-of-Wall-Street_Trailer.mp4?rlkey=o2mybmi1hcc838t2nvqd22qsz",
            },
            new Movie()
            {
                Id = 1014,
                Title = "Oppenheimer",
                Description = "The story of American scientist, J. Robert Oppenheimer, and his role in the development of the atomic bomb.",
                Duration = 173,
                ReleaseDate = new DateTime(2023,07,17),
                CoverImageUrl = "https://dl.dropboxusercontent.com/scl/fi/tqyba8w6ct5ge9us089yx/Oppenheimer_CoverImage.jpg?rlkey=qe18p1jvgb5u7gi0mlmaw35ao",
                DirectorId = 1009,
                TrailerUrl = "https://dl.dropboxusercontent.com/scl/fi/a39704pdrbf20sc9ryrqg/Oppenheimer_Trailer.mp4?rlkey=gkxm0sfjp7ly5sa93q8r79j07",
            },
            new Movie()
            {
                Id = 1015,
                Title = "Suicide Squad",
                Description = "A secret government agency recruits some of the most dangerous incarcerated super-villains to form a defensive task force. Their first mission: save the world from the apocalypse.",
                Duration = 123,
                ReleaseDate = new DateTime(2016,08,05),
                CoverImageUrl = "https://dl.dropboxusercontent.com/scl/fi/6vj6m2b7htake0h44x42r/Suicide-Squad_CoverImage.jpg?rlkey=y68dt274dwvhp17q6bg50i174",
                DirectorId = 1011,
                TrailerUrl = "https://dl.dropboxusercontent.com/scl/fi/wh0hv2p44rycvda4htshb/Suicide-Squad_Trailer.mp4?rlkey=65fr160zxdpgor5sloubtl1t0",
            },
            new Movie()
            {
                Id = 1016,
                Title = "Avengers: Infinity War",
                Description = "The Avengers and their allies must be willing to sacrifice all in an attempt to defeat the powerful Thanos before his blitz of devastation and ruin puts an end to the universe.",
                Duration = 149,
                ReleaseDate = new DateTime(2018,04,27),
                CoverImageUrl = "https://dl.dropboxusercontent.com/scl/fi/2lgy3krvjc8jov1m4tpdw/Avengers-Infinity-War_CoverImage.jpg?rlkey=dglmwdk3be9xjgm0inu984br7",
                DirectorId = 1004,
                TrailerUrl = "https://dl.dropboxusercontent.com/scl/fi/sxo3vldqpnjpupl54e05s/Avengers-Infinity-War_Trailer.mp4?rlkey=2gqw1ajtfm4n0hz9t4faekdde",
            },
            new Movie()
            {
                Id = 1017,
                Title = "Uncharted",
                Description = "Street-smart Nathan Drake is recruited by seasoned treasure hunter Victor \"Sully\" Sullivan to recover a fortune amassed by Ferdinand Magellan, and lost 500 years ago by the House of Moncada.",
                Duration = 116,
                ReleaseDate = new DateTime(2022,12,02),
                CoverImageUrl = "https://dl.dropboxusercontent.com/scl/fi/r31pv26br6jxlgh4phivi/Uncharted_CoverImage.jpg?rlkey=cizigwaqlxg7ypnnji64k1rmi",
                DirectorId = 1012,
                TrailerUrl = "https://dl.dropboxusercontent.com/scl/fi/vn6tjhwdjt63txuzuzc31/Uncharted_Trailer.mp4?rlkey=m14qo6nvhqgirj8r14j76j45z",
            },
            new Movie()
            {
                Id = 1020,
                Title = "Blade Runner 2049",
                Description = "Young Blade Runner K's discovery of a long-buried secret leads him to track down former Blade Runner Rick Deckard, who's been missing for thirty years.",
                Duration = 146,
                ReleaseDate = new DateTime(2017,09,20),
                CoverImageUrl = "https://dl.dropboxusercontent.com/scl/fi/vral4iavrbc0lk0x36vel/Blade-Runner-2049_CoverImage.jpg?rlkey=kgnpjc3nelyba4rgoyml400zy",
                DirectorId = 112,
                TrailerUrl = "https://dl.dropboxusercontent.com/scl/fi/sn346bvsm61xc0swlvb1x/Blade-Runner-2049_Trailer.mp4?rlkey=gyxp5gzest3b56peqk3tars7j",
            },

        };
    }
}

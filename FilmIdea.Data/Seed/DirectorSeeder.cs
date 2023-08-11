namespace FilmIdea.Data.Seed;

using Models;

public class DirectorSeeder
{
    public ICollection<Director> GenerateDirectors()
    {
        return new HashSet<Director>()
        {
            new Director()
            {
                Id = 1,
                Name = "Andy Muschietti",
                Bio = "Andy Muschietti was born on 26 August 1973 in Buenos Aires, Federal District, Argentina. He is a producer and director, known for Мама (2013), То (2017) and То: Част втора (2019).",
                DateOfBirth = new DateTime(1973, 08, 26),
                ProfileImageUrl =
                    "https://dl.dropboxusercontent.com/s/xye9456bo6f9ld3/MV5BMTkwMDE0NTc0NF5B_profile_image._V1_.jpg"
            },
            new Director()
            {
                Id = 2,
                Name = "James Mangold",
                Bio =
                    "James Mangold is an American film and television director, screenwriter and producer. Films he has directed include Луди години (1999), Да преминеш границата (2005), which he also co-wrote, the 2007 remake Ескорт до затвора (2007), Върколакът (2013), and Логан: Върколакът (2017).\r\n\r\nMangold also wrote and directed Копланд (1997), starring Sylvester Stallone, Robert De Niro, Harvey Keitel, and Ray Liotta.",
                DateOfBirth = new DateTime(1963, 12, 16),
                ProfileImageUrl =
                    "https://dl.dropboxusercontent.com/s/491y3cpjoc4ulzm/MV5BNDI3MzgwMmYtY2JjYy00ZWQ2LTgzN_profile_image.jpg"
            },
            new Director()
            {
                Id = 1001,
                Name = "Greta Gerwig",
                Bio = "Greta Gerwig is an American actress, playwright, screenwriter, and director. She has collaborated with Noah Baumbach on several films, including Greenberg (2010), Frances Ha (2012), for which she earned a Golden Globe nomination, and Mistress America (2015). Gerwig made her solo directorial debut with the critically acclaimed comedy-drama film Лейди Бърд (2017), which she also wrote, and has also had starring roles in the films Damsels in Distress (2011), Jackie (2016), and 20th Century Women (2016).\r\n\r\nGreta Celeste Gerwig was born in Sacramento, California, to Christine Gerwig (née Sauer), a nurse, and Gordon Gerwig, a financial consultant and computer programmer. She has German, Irish, and English ancestry. Gerwig was raised as a Unitarian Universalist, but also attended an all-girls Catholic school. She has described herself as \"an intense child\". With an early interest in dance, she intended to get a degree in musical theatre in New York. She graduated from Barnard College in NY, where she studied English and philosophy, instead. Originally intending to become a playwright, after meeting young film director Joe Swanberg, she became the star of a series of intellectual low budget movies made by first-time filmmakers, a trend dubbed \"mumblecore\".\r\n\r\nGerwig was cast in a minor role in Swanberg's LOL (2006) in 2006, while still studying at Barnard. She then appeared in many of Swanberg's films, and personally co-directed, co-wrote and co-produced one entitled Nights and Weekends (2008). She has worked with good quality directors such as Ti West (Къщата на дявола (2009)), Whit Stillman (Damsels in Distress (2011)), or Woody Allen (На Рим с любов (2012)) but success and (international) recognition did not come until Франсис Ха (2012), directed by Noah Baumbach, a film she also co-wrote. Both tall and immature, awkward and graceful, blundering and candid, annoying and engaging, Greta has won all hearts in the title role of Frances Ha(liday).",
                DateOfBirth = new DateTime(1983,08 ,04 ),
                ProfileImageUrl = "https://dl.dropboxusercontent.com/scl/fi/lj96i6jco3k5kquu482hr/Greta-Gerwig_ProfileImage.jpg?rlkey=ipkpl0wok9k8lczi3xb57mdhr"
            },
            new Director()
            {
                Id = 1004,
                Name = "Anthony Russo",
                Bio = "Anthony J. Russo is an American filmmaker and producer who works alongside his brother Joseph Russo. They have directed You, Me and Dupree, Cherry and the Marvel films Captain America: The Winter Soldier, Captain America: Civil War, Avengers: Infinity War and Avengers: Endgame. Endgame is one of the highest grossing films of all time.",
                DateOfBirth = new DateTime(1970,02 ,03 ),
                ProfileImageUrl = "https://dl.dropboxusercontent.com/scl/fi/5s3pw9nltwzxbn9qhtwxe/Anthony-Russo_ProfileImage.jpg?rlkey=vzsat7wh10st141k5a0v35adg"
            },
            new Director()
            {
                Id = 1005,
                Name = "Ryan Coogler",
                Bio = "Ryan Kyle Coogler is an African-American filmmaker and producer who is from Oakland, California. He is known for directing the Black Panther film series, Creed, a Rocky spin-off and Fruitvale Station. He frequently casts Michael B. Jordan in his works. He produced the Creed sequels, Judas and the Black Messiah and Space Jam: A New Legacy. He is married to Zinzi since 2016.",
                DateOfBirth = new DateTime(1986,05 ,23 ),
                ProfileImageUrl = "https://dl.dropboxusercontent.com/scl/fi/kcdzpmxuya99j91c5hqkr/Ryan-Coogler_ProfileImage.jpg?rlkey=2d1mnwewacvfvijm6xswzau6u"
            },
            new Director()
            {
                Id = 1006,
                Name = "Todd Phillips",
                Bio = "Todd Phillips is an American film director, producer, and screenwriter.\r\n\r\nGrowing up on Long Island, New York, Todd Phillips fell in love with feature film teen comedies made in the 1980s, and claims they were his biggest influence in becoming a filmmaker. While studying film at New York University, he made a documentary called Hated (1994), using his credit cards to finance the filmâEUR(TM)s $13,000 budget. About an excessive punk rocker, GG Allen, the student film won an award at the New Orleans Film Festival and went on to be released both theatrically and on DVD. Phillips' next project was a documentary called Frat House (1998), which followed the trials of young men trying to get accepted into a fraternity. The film won the Grand Jury Prize at the Sundance Film Festival, but soon became banned from public viewing when the young men involved objected, and lawyers for their families stepped in.\r\n\r\nWhile working on a commercial for Pepsi, Phillips met comedian Tom Green. He was writing the screenplay for his new film, Road Trip, and asked Green if he would be in it. Green agreed on the spot, and Phillips went on to make his first fictional movie, an homage to the types of films he grew up with. Road Trip was made on a budget of $15.6 million, and nearly made the money back in its opening weekend despite mixed reviews, most of which agreed it was in bad taste, with some finding that funny while others found it offensive.\r\n\r\nPhillips continued on in the same genre with Old School (2003), about three grown men who try to return to their frat boy days. Phillips says, \"Things go in cycles and right now people use the term gross out of comedy a lot and I find it very dismissive. I think it's very easy to be gross and very hard to be funny. The ones that work are actually very funny at their root. I, as a director, want to stick with comedies for a little while. It's the movies I grew up on and the stuff I like to see.\"\r\n\r\nPhillips' next project was action comedy Starsky & Hutch, based on the hit television series that ran from 1975 to 1979. The film, starring Owen Wilson and Ben Stiller, is also set in the '70s. He's hoping to turn another '70s TV show, The Six Million Dollar Man, into a feature film starring Jim Carrey, but in the meantime, filmed the comedy School for Scoundrels (2006), starring Jon Heder and Billy Bob Thornton. His next film, The Hangover 2009, was an enormous success, spawning a 2011 sequel that he also directed. In between those two movies he directed Robert Downey Jr. and Hangover star Zach Galifianakis in the comedy Due Date 2010.\r\n\r\nMore recent films include The Hangover Part II (2011), The Hangover Part III (2013), and War Dogs (2016).\r\n\r\nMove away from his favorite genre, he next took on the film Joker (2019), starring Joaquin Phoenix in the title role. The film debuted to much acclaim, and both Joaquin and Phillips received numerous award nominations, including Best Director nods for Phillips from the Academy Awards, Golden Globes and the BAFTAs.",
                DateOfBirth = new DateTime(1970,12 ,20 ),
                ProfileImageUrl = "https://dl.dropboxusercontent.com/scl/fi/qfibl9810e48o2hlcwwaw/Todd-Phillips_ProfileImage.jpg?rlkey=0qh0gj0cprarvgfwttgsj3efb"
            },
            new Director()
            {
                Id = 1007,
                Name = "Louis Leterrier",
                Bio = "Louis Leterrier is a French film director and producer. He notably directed the first two Transporter films, Unleashed, The Incredible Hulk, Clash of the Titans, Now You See Me, Tower of Strength and The Brothers Grimsby. He also directed episodes of The Dark Crystal: Age of Resistance on Netflix and three episodes of Lupin.",
                DateOfBirth = new DateTime(1973,07 ,17 ),
                ProfileImageUrl = "https://dl.dropboxusercontent.com/scl/fi/hlxsjn0l4psl1rqage5w2/Louis-Leterrier_ProfileImage.jpg?rlkey=c5sim0hwp1gvfrsabphaoaqzo"
            },
            new Director()
            {
                Id = 1008,
                Name = "James Gunn",
                Bio = "James Gunn was born and raised in St. Louis, Missouri, to Leota and James Francis Gunn. He is from a large Catholic family, with Irish and Czech ancestry. His father and his uncles were all lawyers. He has been writing and performing as long as he can remember. He began making 8mm films at the age of twelve. Many of these were comedic splatter films featuring his brothers being disemboweled by zombies. He attended Saint Louis University High (SLUH) college preparatory school but later dropped out of college to pursue a rock and roll career.\r\n\r\nHis band, \"the Icons\", released one album, \"Mom, We Like It Here on Earth\". He earned very little money doing this and so during this time, he also worked as an orderly in Tucson, Arizona, upon which many of the situations in his first novel, \"The Toy Collector\", are based. He wrote and drew comic strips for underground and college newspapers.\r\n\r\nGunn eventually returned to school and received his B.A. at Saint Louis University in his native St. Louis. He moved to New York where he received an MFA in creative writing from Columbia University, which he today thinks may have been a wonderfully expensive waste of time. While finishing his MFA, he started writing \"The Toy Collector\" and began working for \"Troma Studios\", America's leading B-Movie production company. While there he wrote and produced the cult classic Tromeo and Juliet (1996) and, with Lloyd Kaufman, he wrote \"All I Need to Know about Filmmaking I Learned from the Toxic Avenger\".",
                DateOfBirth = new DateTime(1966,08 ,05 ),
                ProfileImageUrl = "https://dl.dropboxusercontent.com/scl/fi/5cigibl1esxhp9lvvou20/James-Gunn_ProfileImage.jpg?rlkey=aqd7cazq1wtjs9m9j8u8zpaga"
            },
            new Director()
            {
                Id = 1009,
                Name = "Christopher Nolan",
                Bio = "Best known for his cerebral, often nonlinear, storytelling, acclaimed writer-director Christopher Nolan was born on July 30, 1970, in London, England. Over the course of 15 years of filmmaking, Nolan has gone from low-budget independent films to working on some of the biggest blockbusters ever made.\r\n\r\nAt 7 years old, Nolan began making short movies with his father's Super-8 camera. While studying English Literature at University College London, he shot 16-millimeter films at U.C.L.'s film society, where he learned the guerrilla techniques he would later use to make his first feature, on a budget of around $6,000. The noir thriller was recognized at a number of international film festivals prior to its theatrical release and gained Nolan enough credibility that he was able to gather substantial financing for his next film.\r\n\r\nNolan's second film was which he directed from his own screenplay based on a short story by his brother Jonathan. Starring Guy Pearce, the film brought Nolan numerous honors, including Academy Award and Golden Globe Award nominations for Best Original Screenplay. Nolan went on to direct the critically acclaimed psychological thriller, starring Al Pacino, Robin Williams and Hilary Swank.",
                DateOfBirth = new DateTime(1970,07 ,30 ),
                ProfileImageUrl = "https://dl.dropboxusercontent.com/scl/fi/yaxuuujaf8hlk3w1xsb30/Christopher-Nolan_ProfileImage.jpg?rlkey=sx7r3sg05xe5r9f5htvw7p0xt"
            },
            new Director()
            {
                Id = 1010,
                Name = "Martin Scorsese",
                Bio = "Martin Charles Scorsese was born on November 17, 1942 in Queens, New York City, to Catherine Scorsese (née Cappa) and Charles Scorsese, who both worked in Manhattan's garment district, and whose families both came from Palermo, Sicily. He was raised in the neighborhood of Little Italy, which later provided the inspiration for several of his films. Scorsese earned a B.S. degree in film communications in 1964, followed by an M.A. in the same field in 1966 at New York University's School of Film. During this time, he made numerous prize-winning short films including The Big Shave (1967), and directed his first feature film, I Call First (1967).\r\n\r\nHe served as assistant director and an editor of the documentary Woodstock (1970) and won critical and popular acclaim for Жестоки улици (1973), which first paired him with actor and frequent collaborator Robert De Niro. In 1976, Scorsese's Шофьор на такси (1976), also starring De Niro, was awarded the Palme d'Or at the Cannes Film Festival, and he followed that film with Ню Йорк, Ню Йорк (1977) and Последният валс (1978). Scorsese directed De Niro to an Oscar-winning performance as boxer Jake LaMotta in Разяреният бик (1980), which received eight Academy Award nominations, including Best Picture and Best Director, and is hailed as one of the masterpieces of modern cinema. Scorsese went on to direct Цветът на парите (1986), Последното изкушение на Христос (1988), Добри момчета (1990), Нос Страх (1991), Невинни години (1993), Казино (1995) and Кундун (1997), among other films. Commissioned by the British Film Institute to celebrate the 100th anniversary of the birth of cinema, Scorsese completed the four-hour documentary, A Personal Journey with Martin Scorsese Through American Movies (1995), co-directed by Michael Henry Wilson.\r\n\r\nHis long-cherished project, Бандите на Ню Йорк (2002), earned numerous critical honors, including a Golden Globe Award for Best Director; the Howard Hughes biopic Авиаторът (2004) won five Academy Awards, in addition to the Golden Globe and BAFTA awards for Best Picture. Scorsese won his first Academy Award for Best Director for От другата страна (2006), which was also honored with the Director's Guild of America, Golden Globe, New York Film Critics, National Board of Review and Critic's Choice awards for Best Director, in addition to four Academy Awards, including Best Picture. Scorsese's documentary of the Rolling Stones in concert, Shine a Light (2008), followed, with the successful thriller Злокобен остров (2010) two years later. Scorsese received his seventh Academy Award nomination for Best Director, as well as a Golden Globe Award, for Изобретението на Хюго (2011), which went on to win five Academy Awards.",
                DateOfBirth = new DateTime(1942,11 ,17 ),
                ProfileImageUrl = "https://dl.dropboxusercontent.com/scl/fi/qfvqldcfp61rp8chtmh05/Martin-Scorsese_ProfileImage.jpg?rlkey=dgfetl4p1xiolsnvsb0gri2us"
            },
            new Director()
            {
                Id = 1011,
                Name = "David Ayer",
                Bio = "David Ayer is an American film director, producer, and screenwriter. David Ayer was born in Champaign, Illinois and grew up in Bloomington, Minnesota, and Bethesda, Maryland, where he was kicked out of his house by his parents as a teenager. Ayer then lived with his cousin in Los Angeles, California, where his experiences in South Central Los Angeles became the inspiration for many of his films. Ayer then enlisted in the United States Navy as a submariner.",
                DateOfBirth = new DateTime(1968,01 ,18 ),
                ProfileImageUrl = "https://dl.dropboxusercontent.com/scl/fi/u7yvbii32fzutbfmgv3hn/David-Ayer_ProfileImage.jpg?rlkey=lnzur78y9klourmam6iwl366z"
            },
            new Director()
            {
                Id = 1012,
                Name = "Ruben Fleischer",
                Bio = "Ruben Fleischer was born on October 31, 1974 in Washington, District of Columbia, USA. He is a producer and director, known for Zombie Land (2009),and Venum (2018). He has been married to Holly Shakoor Fleischer since November 10, 2012.",
                DateOfBirth = new DateTime(1974,10 ,31 ),
                ProfileImageUrl = "https://dl.dropboxusercontent.com/scl/fi/xqrb3ujq03q238llhie8u/Ruben-Fleischer_ProfileImage.jpg?rlkey=zxqfu8aptj5io0m9ohhz2ezdn"
            },

        };
    }
}

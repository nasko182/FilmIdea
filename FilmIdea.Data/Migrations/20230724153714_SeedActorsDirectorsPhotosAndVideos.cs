using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmIdea.Data.Migrations
{
    public partial class SeedActorsDirectorsPhotosAndVideos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Directors_DirectorId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesActors_Actors_ActorId",
                table: "MoviesActors");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Actors_ActorId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Directors_DirectorId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Actors_ActorId",
                table: "Videos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Directors_DirectorId",
                table: "Videos");

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "Bio", "DateOfBirth", "Name", "ProfileImageUrl" },
                values: new object[,]
                {
                    { 1, "Ezra Matthew Miller was born in Wyckoff, New Jersey, to Marta (Koch), a modern dancer, and Robert S. Miller, who has worked at Workman Publishing and as former senior V.P. for Hyperion Books. Ezra has two older sisters and is of Ashkenazi Jewish (father) and German-Dutch (mother) ancestry. Ezra has described themselves as Jewish and \"spiritual\".\r\n\r\nAs a child, Miller sang with the Metropolitan Opera and attended Rockland Country Day School and The Hudson School. Miller's first feature film was the independent Afterschool (2008), with subsequent appearances on the television series Секс до дупка (2007), Закон и ред: Специални разследвания (1999), and Лекар на повикване (2009), and in the films City Island (2009), Всеки ден (2010), Beware the Gonzo (2010), and Another Happy Day (2011).\r\n\r\nMiller drew critical praise playing Kevin Khatchadourian, the homicidal son of Tilda Swinton's character, in the dramatic thriller Трябва да поговорим за Кевин (2011). Miller subsequently played Patrick in the well-received teen drama Предимствата да бъдеш аутсайдер (2012), opposite Logan Lerman and Emma Watson.\r\n\r\nEzra's other roles include the period piece Мадам Бовари (2014), Judd Apatow's comedy Тотал щета (2015), and the psychological thriller The Stanford Prison Experiment (2015). Miller has been cast as superhero The Flash in Светкавицата (2023), scheduled for release in 2022.", new DateTime(1992, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ezra Miller", "https://dl.dropboxusercontent.com/s/aync8kuoks5ii3f/Something_profile_image.jpg" },
                    { 2, "Harrison Ford was born on July 13, 1942 in Chicago, Illinois, to Dorothy (Nidelman), a radio actress, and Christopher Ford (born John William Ford), an actor turned advertising executive. His father was of Irish and German ancestry, while his maternal grandparents were Jewish emigrants from Minsk, Belarus. Harrison was a lackluster student at Maine Township High School East in Park Ridge Illinois (no athletic star, never above a C average). After dropping out of Ripon College in Wisconsin, where he did some acting and later summer stock, he signed a Hollywood contract with Columbia and later Universal. His roles in movies and television (Ironside (1967), The Virginian (1962)) remained secondary and, discouraged, he turned to a career in professional carpentry. He came back big four years later, however, as Bob Falfa in Американски графити (1973). Four years after that, he hit colossal with the role of Han Solo in Междузвездни войни: Епизод IV - Нова Надежда (1977). Another four years and Ford was Indiana Jones in Похитители на изчезналия кивот (1981).\r\n\r\nFour years later and he received Academy Award and Golden Globe nominations for his role as John Book in Свидетел (1985). All he managed four years after that was his third starring success as Indiana Jones; in fact, many of his earlier successful roles led to sequels as did his more recent portrayal of Jack Ryan in Патриотични игри (1992). Another Golden Globe nomination came his way for the part of Dr. Richard Kimble in Беглецът (1993). He is clearly a well-established Hollywood superstar. He also maintains an 800-acre ranch in Jackson Hole, Wyoming.\r\n\r\nFord is a private pilot of both fixed-wing aircraft and helicopters, and owns an 800-acre (3.2 km2) ranch in Jackson, Wyoming, approximately half of which he has donated as a nature reserve. On several occasions, Ford has personally provided emergency helicopter services at the request of local authorities, in one instance rescuing a hiker overcome by dehydration. Ford began flight training in the 1960s at Wild Rose Idlewild Airport in Wild Rose, Wisconsin, flying in a Piper PA-22 Tri-Pacer, but at $15 an hour, he could not afford to continue the training. In the mid-1990s, he bought a used Gulfstream II and asked one of his pilots, Terry Bender, to give him flying lessons. They started flying a Cessna 182 out of Jackson, Wyoming, later switching to Teterboro, New Jersey, flying a Cessna 206, the aircraft he soloed in. Ford is an honorary board member of the humanitarian aviation organization Wings of Hope.\r\n\r\nOn March 5, 2015, Ford's plane, believed to be a Ryan PT-22 Recruit, made an emergency landing on the Penmar Golf Course in Venice, California. Ford had radioed in to report that the plane had suffered engine failure. He was taken to Ronald Reagan UCLA Medical Center, where he was reported to be in fair to moderate condition. Ford suffered a broken pelvis and broken ankle during the accident, as well as other injuries.", new DateTime(1942, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harrison Ford", "https://dl.dropboxusercontent.com/s/aync8kuoks5ii3f/Something_profile_image.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "Bio", "DateOfBirth", "Name", "ProfileImageUrl" },
                values: new object[,]
                {
                    { 1, "Andy Muschietti was born on 26 August 1973 in Buenos Aires, Federal District, Argentina. He is a producer and director, known for Мама (2013), То (2017) and То: Част втора (2019).", new DateTime(1973, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Andy Muschietti", "https://dl.dropboxusercontent.com/s/xye9456bo6f9ld3/MV5BMTkwMDE0NTc0NF5B_profile_image._V1_.jpg" },
                    { 2, "James Mangold is an American film and television director, screenwriter and producer. Films he has directed include Луди години (1999), Да преминеш границата (2005), which he also co-wrote, the 2007 remake Ескорт до затвора (2007), Върколакът (2013), and Логан: Върколакът (2017).\r\n\r\nMangold also wrote and directed Копланд (1997), starring Sylvester Stallone, Robert De Niro, Harvey Keitel, and Ray Liotta.", new DateTime(1963, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "James Mangold", "https://dl.dropboxusercontent.com/s/491y3cpjoc4ulzm/MV5BNDI3MzgwMmYtY2JjYy00ZWQ2LTgzN_profile_image.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CoverImageUrl", "Description", "DirectorId", "Duration", "ReleaseYear", "Title", "TrailerUrl" },
                values: new object[,]
                {
                    { 1, "https://dl.dropboxusercontent.com/scl/fi/lgvgll5jt71j0ad3202d4/The_Flash_cover_image.jpg?rlkey=ya84xsnh8dioy08iw497uyjdf", "Barry Allen uses his super speed to change the past, but his attempt to save his family creates a world without super heroes, forcing him to race for his life in order to save the future.", 1, 144, 2023, "The Flash", "https://dl.dropboxusercontent.com/scl/fi/t44ljvld1q9ybw8oeqe8z/2023-_trailer.mp4?rlkey=wnapnzwvtfhk1x5nlwr7oa88x" },
                    { 2, "https://dl.dropboxusercontent.com/s/kban35sz6tw0d0x/Indiana_Jones_and_the_Dial_of_Destiny_cover_image.jpg", "Archaeologist Indiana Jones races against time to retrieve a legendary artifact that can change the course of history.", 2, 154, 2023, "Indiana Jones and the Dial of Destiny", "https://dl.dropboxusercontent.com/s/evfvsgzlf9f4vok/1434659607842-pgv4ql-1680875129724_trailer.mp4" }
                });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[,]
                {
                    { new Guid("2a9a360e-2e5c-45ec-be98-a787f8dd78e3"), null, 2, null, "https://dl.dropboxusercontent.com/s/491y3cpjoc4ulzm/MV5BNDI3MzgwMmYtY2JjYy00ZWQ2LTgzN_profile_image.jpg" },
                    { new Guid("5e3572db-63c8-4870-b857-2e493f36e620"), 2, null, null, "https://dl.dropboxusercontent.com/s/oxqc24aw3xb9n9z/MV5BMTY4Mjg0NjIxOV5BMl5Ban_profile_image.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[,]
                {
                    { new Guid("2b510e78-2c9f-4f95-9905-e0170cfbb9fb"), null, 2, null, "https://dl.dropboxusercontent.com/s/joz6wy1q4o55jz4/1434659607842-pgv4ql-1687560633004.mp4" },
                    { new Guid("d860f759-bec0-4afe-a3ea-ca2b1eea1d5a"), 2, null, null, "https://dl.dropboxusercontent.com/s/l6151myomc65wqc/1434659607842-pgv4ql-1687556762812.mp4" }
                });

            migrationBuilder.InsertData(
                table: "MoviesActors",
                columns: new[] { "ActorId", "MovieId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "MoviesGenres",
                columns: new[] { "GenreId", "MovieId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[] { new Guid("a3ea2f9d-dfb7-4c08-8f13-b63459382d9b"), null, null, 2, "https://dl.dropboxusercontent.com/s/kban35sz6tw0d0x/Indiana_Jones_and_the_Dial_of_Destiny_cover_image.jpg" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "CriticId", "MovieId", "Rating", "ReviewDate" },
                values: new object[] { new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"), "I like the movie", new Guid("BCC5C503-128B-4BA6-A736-6EFBEDA1EE34"), 2, 9, new DateTime(2023, 7, 24, 15, 37, 13, 688, DateTimeKind.Utc).AddTicks(1472) });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[] { new Guid("d06d5744-8865-4307-b504-03d1e46c8f42"), null, null, 2, "https://dl.dropboxusercontent.com/s/evfvsgzlf9f4vok/1434659607842-pgv4ql-1680875129724_trailer.mp4" });

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Directors_DirectorId",
                table: "Movies",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesActors_Actors_ActorId",
                table: "MoviesActors",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Actors_ActorId",
                table: "Photos",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Directors_DirectorId",
                table: "Photos",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Actors_ActorId",
                table: "Videos",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Directors_DirectorId",
                table: "Videos",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Directors_DirectorId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviesActors_Actors_ActorId",
                table: "MoviesActors");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Actors_ActorId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Directors_DirectorId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Actors_ActorId",
                table: "Videos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Directors_DirectorId",
                table: "Videos");

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "MoviesActors",
                keyColumns: new[] { "ActorId", "MovieId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "MoviesGenres",
                keyColumns: new[] { "GenreId", "MovieId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: new Guid("2a9a360e-2e5c-45ec-be98-a787f8dd78e3"));

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: new Guid("5e3572db-63c8-4870-b857-2e493f36e620"));

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: new Guid("a3ea2f9d-dfb7-4c08-8f13-b63459382d9b"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"));

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: new Guid("2b510e78-2c9f-4f95-9905-e0170cfbb9fb"));

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: new Guid("d06d5744-8865-4307-b504-03d1e46c8f42"));

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: new Guid("d860f759-bec0-4afe-a3ea-ca2b1eea1d5a"));

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Directors_DirectorId",
                table: "Movies",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviesActors_Actors_ActorId",
                table: "MoviesActors",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Actors_ActorId",
                table: "Photos",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Directors_DirectorId",
                table: "Photos",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Actors_ActorId",
                table: "Videos",
                column: "ActorId",
                principalTable: "Actors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Directors_DirectorId",
                table: "Videos",
                column: "DirectorId",
                principalTable: "Directors",
                principalColumn: "Id");
        }
    }
}

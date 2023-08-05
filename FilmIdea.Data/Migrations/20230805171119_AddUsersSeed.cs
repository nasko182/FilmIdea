using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmIdea.Data.Migrations
{
    public partial class AddUsersSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: new Guid("0eb8afd0-e028-4c35-95da-c97044c4567c"));

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: new Guid("4357d61d-f845-4643-b573-d9decd34b81f"));

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: new Guid("7d6e434b-42d7-4627-b8dc-52fa56aedcd8"));

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: new Guid("150c31a5-4df9-42b5-86bd-cfbb323e9ee6"));

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: new Guid("3bad0a61-b76b-48d2-8970-3db6f5d43529"));

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: new Guid("c9bd1b17-296f-4fda-959b-45086c8cd1d3"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("15eb7825-840b-4528-71cc-08db8c333233"),
                columns: new[] { "ConcurrencyStamp", "UserName" },
                values: new object[] { "c88e4c8d-924b-4f30-b55e-f3e79d0e49ae", "CriticCriticov" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233"),
                column: "ConcurrencyStamp",
                value: "97b82d5d-f7ce-42d9-9c3e-91177465b575");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CriticId", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("94756cdf-566e-4bf8-b03b-416a118ad53b"), 0, "eda30e9f-335b-40a1-a996-fa0f11975edb", null, "admin@admin.com", false, false, null, null, null, "AQAAAAEAACcQAAAAEIDAX0WNuYyTTwkcp9gbVk146rrp9KcqXGumzJ7/bR33e1FBPSMU6035VP9GrLkwPA==", null, false, null, false, "Administrator" });

            migrationBuilder.InsertData(
                table: "Critics",
                columns: new[] { "Id", "Bio", "DateOfBirth", "Name", "ProfileImageUrl", "UserId" },
                values: new object[,]
                {
                    { new Guid("93372a0a-b9f4-4bc5-8f53-e8da0bce2bfe"), "Admin bio", new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Administrator", "https://dl.dropboxusercontent.com/scl/fi/jv513momlbuc69iycnqzx/Administrator_ProfileImage.jpg", new Guid("94756cdf-566e-4bf8-b03b-416a118ad53b") },
                    { new Guid("bf595423-7323-4e40-bd43-0f876c1beece"), "Critic Criticov bio", new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Critic Criticov", "https://dl.dropboxusercontent.com/scl/fi/aolsn1cqh30yds8e6fj66/CriticTestov_ProfileImage.jpg", new Guid("15eb7825-840b-4528-71cc-08db8c333233") }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "CriticId", "MovieId", "Rating", "ReviewDate", "Title" },
                values: new object[] { new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"), "I like the movie", new Guid("bf595423-7323-4e40-bd43-0f876c1beece"), 2, 9, new DateTime(2023, 8, 5, 12, 35, 17, 267, DateTimeKind.Utc).AddTicks(950), "About the movie" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentDate", "Content", "ReviewId", "WriterId" },
                values: new object[] { new Guid("05aa3335-a1d1-4b61-bc53-ea80ac781f6f"), new DateTime(2023, 8, 5, 17, 11, 18, 487, DateTimeKind.Utc).AddTicks(9573), "I like the movie", new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"), new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233") });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[,]
                {
                    { new Guid("21f200bc-b7e7-4c81-9ce6-17f05e962221"), null, 2, null, "https://dl.dropboxusercontent.com/s/491y3cpjoc4ulzm/MV5BNDI3MzgwMmYtY2JjYy00ZWQ2LTgzN_profile_image.jpg" },
                    { new Guid("9b619f41-5227-4cb1-8c3d-b260997a0f85"), null, null, 2, "https://dl.dropboxusercontent.com/s/kban35sz6tw0d0x/Indiana_Jones_and_the_Dial_of_Destiny_cover_image.jpg" },
                    { new Guid("ea9f59db-91b6-4792-a227-7ea48485d1a5"), 2, null, null, "https://dl.dropboxusercontent.com/s/oxqc24aw3xb9n9z/MV5BMTY4Mjg0NjIxOV5BMl5Ban_profile_image.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[,]
                {
                    { new Guid("0c1dd942-65c1-45e2-9760-bfc4a14aaa54"), null, null, 2, "https://dl.dropboxusercontent.com/s/evfvsgzlf9f4vok/1434659607842-pgv4ql-1680875129724_trailer.mp4" },
                    { new Guid("55b6963d-3072-4d2e-8779-b80741ceb39c"), null, 2, null, "https://dl.dropboxusercontent.com/s/joz6wy1q4o55jz4/1434659607842-pgv4ql-1687560633004.mp4" },
                    { new Guid("f7354770-1e02-44ba-b4c9-387506068542"), 2, null, null, "https://dl.dropboxusercontent.com/s/l6151myomc65wqc/1434659607842-pgv4ql-1687556762812.mp4" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("94756cdf-566e-4bf8-b03b-416a118ad53b"));

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("05aa3335-a1d1-4b61-bc53-ea80ac781f6f"));

            migrationBuilder.DeleteData(
                table: "Critics",
                keyColumn: "Id",
                keyValue: new Guid("93372a0a-b9f4-4bc5-8f53-e8da0bce2bfe"));

            migrationBuilder.DeleteData(
                table: "Critics",
                keyColumn: "Id",
                keyValue: new Guid("bf595423-7323-4e40-bd43-0f876c1beece"));

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: new Guid("21f200bc-b7e7-4c81-9ce6-17f05e962221"));

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: new Guid("9b619f41-5227-4cb1-8c3d-b260997a0f85"));

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: new Guid("ea9f59db-91b6-4792-a227-7ea48485d1a5"));

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: new Guid("0c1dd942-65c1-45e2-9760-bfc4a14aaa54"));

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: new Guid("55b6963d-3072-4d2e-8779-b80741ceb39c"));

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: new Guid("f7354770-1e02-44ba-b4c9-387506068542"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("15eb7825-840b-4528-71cc-08db8c333233"),
                columns: new[] { "ConcurrencyStamp", "UserName" },
                values: new object[] { "59239abf-0f8c-43db-9638-b9189d25f748", "CriticTestov" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233"),
                column: "ConcurrencyStamp",
                value: "134545f4-6d10-4eda-8ece-22847147ec3a");

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "CriticId", "MovieId", "Rating", "ReviewDate", "Title" },
                values: new object[] { new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"), "I like the movie", new Guid("bf595423-7323-4e40-bd43-0f876c1beece"), 2, 9, new DateTime(2023, 8, 5, 12, 35, 17, 267, DateTimeKind.Utc).AddTicks(950), "About the movie" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentDate", "Content", "ReviewId", "WriterId" },
                values: new object[] { new Guid("bcecd0a8-0e5f-4a70-96de-d410b23b0b6f"), new DateTime(2023, 8, 5, 12, 35, 17, 263, DateTimeKind.Utc).AddTicks(7964), "I like the movie", new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"), new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233") });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[,]
                {
                    { new Guid("0eb8afd0-e028-4c35-95da-c97044c4567c"), 2, null, null, "https://dl.dropboxusercontent.com/s/oxqc24aw3xb9n9z/MV5BMTY4Mjg0NjIxOV5BMl5Ban_profile_image.jpg" },
                    { new Guid("4357d61d-f845-4643-b573-d9decd34b81f"), null, null, 2, "https://dl.dropboxusercontent.com/s/kban35sz6tw0d0x/Indiana_Jones_and_the_Dial_of_Destiny_cover_image.jpg" },
                    { new Guid("7d6e434b-42d7-4627-b8dc-52fa56aedcd8"), null, 2, null, "https://dl.dropboxusercontent.com/s/491y3cpjoc4ulzm/MV5BNDI3MzgwMmYtY2JjYy00ZWQ2LTgzN_profile_image.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[,]
                {
                    { new Guid("150c31a5-4df9-42b5-86bd-cfbb323e9ee6"), null, 2, null, "https://dl.dropboxusercontent.com/s/joz6wy1q4o55jz4/1434659607842-pgv4ql-1687560633004.mp4" },
                    { new Guid("3bad0a61-b76b-48d2-8970-3db6f5d43529"), null, null, 2, "https://dl.dropboxusercontent.com/s/evfvsgzlf9f4vok/1434659607842-pgv4ql-1680875129724_trailer.mp4" },
                    { new Guid("c9bd1b17-296f-4fda-959b-45086c8cd1d3"), 2, null, null, "https://dl.dropboxusercontent.com/s/l6151myomc65wqc/1434659607842-pgv4ql-1687556762812.mp4" }
                });
        }
    }
}

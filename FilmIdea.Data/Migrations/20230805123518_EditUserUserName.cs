using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmIdea.Data.Migrations
{
    public partial class EditUserUserName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("5f6acd50-28f1-4493-84bc-5b6c7e5215f4"));

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: new Guid("1d4b2fed-3db8-423a-aa9a-c25394c5ead3"));

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: new Guid("99b5a3d2-2e87-4b25-8f42-5f2227c8a1cc"));

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: new Guid("9c488d9f-632c-42cd-8654-7d8c724aa490"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("80950fdc-f333-4819-b77b-05149fae405d"));

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: new Guid("00bf070a-ca2e-48c8-ade7-98dd3909fad1"));

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: new Guid("270736ee-7c47-4cb3-802c-01f5aeaae120"));

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: new Guid("5dc44bac-cef2-46a9-bd7c-b7216f3aca73"));

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Critics",
                type: "nvarchar(105)",
                maxLength: 105,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

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
                columns: new[] { "ConcurrencyStamp", "UserName" },
                values: new object[] { "134545f4-6d10-4eda-8ece-22847147ec3a", "user182" });

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
                table: "Reviews",
                columns: new[] { "Id", "Content", "CriticId", "MovieId", "Rating", "ReviewDate", "Title" },
                values: new object[] { new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"), "I like the movie", new Guid("bcc5c503-128b-4ba6-a736-6efbeda1ee34"), 2, 9, new DateTime(2023, 8, 5, 12, 35, 17, 267, DateTimeKind.Utc).AddTicks(950), "About the movie" });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[,]
                {
                    { new Guid("150c31a5-4df9-42b5-86bd-cfbb323e9ee6"), null, 2, null, "https://dl.dropboxusercontent.com/s/joz6wy1q4o55jz4/1434659607842-pgv4ql-1687560633004.mp4" },
                    { new Guid("3bad0a61-b76b-48d2-8970-3db6f5d43529"), null, null, 2, "https://dl.dropboxusercontent.com/s/evfvsgzlf9f4vok/1434659607842-pgv4ql-1680875129724_trailer.mp4" },
                    { new Guid("c9bd1b17-296f-4fda-959b-45086c8cd1d3"), 2, null, null, "https://dl.dropboxusercontent.com/s/l6151myomc65wqc/1434659607842-pgv4ql-1687556762812.mp4" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentDate", "Content", "ReviewId", "WriterId" },
                values: new object[] { new Guid("bcecd0a8-0e5f-4a70-96de-d410b23b0b6f"), new DateTime(2023, 8, 5, 12, 35, 17, 263, DateTimeKind.Utc).AddTicks(7964), "I like the movie", new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"), new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("bcecd0a8-0e5f-4a70-96de-d410b23b0b6f"));

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

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"));

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Critics");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("15eb7825-840b-4528-71cc-08db8c333233"),
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "UserName" },
                values: new object[] { "083310f7-842e-444d-b8f4-488ef23688ba", "Critic", "Testov", "critic@critic.bg" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233"),
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName", "UserName" },
                values: new object[] { "9c8a8807-b931-4eca-92a0-b6711b63a774", "User", "Testov", "user@user.bg" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentDate", "Content", "ReviewId", "WriterId" },
                values: new object[] { new Guid("5f6acd50-28f1-4493-84bc-5b6c7e5215f4"), new DateTime(2023, 8, 5, 9, 54, 57, 690, DateTimeKind.Utc).AddTicks(9969), "I like the movie", new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"), new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233") });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[,]
                {
                    { new Guid("1d4b2fed-3db8-423a-aa9a-c25394c5ead3"), 2, null, null, "https://dl.dropboxusercontent.com/s/oxqc24aw3xb9n9z/MV5BMTY4Mjg0NjIxOV5BMl5Ban_profile_image.jpg" },
                    { new Guid("99b5a3d2-2e87-4b25-8f42-5f2227c8a1cc"), null, null, 2, "https://dl.dropboxusercontent.com/s/kban35sz6tw0d0x/Indiana_Jones_and_the_Dial_of_Destiny_cover_image.jpg" },
                    { new Guid("9c488d9f-632c-42cd-8654-7d8c724aa490"), null, 2, null, "https://dl.dropboxusercontent.com/s/491y3cpjoc4ulzm/MV5BNDI3MzgwMmYtY2JjYy00ZWQ2LTgzN_profile_image.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "CriticId", "MovieId", "Rating", "ReviewDate", "Title" },
                values: new object[] { new Guid("80950fdc-f333-4819-b77b-05149fae405d"), "I like the movie", new Guid("bcc5c503-128b-4ba6-a736-6efbeda1ee34"), 2, 9, new DateTime(2023, 8, 5, 9, 54, 57, 694, DateTimeKind.Utc).AddTicks(3412), "About the movie" });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[,]
                {
                    { new Guid("00bf070a-ca2e-48c8-ade7-98dd3909fad1"), null, null, 2, "https://dl.dropboxusercontent.com/s/evfvsgzlf9f4vok/1434659607842-pgv4ql-1680875129724_trailer.mp4" },
                    { new Guid("270736ee-7c47-4cb3-802c-01f5aeaae120"), null, 2, null, "https://dl.dropboxusercontent.com/s/joz6wy1q4o55jz4/1434659607842-pgv4ql-1687560633004.mp4" },
                    { new Guid("5dc44bac-cef2-46a9-bd7c-b7216f3aca73"), 2, null, null, "https://dl.dropboxusercontent.com/s/l6151myomc65wqc/1434659607842-pgv4ql-1687556762812.mp4" }
                });
        }
    }
}

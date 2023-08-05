using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmIdea.Data.Migrations
{
    public partial class AddUserFirsAndLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: new Guid("6ddee118-bf9a-4029-b728-a271355cdc1d"));

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: new Guid("6f25ab66-2c86-4c23-bca7-2efb971dd681"));

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: new Guid("eaf26d38-95be-4f75-8314-2e9a9766955e"));

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: new Guid("198f84c5-c35d-45a0-9162-0192ccdd28e8"));

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: new Guid("d2c25e89-4323-44a6-8b37-49403b481a75"));

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: new Guid("e687984f-1287-4c39-91ca-20a150629c79"));

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Critics");

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
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName" },
                values: new object[] { "083310f7-842e-444d-b8f4-488ef23688ba", "Critic", "Testov" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233"),
                columns: new[] { "ConcurrencyStamp", "FirstName", "LastName" },
                values: new object[] { "9c8a8807-b931-4eca-92a0-b6711b63a774", "User", "Testov" });


            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "CriticId", "MovieId", "Rating", "ReviewDate", "Title" },
                values: new object[] { new Guid("80950fdc-f333-4819-b77b-05149fae405d"), "I like the movie", new Guid("bcc5c503-128b-4ba6-a736-6efbeda1ee34"), 2, 9, new DateTime(2023, 8, 5, 9, 54, 57, 694, DateTimeKind.Utc).AddTicks(3412), "About the movie" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentDate", "Content", "ReviewId", "WriterId" },
                values: new object[] { new Guid("5f6acd50-28f1-4493-84bc-5b6c7e5215f4"), new DateTime(2023, 8, 5, 9, 54, 57, 690, DateTimeKind.Utc).AddTicks(9969), "I like the movie", new Guid("80950fdc-f333-4819-b77b-05149fae405d"), new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233") });

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
                table: "Videos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[,]
                {
                    { new Guid("00bf070a-ca2e-48c8-ade7-98dd3909fad1"), null, null, 2, "https://dl.dropboxusercontent.com/s/evfvsgzlf9f4vok/1434659607842-pgv4ql-1680875129724_trailer.mp4" },
                    { new Guid("270736ee-7c47-4cb3-802c-01f5aeaae120"), null, 2, null, "https://dl.dropboxusercontent.com/s/joz6wy1q4o55jz4/1434659607842-pgv4ql-1687560633004.mp4" },
                    { new Guid("5dc44bac-cef2-46a9-bd7c-b7216f3aca73"), 2, null, null, "https://dl.dropboxusercontent.com/s/l6151myomc65wqc/1434659607842-pgv4ql-1687556762812.mp4" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("15eb7825-840b-4528-71cc-08db8c333233"),
                column: "ConcurrencyStamp",
                value: "da623be2-c099-483c-8ede-c161df6efaeb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233"),
                column: "ConcurrencyStamp",
                value: "627a032c-7f61-42d2-a9d4-12cd470a2c16");

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[,]
                {
                    { new Guid("6ddee118-bf9a-4029-b728-a271355cdc1d"), null, null, 2, "https://dl.dropboxusercontent.com/s/kban35sz6tw0d0x/Indiana_Jones_and_the_Dial_of_Destiny_cover_image.jpg" },
                    { new Guid("6f25ab66-2c86-4c23-bca7-2efb971dd681"), null, 2, null, "https://dl.dropboxusercontent.com/s/491y3cpjoc4ulzm/MV5BNDI3MzgwMmYtY2JjYy00ZWQ2LTgzN_profile_image.jpg" },
                    { new Guid("eaf26d38-95be-4f75-8314-2e9a9766955e"), 2, null, null, "https://dl.dropboxusercontent.com/s/oxqc24aw3xb9n9z/MV5BMTY4Mjg0NjIxOV5BMl5Ban_profile_image.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[,]
                {
                    { new Guid("198f84c5-c35d-45a0-9162-0192ccdd28e8"), null, null, 2, "https://dl.dropboxusercontent.com/s/evfvsgzlf9f4vok/1434659607842-pgv4ql-1680875129724_trailer.mp4" },
                    { new Guid("d2c25e89-4323-44a6-8b37-49403b481a75"), null, 2, null, "https://dl.dropboxusercontent.com/s/joz6wy1q4o55jz4/1434659607842-pgv4ql-1687560633004.mp4" },
                    { new Guid("e687984f-1287-4c39-91ca-20a150629c79"), 2, null, null, "https://dl.dropboxusercontent.com/s/l6151myomc65wqc/1434659607842-pgv4ql-1687556762812.mp4" }
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmIdea.Data.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("05aa3335-a1d1-4b61-bc53-ea80ac781f6f"));

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
                columns: new[] { "ConcurrencyStamp", "CriticId", "LockoutEnabled", "NormalizedEmail", "NormalizedUserName", "SecurityStamp" },
                values: new object[] { "9a2a3dd1-843c-4af6-8f63-a900b688c09d", new Guid("bf595423-7323-4e40-bd43-0f876c1beece"), true, "CRITIC@CRITIC.BG", "CRITICCRITICOV", "RCRRYTFZIKPBOLEU7PJ4QZDD7PL3EJTX" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233"),
                columns: new[] { "ConcurrencyStamp", "LockoutEnabled", "NormalizedEmail", "NormalizedUserName", "SecurityStamp" },
                values: new object[] { "bcfbbcf7-03cb-4512-94bd-04e6e38f1086", true, "USER@USER.BG", "USER182", "CPHRH5Z35EEOSFSFA6UQV2ZDTJGKCIKX" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("94756cdf-566e-4bf8-b03b-416a118ad53b"),
                columns: new[] { "ConcurrencyStamp", "CriticId", "LockoutEnabled", "NormalizedEmail", "NormalizedUserName", "SecurityStamp" },
                values: new object[] { "09fe4203-727e-4027-ab7b-354b42a5148f", new Guid("93372a0a-b9f4-4bc5-8f53-e8da0bce2bfe"), true, "ADMIN@ADMIN.COM", "ADMINISTRATOR", "NJN3O3VL2VQRL2ESBOFVB5JLFCZCJS2A" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentDate", "Content", "ReviewId", "WriterId" },
                values: new object[] { new Guid("59d4c803-cd7d-4324-882b-b505f22b1f87"), new DateTime(2023, 8, 5, 18, 42, 5, 69, DateTimeKind.Utc).AddTicks(9003), "I like the movie", new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"), new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233") });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[,]
                {
                    { new Guid("15a85343-f3f0-440d-9105-98cbf20645eb"), 2, null, null, "https://dl.dropboxusercontent.com/s/oxqc24aw3xb9n9z/MV5BMTY4Mjg0NjIxOV5BMl5Ban_profile_image.jpg" },
                    { new Guid("c6d4cd7b-fbee-4534-a99b-fd062cb7ea40"), null, null, 2, "https://dl.dropboxusercontent.com/s/kban35sz6tw0d0x/Indiana_Jones_and_the_Dial_of_Destiny_cover_image.jpg" },
                    { new Guid("e711dff0-ceb7-4072-a8f6-b409c529d23d"), null, 2, null, "https://dl.dropboxusercontent.com/s/491y3cpjoc4ulzm/MV5BNDI3MzgwMmYtY2JjYy00ZWQ2LTgzN_profile_image.jpg" }
                });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"),
                columns: new[] { "CriticId", "ReviewDate" },
                values: new object[] { new Guid("bf595423-7323-4e40-bd43-0f876c1beece"), new DateTime(2023, 8, 5, 18, 42, 5, 72, DateTimeKind.Utc).AddTicks(2547) });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[,]
                {
                    { new Guid("36be3d32-5f09-4640-bc19-840a44a07998"), null, null, 2, "https://dl.dropboxusercontent.com/s/evfvsgzlf9f4vok/1434659607842-pgv4ql-1680875129724_trailer.mp4" },
                    { new Guid("e3b502b0-f13f-4738-bdbf-f58a7945d60f"), null, 2, null, "https://dl.dropboxusercontent.com/s/joz6wy1q4o55jz4/1434659607842-pgv4ql-1687560633004.mp4" },
                    { new Guid("f4336dd3-4468-4178-825d-7a68ae8b07dc"), 2, null, null, "https://dl.dropboxusercontent.com/s/l6151myomc65wqc/1434659607842-pgv4ql-1687556762812.mp4" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("59d4c803-cd7d-4324-882b-b505f22b1f87"));

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: new Guid("15a85343-f3f0-440d-9105-98cbf20645eb"));

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: new Guid("c6d4cd7b-fbee-4534-a99b-fd062cb7ea40"));

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: new Guid("e711dff0-ceb7-4072-a8f6-b409c529d23d"));

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: new Guid("36be3d32-5f09-4640-bc19-840a44a07998"));

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: new Guid("e3b502b0-f13f-4738-bdbf-f58a7945d60f"));

            migrationBuilder.DeleteData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: new Guid("f4336dd3-4468-4178-825d-7a68ae8b07dc"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("15eb7825-840b-4528-71cc-08db8c333233"),
                columns: new[] { "ConcurrencyStamp", "CriticId", "LockoutEnabled", "NormalizedEmail", "NormalizedUserName", "SecurityStamp" },
                values: new object[] { "c88e4c8d-924b-4f30-b55e-f3e79d0e49ae", null, false, null, null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233"),
                columns: new[] { "ConcurrencyStamp", "LockoutEnabled", "NormalizedEmail", "NormalizedUserName", "SecurityStamp" },
                values: new object[] { "97b82d5d-f7ce-42d9-9c3e-91177465b575", false, null, null, null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("94756cdf-566e-4bf8-b03b-416a118ad53b"),
                columns: new[] { "ConcurrencyStamp", "CriticId", "LockoutEnabled", "NormalizedEmail", "NormalizedUserName", "SecurityStamp" },
                values: new object[] { "eda30e9f-335b-40a1-a996-fa0f11975edb", null, false, null, null, null });

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

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"),
                columns: new[] { "CriticId", "ReviewDate" },
                values: new object[] { new Guid("bcc5c503-128b-4ba6-a736-6efbeda1ee34"), new DateTime(2023, 8, 5, 17, 11, 18, 491, DateTimeKind.Utc).AddTicks(419) });

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
    }
}

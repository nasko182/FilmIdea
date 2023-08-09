using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmIdea.Data.Migrations
{
    public partial class AddReviewLikesAndDisLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("ed105431-2ec0-42c1-a611-fa2f1c4d9b0e"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("428bd710-7a98-4c58-aed5-d69df62e342f"));

            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "CriticId", "Dislikes", "Likes", "MovieId", "Rating", "ReviewDate", "Title" },
                values: new object[] { new Guid("62b7bfac-44bd-4055-ab9b-3d9a802f8162"), "I like the movie", new Guid("bcc5c503-128b-4ba6-a736-6efbeda1ee34"), 0, 0, 2, 9, new DateTime(2023, 8, 2, 7, 56, 25, 338, DateTimeKind.Utc).AddTicks(5507), "About the movie" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentDate", "Content", "ReviewId", "WriterId" },
                values: new object[] { new Guid("72413d45-d3b7-4733-af9b-e6e8d2ffd841"), new DateTime(2023, 8, 2, 7, 56, 25, 333, DateTimeKind.Utc).AddTicks(8610), "I like the movie", new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"), new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233") });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("72413d45-d3b7-4733-af9b-e6e8d2ffd841"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("62b7bfac-44bd-4055-ab9b-3d9a802f8162"));

            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Reviews");

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "CriticId", "MovieId", "Rating", "ReviewDate", "Title" },
                values: new object[] { new Guid("428bd710-7a98-4c58-aed5-d69df62e342f"), "I like the movie", new Guid("bcc5c503-128b-4ba6-a736-6efbeda1ee34"), 2, 9, new DateTime(2023, 8, 1, 19, 47, 54, 797, DateTimeKind.Utc).AddTicks(6373), "About the movie" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentDate", "Content", "ReviewId", "WriterId" },
                values: new object[] { new Guid("ed105431-2ec0-42c1-a611-fa2f1c4d9b0e"), new DateTime(2023, 8, 1, 19, 47, 54, 794, DateTimeKind.Utc).AddTicks(7780), "I like the movie", new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"), new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233") });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmIdea.Data.Migrations
{
    public partial class AddReviewTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Groups_GroupId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_GroupId",
                table: "Movies");

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("099885f0-e5dd-4c87-83f1-80ba5efab807"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"));

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Movies");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Reviews",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Reviews",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("15eb7825-840b-4528-71cc-08db8c333233"),
                column: "ConcurrencyStamp",
                value: "1dcc23dd-b27f-4f68-8dc1-70daee36ac6c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233"),
                column: "ConcurrencyStamp",
                value: "f38ec75b-ea1b-4f51-b65f-0ae1ae18aebe");

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "CriticId", "MovieId", "Rating", "ReviewDate", "Title" },
                values: new object[] { new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"), "I like the movie", new Guid("bcc5c503-128b-4ba6-a736-6efbeda1ee34"), 2, 9, new DateTime(2023, 8, 1, 19, 47, 54, 797, DateTimeKind.Utc).AddTicks(6373), "About the movie" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentDate", "Content", "ReviewId", "WriterId" },
                values: new object[] { new Guid("ed105431-2ec0-42c1-a611-fa2f1c4d9b0e"), new DateTime(2023, 8, 1, 19, 47, 54, 794, DateTimeKind.Utc).AddTicks(7780), "I like the movie", new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"), new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233") });


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("ed105431-2ec0-42c1-a611-fa2f1c4d9b0e"));

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: new Guid("428bd710-7a98-4c58-aed5-d69df62e342f"));

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Movies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("15eb7825-840b-4528-71cc-08db8c333233"),
                column: "ConcurrencyStamp",
                value: "c1a87784-f8ba-4974-9f07-11d7f2aa8713");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233"),
                column: "ConcurrencyStamp",
                value: "b6615212-9b74-4e4e-a31a-d3b6c84c1369");

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Content", "CriticId", "MovieId", "Rating", "ReviewDate"},
                values: new object[] { new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"), "I like the movie", new Guid("bcc5c503-128b-4ba6-a736-6efbeda1ee34"), 2, 9, new DateTime(2023, 8, 1, 19, 47, 54, 797, DateTimeKind.Utc).AddTicks(6373)});

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "CommentDate", "Content", "ReviewId", "WriterId" },
                values: new object[] { new Guid("991cc70d-c0aa-44c7-b6b7-ccd58d3796fa"), new DateTime(2023, 7, 31, 19, 8, 10, 476, DateTimeKind.Utc).AddTicks(4378), "I like the movie", new Guid("7dcc5bd6-1133-432b-b6a6-f6c27da75948"), new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233") });


            migrationBuilder.CreateIndex(
                name: "IX_Movies_GroupId",
                table: "Movies",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Groups_GroupId",
                table: "Movies",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}

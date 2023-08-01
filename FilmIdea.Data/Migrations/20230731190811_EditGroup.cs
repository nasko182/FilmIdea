using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmIdea.Data.Migrations
{
    public partial class EditGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupsMovies");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Groups_GroupId",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_GroupId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Movies");

            migrationBuilder.CreateTable(
                name: "GroupsMovies",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsMovies", x => new { x.GroupId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_GroupsMovies_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupsMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("15eb7825-840b-4528-71cc-08db8c333233"),
                column: "ConcurrencyStamp",
                value: "d2cd04ab-e081-4ee4-af8b-1ef6f421f086");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233"),
                column: "ConcurrencyStamp",
                value: "4a9d071b-48cc-42ba-9f31-3c33713715c2");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsMovies_MovieId",
                table: "GroupsMovies",
                column: "MovieId");
        }
    }
}

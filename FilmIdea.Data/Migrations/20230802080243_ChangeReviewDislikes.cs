using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmIdea.Data.Migrations
{
    public partial class ChangeReviewDislikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "Dislikes",
                table: "Reviews",
                newName: "Dislikes");

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "Dislikes",
                table: "Reviews",
                newName: "Dislikes");

           
        }
    }
}

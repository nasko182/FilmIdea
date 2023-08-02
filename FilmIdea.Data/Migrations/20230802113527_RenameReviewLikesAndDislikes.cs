using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmIdea.Data.Migrations
{
    public partial class RenameReviewLikesAndDislikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dislike_AspNetUsers_UserId",
                table: "Dislike");

            migrationBuilder.DropForeignKey(
                name: "FK_Dislike_Reviews_ReviewId",
                table: "Dislike");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_AspNetUsers_UserId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_Reviews_ReviewId",
                table: "Like");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                table: "Like");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dislike",
                table: "Dislike");

            migrationBuilder.RenameTable(
                name: "Like",
                newName: "Likes");

            migrationBuilder.RenameTable(
                name: "Dislike",
                newName: "Dislikes");

            migrationBuilder.RenameIndex(
                name: "IX_Like_UserId",
                table: "Likes",
                newName: "IX_Likes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Like_ReviewId",
                table: "Likes",
                newName: "IX_Likes_ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Dislike_UserId",
                table: "Dislikes",
                newName: "IX_Dislikes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Dislike_ReviewId",
                table: "Dislikes",
                newName: "IX_Dislikes_ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dislikes",
                table: "Dislikes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dislikes_AspNetUsers_UserId",
                table: "Dislikes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Dislikes_Reviews_ReviewId",
                table: "Dislikes",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Reviews_ReviewId",
                table: "Likes",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dislikes_AspNetUsers_UserId",
                table: "Dislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Dislikes_Reviews_ReviewId",
                table: "Dislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_UserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Reviews_ReviewId",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dislikes",
                table: "Dislikes");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "Like");

            migrationBuilder.RenameTable(
                name: "Dislikes",
                newName: "Dislike");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_UserId",
                table: "Like",
                newName: "IX_Like_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Likes_ReviewId",
                table: "Like",
                newName: "IX_Like_ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Dislikes_UserId",
                table: "Dislike",
                newName: "IX_Dislike_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Dislikes_ReviewId",
                table: "Dislike",
                newName: "IX_Dislike_ReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                table: "Like",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dislike",
                table: "Dislike",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dislike_AspNetUsers_UserId",
                table: "Dislike",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Dislike_Reviews_ReviewId",
                table: "Dislike",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_AspNetUsers_UserId",
                table: "Like",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Reviews_ReviewId",
                table: "Like",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

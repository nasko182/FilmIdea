using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmIdea.Data.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(105)", maxLength: 105, nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Critics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(105)", maxLength: 105, nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Critics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(105)", maxLength: 105, nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CriticId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Critics_CriticId",
                        column: x => x.CriticId,
                        principalTable: "Critics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    TrailerUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    DirectorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_Directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupsUsers",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsUsers", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_GroupsUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupsUsers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MoviesActors",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    ActorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesActors", x => new { x.MovieId, x.ActorId });
                    table.ForeignKey(
                        name: "FK_MoviesActors_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MoviesActors_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MoviesGenres",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviesGenres", x => new { x.MovieId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_MoviesGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MoviesGenres_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PassedMovies",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassedMovies", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_PassedMovies_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PassedMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    ActorId = table.Column<int>(type: "int", nullable: true),
                    DirectorId = table.Column<int>(type: "int", nullable: true),
                    MovieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_Directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    CriticId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Critics_CriticId",
                        column: x => x.CriticId,
                        principalTable: "Critics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRatings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRatings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRatings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersMovies",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersMovies", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_UsersMovies_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    ActorId = table.Column<int>(type: "int", nullable: true),
                    DirectorId = table.Column<int>(type: "int", nullable: true),
                    MovieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Videos_Directors_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Videos_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CommentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WriterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_WriterId",
                        column: x => x.WriterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dislikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dislikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dislikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dislikes_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Likes_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "Bio", "DateOfBirth", "Name", "ProfileImageUrl" },
                values: new object[,]
                {
                    { 1, "Ezra Matthew Miller was born in Wyckoff, New Jersey, to Marta (Koch), a modern dancer, and Robert S. Miller, who has worked at Workman Publishing and as former senior V.P. for Hyperion Books. Ezra has two older sisters and is of Ashkenazi Jewish (father) and German-Dutch (mother) ancestry. Ezra has described themselves as Jewish and \"spiritual\".\r\n\r\nAs a child, Miller sang with the Metropolitan Opera and attended Rockland Country Day School and The Hudson School. Miller's first feature film was the independent Afterschool (2008), with subsequent appearances on the television series Секс до дупка (2007), Закон и ред: Специални разследвания (1999), and Лекар на повикване (2009), and in the films City Island (2009), Всеки ден (2010), Beware the Gonzo (2010), and Another Happy Day (2011).\r\n\r\nMiller drew critical praise playing Kevin Khatchadourian, the homicidal son of Tilda Swinton's character, in the dramatic thriller Трябва да поговорим за Кевин (2011). Miller subsequently played Patrick in the well-received teen drama Предимствата да бъдеш аутсайдер (2012), opposite Logan Lerman and Emma Watson.\r\n\r\nEzra's other roles include the period piece Мадам Бовари (2014), Judd Apatow's comedy Тотал щета (2015), and the psychological thriller The Stanford Prison Experiment (2015). Miller has been cast as superhero The Flash in Светкавицата (2023), scheduled for release in 2022.", new DateTime(1992, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ezra Miller", "https://dl.dropboxusercontent.com/s/aync8kuoks5ii3f/Something_profile_image.jpg" },
                    { 2, "Harrison Ford was born on July 13, 1942 in Chicago, Illinois, to Dorothy (Nidelman), a radio actress, and Christopher Ford (born John William Ford), an actor turned advertising executive. His father was of Irish and German ancestry, while his maternal grandparents were Jewish emigrants from Minsk, Belarus. Harrison was a lackluster student at Maine Township High School East in Park Ridge Illinois (no athletic star, never above a C average). After dropping out of Ripon College in Wisconsin, where he did some acting and later summer stock, he signed a Hollywood contract with Columbia and later Universal. His roles in movies and television (Ironside (1967), The Virginian (1962)) remained secondary and, discouraged, he turned to a career in professional carpentry. He came back big four years later, however, as Bob Falfa in Американски графити (1973). Four years after that, he hit colossal with the role of Han Solo in Междузвездни войни: Епизод IV - Нова Надежда (1977). Another four years and Ford was Indiana Jones in Похитители на изчезналия кивот (1981).\r\n\r\nFour years later and he received Academy Award and Golden Globe nominations for his role as John Book in Свидетел (1985). All he managed four years after that was his third starring success as Indiana Jones; in fact, many of his earlier successful roles led to sequels as did his more recent portrayal of Jack Ryan in Патриотични игри (1992). Another Golden Globe nomination came his way for the part of Dr. Richard Kimble in Беглецът (1993). He is clearly a well-established Hollywood superstar. He also maintains an 800-acre ranch in Jackson Hole, Wyoming.\r\n\r\nFord is a private pilot of both fixed-wing aircraft and helicopters, and owns an 800-acre (3.2 km2) ranch in Jackson, Wyoming, approximately half of which he has donated as a nature reserve. On several occasions, Ford has personally provided emergency helicopter services at the request of local authorities, in one instance rescuing a hiker overcome by dehydration. Ford began flight training in the 1960s at Wild Rose Idlewild Airport in Wild Rose, Wisconsin, flying in a Piper PA-22 Tri-Pacer, but at $15 an hour, he could not afford to continue the training. In the mid-1990s, he bought a used Gulfstream II and asked one of his pilots, Terry Bender, to give him flying lessons. They started flying a Cessna 182 out of Jackson, Wyoming, later switching to Teterboro, New Jersey, flying a Cessna 206, the aircraft he soloed in. Ford is an honorary board member of the humanitarian aviation organization Wings of Hope.\r\n\r\nOn March 5, 2015, Ford's plane, believed to be a Ryan PT-22 Recruit, made an emergency landing on the Penmar Golf Course in Venice, California. Ford had radioed in to report that the plane had suffered engine failure. He was taken to Ronald Reagan UCLA Medical Center, where he was reported to be in fair to moderate condition. Ford suffered a broken pelvis and broken ankle during the accident, as well as other injuries.", new DateTime(1942, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harrison Ford", "https://dl.dropboxusercontent.com/s/aync8kuoks5ii3f/Something_profile_image.jpg" },
                    { 1001, "Thomas Stanley Holland was born in Kingston-upon-Thames, Surrey, to Nicola Elizabeth (Frost), a photographer, and Dominic Holland (Dominic Anthony Holland), who is a comedian and author. His paternal grandparents were from the Isle of Man and Ireland, respectively. He lives with his parents and three younger brothers - Paddy and twins Sam and Harry. Tom attended Donhead Prep School. Then, after a successful eleven plus exam, he became a pupil at Wimbledon College. Having successfully completed his GCSEs, in September 2012 Tom started a two-year course in the BRIT School for Performing Arts & Technology notable for its numerous famous alumni.\r\n\r\nHolland began dancing at a hip hop class at Nifty Feet Dance School in Wimbledon, London. His potential was spotted by choreographer Lynne Page (who was an Associate to Peter Darling, choreographer of Billy Elliot and Billy Elliot the Musical) when he performed with his dance school as part of the Richmond Dance Festival 2006. After eight auditions and subsequent two years of training, on 28 June 2008 Tom made his West End debut in Billy Elliot the Musical as Michael, Billy's best friend. He gave his first performance in the title role of Billy on 8 September 2008 getting rave reviews praising his versatile acting and dancing skills.\r\n\r\nIn September 2008 Tom (together with co-star Tanner Pflueger) appeared on the news programme on channel FIVE and gave his first TV interview. In 2009 Tom was featured on ITV1 show \"The Feel Good Factor\". At the launch show on 31 January he and two other Billy Elliots, Tanner Pflueger and Layton Williams, performed a specially choreographered version of Angry Dance from Billy Elliot the Musical, after which Tom was interviewed by host Myleene Klass. Then he became involved into training five ordinary British schoolboys learning to get fit and preparing their dance routine (fronted by Tom) for the final \"The Feel Good Factor\" show on 28 March 2009. On 11 March 2010, Tom, along with fellow Billy Elliots Dean-Charles Chapman and Fox Jackson-Keen appeared on The Alan Titchmarsh Show on ITV1.\r\n\r\nOn 8 March 2010, to mark the fifth anniversary of Billy Elliot the Musical, four current Billy Elliots, including Tom Holland, were invited to 10 Downing Street to meet the Prime Minister Gordon Brown. It was Tom Holland who was chosen to be a lead at the special fifth anniversary show on 31 March 2010. Elton John, Billy Elliot the Musical composer, who was at the audience, called Tom's performance \"astonishing\" and said that he was \"blown away\" by it. Holland had been appearing on a regular basis as Billy in Billy Elliot the Musical rotating with three other performers till 29 May 2010 when he finished his run in the musical.\r\n\r\nIn two months after leaving Billy Elliot the Musical, Holland successfully auditioned for a starring role in the film The Impossible (directed by Juan Antonio Bayona) alongside Naomi Watts and Ewan McGregor. The Impossible was based on a true story that took place during the 2004 Indian Ocean earthquake. The film premiered at the Toronto International Film Festival on September 9, 2012, and was released in Europe in October 2012, and in North America in December 2012.\r\n\r\nTom has received universal praise for his performance, in particular: \"What a debut, too, from Tom Holland as the eldest of their three lads\" (The Telegraph); \"Tom Holland, making one of the finest feature debuts in years\" (HeyUGuys); \"the excellent Tom Holland\" (The Guardian); \"The child performers are uncanny and there is an especially terrific performance from Tom Holland as the resourceful, levelheaded Lucas terrified but tenacious in the face of an unspeakable ordeal\" (Screen Daily); \"Young Holland in particular is astonishingly good as the terrified but courageous Lucas.\" (The Hollywood Reporter); \"However, the real acting standout in The Impossible is the performance of Tom Holland as the eldest son Lucas. His portrayal is genuine, and at no moment does it feel melodramatic and forced. The majority of his scenes are separate from the lead actors and for the most part it feels like The Impossible is Holland's film\" (Entertainment Maven); \"Mr. Holland, meanwhile, matures before our eyes, navigating the passage from adolescent self-absorption to profound and terrible responsibility. He is a terrific young actor\" (New York Times).\r\n\r\nTom has given a number of interviews about his role in The Impossible. In particular, he talked on video to Vanity Fair Senior West Coast editor Krista Smith and with IAMROGUE's Managing Editor Jami Philbrick. He has also given interviews to The Hollywood Reporter, to the MovieWeb, to Today Show on NBC and to other outlets. Tom's director and co-stars have also talked about him. Juan Antonio Bayona: \"He had this extraordinary ability to get into the emotion and portray it in a very, very easy way. The best I'd ever seen in a kid.\" Ewan McGregor: \"It was wonderful watching Tom who had never worked in front of a camera before, to see him really get it and grow as a film actor as he went along. He's really talented and polite to everyone. It's very easy for children to lose perspective but he's absolutely on the right road and a brilliant actor.\" Naomi Watts: \"He has an incredible emotional instrument and an unbelievable sense of himself... Tom Holland and I had a couple of moments where we came together and I could just tell how wonderful he was and what a beautiful instrument he had. It was just easy to work with him, that was one of the greatest highlights for me: discovering a friendship with Tom off-screen and this beautiful relationship between mother and son on-screen. The intimacy that develops through the course of the film between Lucas and Maria, I just loved that relationship. I mean, Tom is a beyond gifted actor. He's just a raw, open talent that is just so easy to work with. And Tom, he's inspiring, he kind of lifts everyone's game around him because he can do nothing but tell the truth. He was great.\"", new DateTime(1996, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tom Holland", "https://dl.dropboxusercontent.com/scl/fi/qxam6ib8y8qm78k3u230n/Tom-Holland_ProfileImageImage.jpg?dl=0&rlkey=qxqp5gxawx1hmwosz51m1lavo" },
                    { 1007, "Robert Downey Jr. has evolved into one of the most respected actors in Hollywood. With an amazing list of credits to his name, he has managed to stay new and fresh even after over four decades in the business.\r\n\r\nDowney was born April 4, 1965 in Manhattan, New York, the son of writer, director and filmographer Robert Downey Sr. and actress Elsie Downey (née Elsie Ann Ford). Robert's father is of half Lithuanian Jewish, one quarter Hungarian Jewish, and one quarter Irish, descent, while Robert's mother was of English, Scottish, German, and Swiss-German ancestry. Robert and his sister, Allyson Downey, were immersed in film and the performing arts from a very young age, leading Downey Jr. to study at the Stagedoor Manor Performing Arts Training Center in upstate New York, before moving to California with his father following his parents' 1978 divorce. In 1982, he dropped out of Santa Monica High School to pursue acting full time. Downey Sr., himself a drug addict, exposed his son to drugs at a very early age, and Downey Jr. would go on to struggle with abuse for decades.\r\n\r\nDowney Jr. made his debut as an actor at the age of five in the film Pound (1970), written and directed by his father, Robert Downey Sr.. He built his film repertoire throughout the 1980s and 1990s with roles in Герой Бунтовник (1985), Нечиста наука (1985), The Fanatic (1989) , and Wonder Boys (2000) among many others. In 1992, Downey received an Academy Award nomination and won the BAFTA (British Academy Award) for Best Actor for his performance in the title role of Chaplin (1992) .\r\n\r\nIn Robert Altman 's Shortcuts (1993) , he appeared as an aspiring film make-up artist whose best friend commits murder. In Oliver Stone 's Born Killers (1994) , with Woody Harrelson and Juliette Lewis , Downey starred as a tabloid TV journalist who exploits a murderous couple's killing spree to boost his ratings. For the comedy Heart and Soul (1993), Downey starred as a young man with a special relationship with four ghosts. In 1995, Downey starred in Restoration (1995) , with Hugh Grant , Meg Ryan and Ian McKellen , directed by Michael Hoffman . Also that year, he starred in Richard III (1995) , in which he appears opposite his Restoration (1995) co-star McKellen.\r\n\r\nIn 1997, Downey was seen in Robert Altman 's The Bread Man (1998) , alongside Kenneth Branagh , Daryl Hannah and Embeth Davidtz ; in One Night Flirting (1997) , directed byMike Figgis and starring Wesley Snipes and Nastassja Kinski; and in Басейнът на Хюго (1997), directed by his father, Robert Downey Sr. and starring Sean Penn and Patrick Dempsey. In September of 1999, Downey appeared in Чeрно и бяло (1999), written and directed by James Toback, along with Ben Stiller, Elijah Wood, Gaby Hoffmann, Brooke Shields and Claudia Schiffer. In January of 1999, he starred with Annette Bening and Aidan Quinn in Видението (1999), directed by Neil Jordan.\r\n\r\nIn 2000, Downey co-starred with Michael Douglas and Tobey Maguire in Момчета-чудо (2000), directed by Curtis Hanson. In this dramatic comedy, Downey played the role of a bisexual literary agent. In 2001, Downey made his prime-time television debut when he joined the cast of the Fox-TV series Али Макбийл (1997) as attorney \"Larry Paul\". For this role, he won the Golden Globe Award for Best Performance by an Actor in a Supporting Role in a Series, Mini-Series or Motion Picture Made for Television, as well as the Screen Actors Guild Award for Outstanding Performance by a Male in a Comedy Series. In addition, Downey was nominated for an Emmy for Outstanding Supporting Actor in a Comedy Series.\r\n\r\nThe actor's drug-related problems escalated from 1996 to 2001, leading to arrests, rehab visits and incarcerations, and he was eventually fired from Али Макбийл (1997). Emerging clean and sober in 2003, Downey Jr. began to rebuild his career.\r\n\r\nHe marked his debut into music with his debut album, titled \"The Futurist\", on the Sony Classics Label on November 23rd, 2004. The album's eight original songs, that Downey wrote, and his two musical numbers debuting as cover songs revealed his sultry singing voice and his musical talents. Downey displayed his versatility in two different films in October 2003: the musical/drama The Singing Detective (2003), a remake of the BBC hit of the same name, and the thriller Готика (2003) starring Halle Berry and Penélope Cruz. Downey starred in powerful yet humbling roles inspired by real-life accounts of some of history's most precious kept secrets, including Richard Linklater's Камера потъмняла (2006)in 2006 co-starring Keanu Reeves , Winona Ryder and Woody Harrelson , and Kozina: Imaginary Portrait of Diane Arbus (2006) co-starring Nicole Kidman , a film inspired by the life of Diane Arbus, the revered photographer whose images captured attention in the early 1960s. These roles exhibited Downey's momentum from the previous year of 2005, in which he starred in the Academy Award®-nominated feature film Good Night and Luck (2005) , directed by George Clooney and in Shane Black 's action comedy Kisses with an Unexpected End ( 2005) co-starring Val Kilmer . In 2007, he co-starred inDavid Fincher's suspenseful Зодиак (2007), alongside Jake Gyllenhaal and Mark Ruffalo, about the notorious serial killer who haunted San Francisco during the 1970s.\r\n\r\nIn May 2008, Downey achieved critical acclaim and worldwide box office success for his starring role in Железният човек (2008), Jon Favreau's big-screen rendering of the Marvel comic book superhero. The film co-starred Gwyneth Paltrow, Jeff Bridges and Terrence Howard. In August of 2008, Downey starred with Ben Stiller and Jack Black in the comedy Тропическа буря (2008), and went on to receive an Academy Award®-nomination for Best Supporting Actor for his, Kirk Lazarus.\r\n\r\nIn December 2009, Downey starred in the action-adventure Шерлок Холмс (2009). The film, directed by Guy Ritchie, co-starred Jude Law and Rachel McAdams and earned Downey a Golden Globe for Best Performance by an Actor in a Motion Picture - Comedy or Musical in January of 2010. In early Summer 2010, Downey re-teamed with director Jon Favreau and reprised his role as \"Tony Stark/Iron Man\" in the hugely successful sequel to the original film, Железният човек 2 (2010), starring Gwyneth Paltrow, Scarlett Johansson, Samuel L. Jackson and Mickey Rourke .\r\n\r\nDowney next starred in Road with an Advantage (2010) , a comedy directed by Todd Phillips , in which he plays the role of an expectant father on a road trip racing to get back in time for the birth of his first child. Lane of Precedence (2010) , starring The Last Bachelor Party (2009) 's Zach Galifianakis , was released in November 2010.\r\n\r\nDowney was honored by Time Magazine's \"Time 100\" in 2008, an annual list of the 100 most influential people in the world. His laurels include two Academy Award nominations, three Golden Globe wins, numerous other award nominations and wins, and tremendous popular and commercial success, particularly in his roles as Sherlock Holmes and Tony Stark (the latter of which he has so far played in Iron Man (2008) , Iron Man 2 (2010) , The Avengers (2012) , Iron Man 3 (2013) , and Avengers: Age of Ultron (2015). For three consecutive years, from 2012 to 2015, Downey has topped the Forbes list of Hollywood's highest-paid actors, making an estimated $80 million in earnings between June 2014 and June 2015.", new DateTime(1965, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Robert Downey Jr.", "https://dl.dropboxusercontent.com/scl/fi/xen6j6nfxtx59ho0ud79f/Robert-Downey-Jr._ProfileImageImage.jpg" },
                    { 1009, "Margot Elise Robbie was born on July 2, 1990 in Dalby, Queensland, Australia to Scottish parents. Her mother, Sarie Kessler, is a physiotherapist, and her father, is Doug Robbie. She comes from a family of four children, having two brothers and one sister. She graduated from Somerset College in Mudgeeraba, Queensland, Australia, a suburb in the Gold Coast hinterland of South East Queensland, where she and her siblings were raised by their mother and spent much of her time at the farm belonging to her grandparents. In her late teens, she moved to Melbourne, Victoria, Australia to pursue an acting career.\r\n\r\nFrom 2008-2010, Robbie played the character of Donna Freedman in the long-running Australian soap opera, Neighbours (1985), for which she was nominated for two Logie Awards. She set off to pursue Hollywood opportunities, quickly landing the role of Laura Cameron on the short-lived ABC series, Pan Am (2011). She made her big screen debut in the film, Въпрос на време (2013).\r\n\r\nRobbie rose to fame co-starring alongside Leonardo DiCaprio, portraying the role of Naomi Lapaglia in Martin Scorsese's Oscar nominated film, Вълкът от Уолстрийт (2013). She was nominated for a Breakthrough Performance MTV Movie Award, and numerous other awards.\r\n\r\nIn 2014, Robbie founded her own production company, LuckyChap Entertainment. She also appeared in the World War II romantic-drama film, Френска сюита (2014). She starred in Focus (2015) and Z for Zachariah (2015) , and made a cameo in The Big Bet (2015) .\r\n\r\nIn 2016, she married Tom Ackerley in Byron Bay, New South Wales, Australia.\r\n\r\nShe starred as Jane Porter in The Legend of Tarzan (2016) , Tanya Vanderpoel in Whiskey, Tango, Foxtrot (2016) and as DC comics villain Harley Quinn in Suicide Squad (2016) , for which she was nominated for a Teen Choice Award, and many other awards.\r\n\r\nShe portrayed figure skater Tonya Harding in the biographical film I, Tonya (2017), receiving critical acclaim and a Golden Globe Award nomination for Best Actress - Motion Picture Comedy or Musical.", new DateTime(1990, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Margot Robbie", "https://dl.dropboxusercontent.com/scl/fi/10unao6fvgmo15dgqatr9/Margot-Robbie_ProfileImageImage.jpg?rlkey=upttbqbm9bedq7oiy9o3ogquk" },
                    { 1012, "Born Ryan Thomas Gosling on November 12, 1980, in London, Ontario, Canada, he is the son of Donna (Wilson), a secretary, and Thomas Ray Gosling, a traveling salesman. Ryan was the second of their two children, with an older sister, Mandi. His ancestry is French-Canadian, as well as English, Scottish, and Irish. The Gosling family moved to Cornwall, Ontario, where Ryan grew up and was home-schooled by his mother. He also attended Gladstone Public School, Cornwall Collegiate, and Vocational High School in Cornwall, where he excelled in Drama and Fine Arts. The family then relocated to Burlington, Ontario, where Ryan attended Lester B. Pearson High School.\r\n\r\nRyan first performed as a singer at talent contests with Mandi. He attended an open audition in Montreal for the TV series \"The Mickey Mouse Club\" (MMC (1989)) in January 1993 and beat out 17,000 other aspiring actors for a a spot on the show. While appearing on \"MMC\" for two years, he lived with co-star Justin Timberlake's family.\r\n\r\nThough he received no formal acting training, after \"MMC,\" Gosling segued into an acting career, appearing on the TV series Young Hercules (1998) and Breaker High (1997), as well as the films The Slaughter Rule (2002), Убийство по учебник (2002), and Помни титаните (2000). He first attracted serious critical attention with his performance as the Jewish neo-Nazi in the controversial film Твърда вяра (2001), which won the Grand Jury Prize at the 2001 Sundance Film Festival. He was cast in the part by writer-director Henry Bean, who believed that Gosling's strict upbringing gave him the insight to understand the character Danny, whose obsessiveness with the Judaism he was born into turns to hatred. He was nominated for an Independent Spirit Award as Best Male Lead in 2002 for the role and won the Golden Aries award from the Russian Guild of Film Critics.\r\n\r\nAfter appearing in the sleeper Тетрадката (2004) in 2004, Gosling won the dubious honor of being named one of the 50 Hottest Bachelors by People Magazine. More significantly, he was named the Male Star of Tomorrow at the 2004 Show West convention of movie exhibitors.\r\n\r\nGosling reached a summit of his profession with his performance in Half Nelson (2006) , which garnered him an Academy Award nomination as Best Actor. In a short time, he has established himself as one of the finest actors of his generation. Throughout the subsequent decade, he has become all three of an internet fixation, a box office star, and a critical darling, having headlined Blue Valentine (2010) , Stupid of Love (2011) , Drive: Life at Speed ​​(2011) , The Mask of Power (2011) , The Place Beyond the Trees (2012) , The Kind Guys (2016) , and La La Land (2016). In 2017, he starred in the long-awaited science fiction sequel Блейд Рънър 2049 (2017), with Harrison Ford.", new DateTime(1980, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ryan Gosling", "https://dl.dropboxusercontent.com/scl/fi/8tadfvvsbj80q25z3ircc/Ryan-Gosling_ProfileImage.jpg?rlkey=77yl6oquxsszz9lglpkhurzvk" },
                    { 1013, "Few actors in the world have had a career quite as diverse as Leonardo DiCaprio's. DiCaprio has gone from relatively humble beginnings, as a supporting cast member of the sitcom Growing Pains (1985) and low budget horror movies, such as,to a major teenage heartthrob in the 1990s, as the hunky lead actor in movies such to then become a leading man in Hollywood blockbusters, made by internationally renowned directors such as Martin Scorsese and Christopher Nolan.\r\n\r\nLeonardo Wilhelm DiCaprio was born November 11, 1974 in Los Angeles, California, the only child of Irmelin DiCaprio (née Indenbirken) and former comic book artist George DiCaprio. His father is of Italian and German descent, and his mother, who is German-born, is of German and Russian ancestry. His middle name, \"Wilhelm\", was his maternal grandfather's first name. Leonardo's father had achieved minor status as an artist and distributor of cult comic book titles, and was even depicted in several issues of American Splendor, the cult semi-autobiographical comic book series by the late 'Harvey Pekar', a friend of George's. Leonardo's performance skills became obvious to his parents early on, and after signing him up with a talent agent who wanted Leonardo to perform under the stage name \"Lenny Williams\", DiCaprio began appearing on a number of television commercials and educational programs.\r\n\r\nDiCaprio began attracting the attention of producers, who cast him in small roles in a number of television series, such as Розан (1988) and The New Lassie (1989), but it wasn't until 1991 that DiCaprio made his film debut in Критърси 3 (1991), a low-budget horror movie. While Критърси 3 (1991) did little to help showcase DiCaprio's acting abilities, it did help him develop his show-reel, and attract the attention of the people behind the hit sitcom Growing Pains (1985), in which Leonardo was cast in the \"Cousin Oliver\" role of a young homeless boy who moves in with the Seavers. While DiCaprio's stint on Growing Pains (1985) was very short, as the sitcom was axed the year after he joined, it helped bring DiCaprio into the public's attention and, after the sitcom ended, DiCaprio began auditioning for roles in which he would get the chance to prove his acting chops.", new DateTime(1974, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Leonardo DiCaprio", "https://dl.dropboxusercontent.com/scl/fi/bx3oo34x7f54kugay89ol/Leonardo-DiCaprio_ProfileImage.jpg?rlkey=t94alukb9h16ukk4lcvito4r2" },
                    { 1014, "Christopher Michael \"Chris\" Pratt was born on June 21, 1979 in Virginia, Minnesota and raised in Lake Stevens, Washington, to Kathleen Louise (Indahl), who worked at a supermarket, and Daniel Clifton Pratt, who remodeled houses. He is of mostly Norwegian descent. He graduated from Lake Stevens High School in 1997, and has two older siblings, Cully and Angie.\r\n\r\nChris came to prominence for his small-screen roles, including Bright Abbott in Everwood (2002)", new DateTime(1979, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chris Pratt", "https://dl.dropboxusercontent.com/scl/fi/kmna812bb97h1ndvuenwy/Chris-Pratt_ProfileImage.jpg?rlkey=gnbt948o3re8rn43xb9yenas8" },
                    { 1015, "Chadwick Boseman was an American actor. He is known for his portrayal of T'Challa / Black Panther in the Marvel Cinematic Universe from 2016 to 2019, particularly in Black Panther (2018) , and for his starring roles as several pioneering Americans, Jackie Robinson in Number 42 (2013 ) , James Brown in The James Brown Story (2014) , and Thurgood Marshall in Marshall (2017) . He also had choice parts in The Ernie Davis Story (2008) , Selection Day (2014) , and Message from the King (2016). Born in Anderson, South Carolina, he attended Howard University and studied at the Oxford Mid-Summer Program for acting, before moving to Los Angeles in 2008 to pursue his craft on the big screen. He died in 2020, after a four year bout with colon cancer, during which time he had starred in several of the biggest movies ever made.", new DateTime(1976, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chadwick Boseman", "https://dl.dropboxusercontent.com/scl/fi/4qi8szlxxxa7d292nny4t/Chadwick-Boseman_ProfileImage.jpg?rlkey=j1f2zhss3woy2gugwhb3txvc4" },
                    { 1016, "Vin Diesel was born Mark Sinclair in Alameda County, California, along with his fraternal twin brother, Paul Vincent. He was raised by his astrologer/psychologist mother, Delora Sherleen (Sinclair), and adoptive father, Irving H. Vincent, an acting instructor and theatre manager, in an artists' housing project in New York City's Greenwich Village. He never knew his biological father. His mother is white (with English, German, Scottish, and Irish ancestry), and his adoptive father is African-American; referring to his biological father's background, Diesel has said that he himself is \"definitely a person of colour\".\r\n\r\nHis first break in acting happened by chance, when at the age of seven he and his friends broke into a theatre to vandalize it. A woman stopped them and offered them each a script and $20, on the condition that they would attend everyday after school. From there, Vin's fledgling career progressed from the New York repertory company run by his father, to the Off-Off-Broadway circuit. At age seventeen and already sporting a well-honed physique, he became a bouncer at some of New York's hippest clubs to earn himself some extra cash. It was at this time that he changed his name to Vin Diesel.\r\n\r\nFollowing high school, Vin enrolled as an English major at Hunter College, but dropped out after three years to go to Hollywood to further his acting career. Being an experienced theatre actor did not make any impression in Hollywood and after a year of struggling to make his mark, he returned to New York. His mother then gave him a book called \"Feature Films at used Car Prices\" by Rick Schmidt. The book showed him that he could take control of his career and make his own movies. He wrote a short film based on his own experiences as an actor, called Multi-Facial (1995), which was shot in less than three days at a cost of $3,000. Multi-Facial (1995) was eventually accepted for the 1995 Cannes Film Festival where it got a tumultuous reception.\r\n\r\nAfterwards, Vin returned to Los Angeles and raised almost $50,000 through telemarketing to fund the making of his first feature, Бездомници (1997). Six months after shooting, the film was accepted for the 1997 Sundance Film Festival, and although it received a good reception, it did not sell as well as hoped. Yet again Vin returned disappointed to New York only to receive a dream phone call. Steven Spielberg was impressed by Multi-Facial (1995) and wanted to meet Vin, leading him to be cast in Спасяването на редник Райън (1998). Multi-Facial (1995) earned Vin more work, when the director of Железният гигант (1999) saw it and decided to cast Vin in the title role. From there, Vin's career steadily grew, with him securing his first lead role, as Richard B. Riddick in the sci-fi film Пълен мрак (2000). The role has earned him a legion of devoted fans and the public recognition he deserves.", new DateTime(1967, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vin Diesel", "https://dl.dropboxusercontent.com/scl/fi/d5f15vpsm1cun1p15zur9/Vin-Diesel_ProfileImage.jpg?rlkey=ekwff4xuw8zncn862p64syqpc" },
                    { 1017, "Striking Irish actor Cillian Murphy was born in Douglas, the oldest child of Brendan Murphy, who works for the Irish Department of Education, and a mother who is a teacher of French. He has three younger siblings. Murphy was educated at Presentation Brothers College, Cork. He went on to study law at University College Cork, but dropped out after about a year. During this time Murphy also pursued an interest in music, playing guitar in various bands. Upon leaving University, Murphy joined the Corcadorca Theater Company in Cork, and played the lead role in \"Disco Pigs\", amongst other plays.\r\n\r\nVarious film roles followed, including a film adaptation of Disco Pigs (2001). However, his big film break came when he was cast in Danny Boyle's 28 дни по-късно (2002), which became a surprise international hit. This performance earned him nominations for Best Newcomer at the Empire Awards and Breakthrough Male Performance at the MTV Movie Awards.\r\n\r\nMurphy went on to supporting roles in high-profile films such as Cold Mountain (2003) and The Girl with the Pearl Earring (2003) , and then was cast in two villain roles: Dr. Jonathan Crane, aka The Scarecrow, in Batman Begins (2005) and Jackson Rippner in Night Flight (2005). Although slight in nature for a villain, Murphy's piercing blue eyes helped to create creepy performances and critics began to take notice. Manhola Dargis of the New York Times cited Murphy as a \"picture-perfect villain\", while David Denby of The New Yorker noted he was both \"seductive\" and \"sinister\".\r\n\r\nLater that year, Murphy starred as Patrick \"Kitten\" Braden, an Irish transgender woman in search of her mother, in Neil Jordan's Закуска на Плутон (2005), a film adaptation of the Pat McCabe novel. Although the film was not a box office success, Murphy was nominated for a Golden Globes for Best Actor in a Comedy or Musical and he won Best Actor for the Irish Film and Television Academy Awards.\r\n\r\nThe following year, Murphy starred in Ken Loach's Вятърът в ечемичените ниви (2006). The film was the most successful independent Irish film and won the Palm D'Or at the 2006 Cannes Film Festival. Murphy continued to take roles in a number of independent films, and also reprised his role as the Scarecrow in Christopher Nolan's Черният рицар (2008). Nolan is known for working with actors in multiple films, and cast Murphy in Генезис (2010), as Robert Fischer, the young heir of the multi-billion dollar empire, who was the target of DiCaprio's dream team. His most well-known work is starring as Thomas Shelby in the British TV show Peaky Blinders beginning in 2013.\r\n\r\nMurphy continues to appear in high profile films such as Time Dealers (2011) , Red Light (2012) , and The Dark Knight Rises (2012) , the final film in Nolan's Batman trilogy.\r\n\r\nMurphy is married to Yvonne McGuinness, an artist. The couple have two sons, Malachy and Aran.", new DateTime(1976, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cillian Murphy", "https://dl.dropboxusercontent.com/scl/fi/z5r62ldp5v487racpwcjl/Cillian-Murphy_ProfileImage.jpg?rlkey=jl8fen39pczosvoi1kdtfa8e1" },
                    { 1018, "Zach Galifianakis was born in Wilkesboro, North Carolina, to Mary Frances (Cashion), who owned a community arts center, and Harry Galifianakis, a heating oil vendor. His father is of Greek descent and his mother is of mostly English and Scottish ancestry. Zach moved to New York City after failing his last college class by one point. Zach got his start performing his brand of humor in the back of a hamburger joint in Times Square. He toured the country, performing in coffee shops and universities.\r\n\r\nAfter more than a decade performing stand-up and making both television and film appearances, Zach broke through to wider recognition with his co-starring role as \"Alan Garner\", in the comedy mega-hit, Последният ергенски запой (2009). Later that year, he played a large role in the CGI-heavy kids movie, G-Force: Special Squad (2009) , and then appeared in memorable supporting parts in the films, High in the Sky (2009) (as a laid-off employee), Youth in Revolt (2009) (as a loutish stepfather), and Dinner for Idiots (2010) , as one of the title characters. More recently, he co-starred with Keir Gilchrist in the teen dramedy, Something Like a Funny Story (2010) , with Robert Downey Jr. in the road trip comedy, Road with Advantage (2010) , and alongside Will Ferrell in the political spoof, The Campaign (2012). He also voiced \"Humpty Dumpty\" in the animated film, Puss in Boots (2011) , and reprized his character in both The Bachelor Party (2011) and The Bachelor Party Part III (2013) . In 2014, he appeared in the winner of the Academy Award for Best Picture, Birdman, or the Unexpected Virtue of Ignorance (2014) , and in 2016, he starred in the comedies Bash Robbers (2016) and The Joneses (2016) , released three weeks apart.\r\n\r\nWhen not performing and acting, Zach spends time at his home in the mountains of his native North Carolina, where he hopes to open a writer's retreat on a completely self-sustained farm.", new DateTime(1969, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zach Galifianakis", "https://dl.dropboxusercontent.com/scl/fi/hy4spbc488efk3zwq41iq/Zach-Galifianakis_ProfileImage.jpg?rlkey=ceg2c0nm6khhw6ydub5wuprjb" },
                    { 1019, "Joaquin Phoenix was born Joaquin Rafael Bottom in San Juan, Puerto Rico, to Arlyn (Dunetz) and John Bottom, and is the middle child in a brood of five. His parents, from the continental United States, were then serving as Children of God missionaries. His mother is from a Jewish family from New York, while his father, from California, is of mostly British Isles descent. As a youngster, Joaquin took his cues from older siblings River Phoenix and Rain Phoenix, changing his name to Leaf to match their earthier monikers. When the children were encouraged to develop their creative instincts, he followed their lead into acting. Younger sisters Liberty Phoenix and Summer Phoenix rounded out the talented troupe.\r\n\r\nThe family moved often, traveling through Central and South America (and adopting the surname \"Phoenix\" to celebrate their new beginnings) but, by the time Joaquin was age 6, they had more or less settled in the Los Angeles area. Arlyn found work as a secretary at NBC, and John turned his talents to landscaping. They eventually found an agent who was willing to represent all five children, and the younger generation dove into television work. Commercials for meat, milk, and junk food were off-limits (the kids were all raised as strict vegans), but they managed to find plenty of work pushing other products. Joaquin's first real acting gig was a guest appearance on River's sitcom, Seven Brides for Seven Brothers (1982).", new DateTime(1974, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Joaquin Phoenix", "https://dl.dropboxusercontent.com/scl/fi/9g635ce0x3qwotcyyip4i/Joaquin-Phoenix_ProfileImage.jpg?rlkey=y4ibnd111v87lgl799i5hmhtf" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CriticId", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("2532ddaa-63f0-4de8-71cb-08db8c333233"), 0, "dcbf6e12-64f6-47f1-86d3-c3e2da63520c", null, "user@user.bg", false, true, null, "USER@USER.BG", "USER182", "AQAAAAEAACcQAAAAEAsQ9sg0mW31vlM2DKquhykexBxdIKzD8YMSV5aAVT9ii4TazrF0Ep9t4AwwalAV8w==", null, false, "CPHRH5Z35EEOSFSFA6UQV2ZDTJGKCIKX", false, "user182" });

            migrationBuilder.InsertData(
                table: "Critics",
                columns: new[] { "Id", "Bio", "DateOfBirth", "Name", "ProfileImageUrl", "UserId" },
                values: new object[,]
                {
                    { new Guid("93372a0a-b9f4-4bc5-8f53-e8da0bce2bfe"), "Administrators do NOT need bio :)", new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Administrator", "https://dl.dropboxusercontent.com/scl/fi/jv513momlbuc69iycnqzx/Administrator_ProfileImage.jpg", new Guid("94756cdf-566e-4bf8-b03b-416a118ad53b") },
                    { new Guid("bf595423-7323-4e40-bd43-0f876c1beece"), "Critic Criticov is  an American film critic, film historian, journalist, essayist, screenwriter, and author. He was a film critic for the Chicago Sun-Times from 1967 until his death in 2013. In 1975, Ebert became the first film critic to win the Pulitzer Prize for Criticism. Neil Steinberg of the Chicago Sun-Times said Ebert \"was without question the nation's most prominent and influential film critic,\"[1] and Kenneth Turan of the Los Angeles Times called him \"the best-known film critic in America.\"[2]\r\n\r\nEbert was known for his intimate, Midwestern writing voice and critical views informed by values of populism and humanism.[3] Writing in a prose style intended to be entertaining and direct, he made sophisticated cinematic and analytical ideas more accessible to non-specialist audiences.[4] While a populist, Ebert frequently endorsed foreign and independent films he believed would be appreciated by mainstream viewers, which often resulted in such films receiving greater exposure.[5] Critic A. O. Scott wrote that Ebert's prose had a \"plain-spoken Midwestern clarity\" and a \"genial, conversational presence on the page...his criticism shows a nearly unequaled grasp of film history and technique, and formidable intellectual range, but he rarely seems to be showing off. He's just trying to tell you what he thinks, and to provoke some thought on your part about how movies work and what they can do\".[6]", new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Critic Criticov", "https://dl.dropboxusercontent.com/scl/fi/aolsn1cqh30yds8e6fj66/CriticTestov_ProfileImage.jpg", new Guid("15eb7825-840b-4528-71cc-08db8c333233") }
                });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "Bio", "DateOfBirth", "Name", "ProfileImageUrl" },
                values: new object[,]
                {
                    { 1, "Andy Muschietti was born on 26 August 1973 in Buenos Aires, Federal District, Argentina. He is a producer and director, known for Мама (2013), То (2017) and То: Част втора (2019).", new DateTime(1973, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Andy Muschietti", "https://dl.dropboxusercontent.com/s/xye9456bo6f9ld3/MV5BMTkwMDE0NTc0NF5B_profile_image._V1_.jpg" },
                    { 2, "James Mangold is an American film and television director, screenwriter and producer. Films he has directed include Луди години (1999), Да преминеш границата (2005), which he also co-wrote, the 2007 remake Ескорт до затвора (2007), Върколакът (2013), and Логан: Върколакът (2017).\r\n\r\nMangold also wrote and directed Копланд (1997), starring Sylvester Stallone, Robert De Niro, Harvey Keitel, and Ray Liotta.", new DateTime(1963, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "James Mangold", "https://dl.dropboxusercontent.com/s/491y3cpjoc4ulzm/MV5BNDI3MzgwMmYtY2JjYy00ZWQ2LTgzN_profile_image.jpg" },
                    { 1001, "Greta Gerwig is an American actress, playwright, screenwriter, and director. She has collaborated with Noah Baumbach on several films, including Greenberg (2010), Frances Ha (2012), for which she earned a Golden Globe nomination, and Mistress America (2015). Gerwig made her solo directorial debut with the critically acclaimed comedy-drama film Лейди Бърд (2017), which she also wrote, and has also had starring roles in the films Damsels in Distress (2011), Jackie (2016), and 20th Century Women (2016).\r\n\r\nGreta Celeste Gerwig was born in Sacramento, California, to Christine Gerwig (née Sauer), a nurse, and Gordon Gerwig, a financial consultant and computer programmer. She has German, Irish, and English ancestry. Gerwig was raised as a Unitarian Universalist, but also attended an all-girls Catholic school. She has described herself as \"an intense child\". With an early interest in dance, she intended to get a degree in musical theatre in New York. She graduated from Barnard College in NY, where she studied English and philosophy, instead. Originally intending to become a playwright, after meeting young film director Joe Swanberg, she became the star of a series of intellectual low budget movies made by first-time filmmakers, a trend dubbed \"mumblecore\".\r\n\r\nGerwig was cast in a minor role in Swanberg's LOL (2006) in 2006, while still studying at Barnard. She then appeared in many of Swanberg's films, and personally co-directed, co-wrote and co-produced one entitled Nights and Weekends (2008). She has worked with good quality directors such as Ti West (Къщата на дявола (2009)), Whit Stillman (Damsels in Distress (2011)), or Woody Allen (На Рим с любов (2012)) but success and (international) recognition did not come until Франсис Ха (2012), directed by Noah Baumbach, a film she also co-wrote. Both tall and immature, awkward and graceful, blundering and candid, annoying and engaging, Greta has won all hearts in the title role of Frances Ha(liday).", new DateTime(1983, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Greta Gerwig", "https://dl.dropboxusercontent.com/scl/fi/lj96i6jco3k5kquu482hr/Greta-Gerwig_ProfileImage.jpg?rlkey=ipkpl0wok9k8lczi3xb57mdhr" },
                    { 1004, "Anthony J. Russo is an American filmmaker and producer who works alongside his brother Joseph Russo. They have directed You, Me and Dupree, Cherry and the Marvel films Captain America: The Winter Soldier, Captain America: Civil War, Avengers: Infinity War and Avengers: Endgame. Endgame is one of the highest grossing films of all time.", new DateTime(1970, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anthony Russo", "https://dl.dropboxusercontent.com/scl/fi/5s3pw9nltwzxbn9qhtwxe/Anthony-Russo_ProfileImage.jpg?rlkey=vzsat7wh10st141k5a0v35adg" },
                    { 1005, "Ryan Kyle Coogler is an African-American filmmaker and producer who is from Oakland, California. He is known for directing the Black Panther film series, Creed, a Rocky spin-off and Fruitvale Station. He frequently casts Michael B. Jordan in his works. He produced the Creed sequels, Judas and the Black Messiah and Space Jam: A New Legacy. He is married to Zinzi since 2016.", new DateTime(1986, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ryan Coogler", "https://dl.dropboxusercontent.com/scl/fi/kcdzpmxuya99j91c5hqkr/Ryan-Coogler_ProfileImage.jpg?rlkey=2d1mnwewacvfvijm6xswzau6u" },
                    { 1006, "Todd Phillips is an American film director, producer, and screenwriter.\r\n\r\nGrowing up on Long Island, New York, Todd Phillips fell in love with feature film teen comedies made in the 1980s, and claims they were his biggest influence in becoming a filmmaker. While studying film at New York University, he made a documentary called Hated (1994), using his credit cards to finance the filmâEUR(TM)s $13,000 budget. About an excessive punk rocker, GG Allen, the student film won an award at the New Orleans Film Festival and went on to be released both theatrically and on DVD. Phillips' next project was a documentary called Frat House (1998), which followed the trials of young men trying to get accepted into a fraternity. The film won the Grand Jury Prize at the Sundance Film Festival, but soon became banned from public viewing when the young men involved objected, and lawyers for their families stepped in.\r\n\r\nWhile working on a commercial for Pepsi, Phillips met comedian Tom Green. He was writing the screenplay for his new film, Road Trip, and asked Green if he would be in it. Green agreed on the spot, and Phillips went on to make his first fictional movie, an homage to the types of films he grew up with. Road Trip was made on a budget of $15.6 million, and nearly made the money back in its opening weekend despite mixed reviews, most of which agreed it was in bad taste, with some finding that funny while others found it offensive.\r\n\r\nPhillips continued on in the same genre with Old School (2003), about three grown men who try to return to their frat boy days. Phillips says, \"Things go in cycles and right now people use the term gross out of comedy a lot and I find it very dismissive. I think it's very easy to be gross and very hard to be funny. The ones that work are actually very funny at their root. I, as a director, want to stick with comedies for a little while. It's the movies I grew up on and the stuff I like to see.\"\r\n\r\nPhillips' next project was action comedy Starsky & Hutch, based on the hit television series that ran from 1975 to 1979. The film, starring Owen Wilson and Ben Stiller, is also set in the '70s. He's hoping to turn another '70s TV show, The Six Million Dollar Man, into a feature film starring Jim Carrey, but in the meantime, filmed the comedy School for Scoundrels (2006), starring Jon Heder and Billy Bob Thornton. His next film, The Hangover 2009, was an enormous success, spawning a 2011 sequel that he also directed. In between those two movies he directed Robert Downey Jr. and Hangover star Zach Galifianakis in the comedy Due Date 2010.\r\n\r\nMore recent films include The Hangover Part II (2011), The Hangover Part III (2013), and War Dogs (2016).\r\n\r\nMove away from his favorite genre, he next took on the film Joker (2019), starring Joaquin Phoenix in the title role. The film debuted to much acclaim, and both Joaquin and Phillips received numerous award nominations, including Best Director nods for Phillips from the Academy Awards, Golden Globes and the BAFTAs.", new DateTime(1970, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Todd Phillips", "https://dl.dropboxusercontent.com/scl/fi/qfibl9810e48o2hlcwwaw/Todd-Phillips_ProfileImage.jpg?rlkey=0qh0gj0cprarvgfwttgsj3efb" },
                    { 1007, "Louis Leterrier is a French film director and producer. He notably directed the first two Transporter films, Unleashed, The Incredible Hulk, Clash of the Titans, Now You See Me, Tower of Strength and The Brothers Grimsby. He also directed episodes of The Dark Crystal: Age of Resistance on Netflix and three episodes of Lupin.", new DateTime(1973, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Louis Leterrier", "https://dl.dropboxusercontent.com/scl/fi/hlxsjn0l4psl1rqage5w2/Louis-Leterrier_ProfileImage.jpg?rlkey=c5sim0hwp1gvfrsabphaoaqzo" },
                    { 1008, "James Gunn was born and raised in St. Louis, Missouri, to Leota and James Francis Gunn. He is from a large Catholic family, with Irish and Czech ancestry. His father and his uncles were all lawyers. He has been writing and performing as long as he can remember. He began making 8mm films at the age of twelve. Many of these were comedic splatter films featuring his brothers being disemboweled by zombies. He attended Saint Louis University High (SLUH) college preparatory school but later dropped out of college to pursue a rock and roll career.\r\n\r\nHis band, \"the Icons\", released one album, \"Mom, We Like It Here on Earth\". He earned very little money doing this and so during this time, he also worked as an orderly in Tucson, Arizona, upon which many of the situations in his first novel, \"The Toy Collector\", are based. He wrote and drew comic strips for underground and college newspapers.\r\n\r\nGunn eventually returned to school and received his B.A. at Saint Louis University in his native St. Louis. He moved to New York where he received an MFA in creative writing from Columbia University, which he today thinks may have been a wonderfully expensive waste of time. While finishing his MFA, he started writing \"The Toy Collector\" and began working for \"Troma Studios\", America's leading B-Movie production company. While there he wrote and produced the cult classic Tromeo and Juliet (1996) and, with Lloyd Kaufman, he wrote \"All I Need to Know about Filmmaking I Learned from the Toxic Avenger\".", new DateTime(1966, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "James Gunn", "https://dl.dropboxusercontent.com/scl/fi/5cigibl1esxhp9lvvou20/James-Gunn_ProfileImage.jpg?rlkey=aqd7cazq1wtjs9m9j8u8zpaga" },
                    { 1009, "Best known for his cerebral, often nonlinear, storytelling, acclaimed writer-director Christopher Nolan was born on July 30, 1970, in London, England. Over the course of 15 years of filmmaking, Nolan has gone from low-budget independent films to working on some of the biggest blockbusters ever made.\r\n\r\nAt 7 years old, Nolan began making short movies with his father's Super-8 camera. While studying English Literature at University College London, he shot 16-millimeter films at U.C.L.'s film society, where he learned the guerrilla techniques he would later use to make his first feature, on a budget of around $6,000. The noir thriller was recognized at a number of international film festivals prior to its theatrical release and gained Nolan enough credibility that he was able to gather substantial financing for his next film.\r\n\r\nNolan's second film was which he directed from his own screenplay based on a short story by his brother Jonathan. Starring Guy Pearce, the film brought Nolan numerous honors, including Academy Award and Golden Globe Award nominations for Best Original Screenplay. Nolan went on to direct the critically acclaimed psychological thriller, starring Al Pacino, Robin Williams and Hilary Swank.", new DateTime(1970, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Christopher Nolan", "https://dl.dropboxusercontent.com/scl/fi/yaxuuujaf8hlk3w1xsb30/Christopher-Nolan_ProfileImage.jpg?rlkey=sx7r3sg05xe5r9f5htvw7p0xt" },
                    { 1010, "Martin Charles Scorsese was born on November 17, 1942 in Queens, New York City, to Catherine Scorsese (née Cappa) and Charles Scorsese, who both worked in Manhattan's garment district, and whose families both came from Palermo, Sicily. He was raised in the neighborhood of Little Italy, which later provided the inspiration for several of his films. Scorsese earned a B.S. degree in film communications in 1964, followed by an M.A. in the same field in 1966 at New York University's School of Film. During this time, he made numerous prize-winning short films including The Big Shave (1967), and directed his first feature film, I Call First (1967).\r\n\r\nHe served as assistant director and an editor of the documentary Woodstock (1970) and won critical and popular acclaim for Жестоки улици (1973), which first paired him with actor and frequent collaborator Robert De Niro. In 1976, Scorsese's Шофьор на такси (1976), also starring De Niro, was awarded the Palme d'Or at the Cannes Film Festival, and he followed that film with Ню Йорк, Ню Йорк (1977) and Последният валс (1978). Scorsese directed De Niro to an Oscar-winning performance as boxer Jake LaMotta in Разяреният бик (1980), which received eight Academy Award nominations, including Best Picture and Best Director, and is hailed as one of the masterpieces of modern cinema. Scorsese went on to direct Цветът на парите (1986), Последното изкушение на Христос (1988), Добри момчета (1990), Нос Страх (1991), Невинни години (1993), Казино (1995) and Кундун (1997), among other films. Commissioned by the British Film Institute to celebrate the 100th anniversary of the birth of cinema, Scorsese completed the four-hour documentary, A Personal Journey with Martin Scorsese Through American Movies (1995), co-directed by Michael Henry Wilson.\r\n\r\nHis long-cherished project, Бандите на Ню Йорк (2002), earned numerous critical honors, including a Golden Globe Award for Best Director; the Howard Hughes biopic Авиаторът (2004) won five Academy Awards, in addition to the Golden Globe and BAFTA awards for Best Picture. Scorsese won his first Academy Award for Best Director for От другата страна (2006), which was also honored with the Director's Guild of America, Golden Globe, New York Film Critics, National Board of Review and Critic's Choice awards for Best Director, in addition to four Academy Awards, including Best Picture. Scorsese's documentary of the Rolling Stones in concert, Shine a Light (2008), followed, with the successful thriller Злокобен остров (2010) two years later. Scorsese received his seventh Academy Award nomination for Best Director, as well as a Golden Globe Award, for Изобретението на Хюго (2011), which went on to win five Academy Awards.", new DateTime(1942, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Martin Scorsese", "https://dl.dropboxusercontent.com/scl/fi/qfvqldcfp61rp8chtmh05/Martin-Scorsese_ProfileImage.jpg?rlkey=dgfetl4p1xiolsnvsb0gri2us" },
                    { 1011, "David Ayer is an American film director, producer, and screenwriter. David Ayer was born in Champaign, Illinois and grew up in Bloomington, Minnesota, and Bethesda, Maryland, where he was kicked out of his house by his parents as a teenager. Ayer then lived with his cousin in Los Angeles, California, where his experiences in South Central Los Angeles became the inspiration for many of his films. Ayer then enlisted in the United States Navy as a submariner.", new DateTime(1968, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "David Ayer", "https://dl.dropboxusercontent.com/scl/fi/u7yvbii32fzutbfmgv3hn/David-Ayer_ProfileImage.jpg?rlkey=lnzur78y9klourmam6iwl366z" },
                    { 1012, "Ruben Fleischer was born on October 31, 1974 in Washington, District of Columbia, USA. He is a producer and director, known for Zombie Land (2009),and Venum (2018). He has been married to Holly Shakoor Fleischer since November 10, 2012.", new DateTime(1974, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ruben Fleischer", "https://dl.dropboxusercontent.com/scl/fi/xqrb3ujq03q238llhie8u/Ruben-Fleischer_ProfileImage.jpg?rlkey=zxqfu8aptj5io0m9ohhz2ezdn" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Adventure" },
                    { 3, "Animation" },
                    { 4, "Biographical" },
                    { 5, "Comedy" },
                    { 6, "Action" },
                    { 7, "Crime" },
                    { 8, "Documentary" },
                    { 9, "Drama" },
                    { 10, "Family" },
                    { 11, "Fantasy" },
                    { 12, "History" },
                    { 13, "Horror" },
                    { 14, "Musical" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 15, "Mystery" },
                    { 16, "Romance" },
                    { 17, "Sci-Fi" },
                    { 18, "Sport" },
                    { 19, "Superhero" },
                    { 20, "Short" },
                    { 21, "Thriller" },
                    { 22, "War" },
                    { 23, "Western" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CriticId", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("15eb7825-840b-4528-71cc-08db8c333233"), 0, "a8c8e65b-4a5d-4ec8-a4fe-1054b3e543d0", new Guid("bf595423-7323-4e40-bd43-0f876c1beece"), "critic@critic.bg", false, true, null, "CRITIC@CRITIC.BG", "CRITICCRITICOV", "AQAAAAEAACcQAAAAED8AiAX7LsAuELS3lYisOFNJVOGpUD+yNBUxLCVvKeE0nRH/nHDdWjTTbUk9Pt6efA==", null, false, "RCRRYTFZIKPBOLEU7PJ4QZDD7PL3EJTX", false, "CriticCriticov" },
                    { new Guid("94756cdf-566e-4bf8-b03b-416a118ad53b"), 0, "57bc2c05-86fa-4586-8a84-fd5019e8b4e6", new Guid("93372a0a-b9f4-4bc5-8f53-e8da0bce2bfe"), "admin@admin.com", false, true, null, "ADMIN@ADMIN.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAEIDAX0WNuYyTTwkcp9gbVk146rrp9KcqXGumzJ7/bR33e1FBPSMU6035VP9GrLkwPA==", null, false, "NJN3O3VL2VQRL2ESBOFVB5JLFCZCJS2A", false, "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "CoverImageUrl", "Description", "DirectorId", "Duration", "ReleaseDate", "Title", "TrailerUrl" },
                values: new object[,]
                {
                    { 1, "https://dl.dropboxusercontent.com/scl/fi/lgvgll5jt71j0ad3202d4/The_Flash_cover_image.jpg?rlkey=ya84xsnh8dioy08iw497uyjdf", "Barry Allen uses his super speed to change the past, but his attempt to save his family creates a world without super heroes, forcing him to race for his life in order to save the future.", 1, 144, new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Flash", "https://dl.dropboxusercontent.com/scl/fi/t44ljvld1q9ybw8oeqe8z/2023-_trailer.mp4?rlkey=wnapnzwvtfhk1x5nlwr7oa88x" },
                    { 2, "https://dl.dropboxusercontent.com/s/kban35sz6tw0d0x/Indiana_Jones_and_the_Dial_of_Destiny_cover_image.jpg", "Archaeologist Indiana Jones races against time to retrieve a legendary artifact that can change the course of history.", 2, 154, new DateTime(2023, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Indiana Jones and the Dial of Destiny", "https://dl.dropboxusercontent.com/s/evfvsgzlf9f4vok/1434659607842-pgv4ql-1680875129724_trailer.mp4" },
                    { 1003, "https://dl.dropboxusercontent.com/scl/fi/azbfvlt972dy4t5sygzii/Barbie_CoverImage.jpg?rlkey=vmf7122bseqsxeif5vigbyxb4", "Barbie suffers a crisis that leads her to question her world and her existence", 1001, 114, new DateTime(2023, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Barbie", "https://dl.dropboxusercontent.com/scl/fi/c4uuqbg38lipejpdkj0s3/Barbie_Trailer.mp4?rlkey=hexi9dm3p97ujwglgkw1cn3ju" },
                    { 1006, "https://dl.dropboxusercontent.com/scl/fi/ksdursap46bdhr8ccfkm1/Jurassic-World-Fallen-Kingdom_CoverImage.jpg?rlkey=zpdumvgy9jbc25ppxvk7gtj6r", "When the island's dormant volcano begins roaring to life, Owen and Claire mount a campaign to rescue the remaining dinosaurs from this extinction-level event.", 1005, 128, new DateTime(2018, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jurassic World: Fallen Kingdom", "https://dl.dropboxusercontent.com/scl/fi/har6gphbx8whg0onnm969/Jurassic-World-Fallen-Kingdom_Trailer.mp4?rlkey=7wzfd7c5cu1l90cqdij7cm4jv" },
                    { 1007, "https://dl.dropboxusercontent.com/scl/fi/4e8kznbyb0r1ly2xadp2i/Avengers-Endgame_CoverImage.jpg?rlkey=unhpzjogw5izyod2xde36z0p9", "After the devastating events of Advengers:  War(2018), the universe is in ruins. With the help of remaining allies, the Avengers assemble once more in order to reverse Thanos' actions and restore balance to the universe.", 1004, 181, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avengers: Endgame", "https://dl.dropboxusercontent.com/scl/fi/dbnf05zefybz86yeuk4fl/Avengers-Endgame_Trailer.mp4?rlkey=mxmy8lwsqnxb3u5hv2oti0ebk" },
                    { 1008, "https://dl.dropboxusercontent.com/scl/fi/qhgpdaqm91cf4urqmrrez/Black-Panther_CoverImage.jpg?rlkey=ch2mmht2bgxd388gt3ppq8f5t", "T'Challa, heir to the hidden but advanced kingdom of Wakanda, must step forward to lead his people into a new future and must confront a challenger from his country's past.", 1005, 134, new DateTime(2018, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Black Panther", "https://dl.dropboxusercontent.com/scl/fi/eo9zd6no7w6cptcbvb8sj/Black-Panther_Trailer.mp4?rlkey=4z29a87hxg3taxccso90jml6e" },
                    { 1009, "https://dl.dropboxusercontent.com/scl/fi/7bgm1wt5sxnk0wtzc3y2r/The-Hangover-II_CoverImage.jpg?rlkey=e80yfei74yh1qzyr9j9lxogfz", "Three buddies wake up from a bachelor party in Las Vegas, with no memory of the previous night and the bachelor missing. They make their way around the city in order to find their friend before his wedding.", 1006, 142, new DateTime(2007, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Hangover II", "https://dl.dropboxusercontent.com/scl/fi/aboafm63fb89qirpszl44/The-Hangover-II_Trailer.mp4?rlkey=l23lend702b87v0ekri7bpnaz" },
                    { 1010, "https://dl.dropboxusercontent.com/scl/fi/zeh9q21hihz4h7x3jaokz/Fast-X_CoverImage.jpg?rlkey=ujgvvdcq8bng4q08ap8u73x1c", "Dom Toretto and his family are targeted by the vengeful son of drug kingpin Hernan Reyes.", 1007, 141, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fast X", "https://dl.dropboxusercontent.com/scl/fi/sl4s78qzqz108qrnlvw06/Fast-X_Trailer.mp4?rlkey=1dhdqwjlwt0bkwnfwb3qki3km" },
                    { 1011, "https://dl.dropboxusercontent.com/scl/fi/wa981exkyfch3lek0mj09/Guardians-of-the-Galaxy-Vol.-3_CoverImage.jpg?rlkey=bthwfsde1atsepmy2w4hfvaj5", "Still reeling from the loss of Gamora, Peter Quill rallies his team to defend the universe and one of their own - a mission that could mean the end of the Guardians if not successful.", 1008, 150, new DateTime(2020, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guardians of the Galaxy Vol. 3", "https://dl.dropboxusercontent.com/scl/fi/6sg5jze46rsgm48zn6nn0/Guardians-of-the-Galaxy-Vol.-3_Trailer.mp4?rlkey=sf6jg0l9ba3ttgfmsqces26qi" },
                    { 1012, "https://dl.dropboxusercontent.com/scl/fi/9osghrllfz8kuw2rx2qey/Joker_CoverImage.jpg?rlkey=rx89asfu5pjuzkofbg9u0xfpn", "The rise of Arthur Fleck, from aspiring stand-up comedian and pariah to Gotham's clown prince and leader of the revolution.", 1006, 122, new DateTime(2019, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Joker", "https://dl.dropboxusercontent.com/scl/fi/g8af3e82qafqlqeqybobe/Joker_Trailer.mp4?rlkey=u3tjhbgizp5q9oyr09eumuk18" },
                    { 1013, "https://dl.dropboxusercontent.com/scl/fi/u1ew0gbr96hpkm828hpgu/The-Wolf-of-Wall-Street_CoverImage.jpg?rlkey=wzfv4mkzhtmrb2cz9eug8aeb6", "Based on the true story of Jordan Belfort, from his rise to a wealthy stock-broker living the high life to his fall involving crime, corruption and the federal government.", 1010, 187, new DateTime(2013, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Wolf of Wall Street", "https://dl.dropboxusercontent.com/scl/fi/r38t5xjw1q8j17sv14xtb/The-Wolf-of-Wall-Street_Trailer.mp4?rlkey=o2mybmi1hcc838t2nvqd22qsz" },
                    { 1014, "https://dl.dropboxusercontent.com/scl/fi/tqyba8w6ct5ge9us089yx/Oppenheimer_CoverImage.jpg?rlkey=qe18p1jvgb5u7gi0mlmaw35ao", "The story of American scientist, J. Robert Oppenheimer, and his role in the development of the atomic bomb.", 1009, 173, new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Oppenheimer", "https://dl.dropboxusercontent.com/scl/fi/a39704pdrbf20sc9ryrqg/Oppenheimer_Trailer.mp4?rlkey=gkxm0sfjp7ly5sa93q8r79j07" },
                    { 1015, "https://dl.dropboxusercontent.com/scl/fi/6vj6m2b7htake0h44x42r/Suicide-Squad_CoverImage.jpg?rlkey=y68dt274dwvhp17q6bg50i174", "A secret government agency recruits some of the most dangerous incarcerated super-villains to form a defensive task force. Their first mission: save the world from the apocalypse.", 1011, 123, new DateTime(2016, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Suicide Squad", "https://dl.dropboxusercontent.com/scl/fi/wh0hv2p44rycvda4htshb/Suicide-Squad_Trailer.mp4?rlkey=65fr160zxdpgor5sloubtl1t0" },
                    { 1016, "https://dl.dropboxusercontent.com/scl/fi/2lgy3krvjc8jov1m4tpdw/Avengers-Infinity-War_CoverImage.jpg?rlkey=dglmwdk3be9xjgm0inu984br7", "The Avengers and their allies must be willing to sacrifice all in an attempt to defeat the powerful Thanos before his blitz of devastation and ruin puts an end to the universe.", 1004, 149, new DateTime(2018, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avengers: Infinity War", "https://dl.dropboxusercontent.com/scl/fi/sxo3vldqpnjpupl54e05s/Avengers-Infinity-War_Trailer.mp4?rlkey=2gqw1ajtfm4n0hz9t4faekdde" },
                    { 1017, "https://dl.dropboxusercontent.com/scl/fi/r31pv26br6jxlgh4phivi/Uncharted_CoverImage.jpg?rlkey=cizigwaqlxg7ypnnji64k1rmi", "Street-smart Nathan Drake is recruited by seasoned treasure hunter Victor \"Sully\" Sullivan to recover a fortune amassed by Ferdinand Magellan, and lost 500 years ago by the House of Moncada.", 1012, 116, new DateTime(2022, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uncharted", "https://dl.dropboxusercontent.com/scl/fi/vn6tjhwdjt63txuzuzc31/Uncharted_Trailer.mp4?rlkey=m14qo6nvhqgirj8r14j76j45z" },
                    { 1020, "https://dl.dropboxusercontent.com/scl/fi/vral4iavrbc0lk0x36vel/Blade-Runner-2049_CoverImage.jpg?rlkey=kgnpjc3nelyba4rgoyml400zy", "Young Blade Runner K's discovery of a long-buried secret leads him to track down former Blade Runner Rick Deckard, who's been missing for thirty years.", 1012, 146, new DateTime(2017, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Blade Runner 2049", "https://dl.dropboxusercontent.com/scl/fi/sn346bvsm61xc0swlvb1x/Blade-Runner-2049_Trailer.mp4?rlkey=gyxp5gzest3b56peqk3tars7j" }
                });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[,]
                {
                    { new Guid("68d65f56-b265-4445-9407-8d87e3ef14f2"), 2, null, null, "https://dl.dropboxusercontent.com/s/oxqc24aw3xb9n9z/MV5BMTY4Mjg0NjIxOV5BMl5Ban_profile_image.jpg" },
                    { new Guid("ccda3d1b-f7db-41f8-9c42-6e92b11ce957"), null, 2, null, "https://dl.dropboxusercontent.com/s/491y3cpjoc4ulzm/MV5BNDI3MzgwMmYtY2JjYy00ZWQ2LTgzN_profile_image.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[,]
                {
                    { new Guid("55842a6a-53e3-4899-a535-55d9044eea19"), 2, null, null, "https://dl.dropboxusercontent.com/s/l6151myomc65wqc/1434659607842-pgv4ql-1687556762812.mp4" },
                    { new Guid("fc56776c-9109-4bf8-9cf3-24c1caf7cfe5"), null, 2, null, "https://dl.dropboxusercontent.com/s/joz6wy1q4o55jz4/1434659607842-pgv4ql-1687560633004.mp4" }
                });

            migrationBuilder.InsertData(
                table: "MoviesActors",
                columns: new[] { "ActorId", "MovieId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 1009, 1003 },
                    { 1012, 1003 },
                    { 1014, 1006 },
                    { 1001, 1007 },
                    { 1007, 1007 },
                    { 1015, 1008 },
                    { 1018, 1009 },
                    { 1016, 1010 },
                    { 1014, 1011 },
                    { 1019, 1012 },
                    { 1009, 1013 },
                    { 1013, 1013 },
                    { 1007, 1014 },
                    { 1017, 1014 },
                    { 1009, 1015 },
                    { 1001, 1016 },
                    { 1007, 1016 },
                    { 1001, 1017 },
                    { 2, 1020 },
                    { 1012, 1020 }
                });

            migrationBuilder.InsertData(
                table: "MoviesGenres",
                columns: new[] { "GenreId", "MovieId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 5, 1003 },
                    { 14, 1003 },
                    { 16, 1003 },
                    { 1, 1006 },
                    { 11, 1006 },
                    { 17, 1006 },
                    { 1, 1007 },
                    { 2, 1007 },
                    { 11, 1007 },
                    { 19, 1007 },
                    { 11, 1008 },
                    { 19, 1008 },
                    { 5, 1009 },
                    { 1, 1010 },
                    { 10, 1010 },
                    { 11, 1010 },
                    { 15, 1011 }
                });

            migrationBuilder.InsertData(
                table: "MoviesGenres",
                columns: new[] { "GenreId", "MovieId" },
                values: new object[,]
                {
                    { 17, 1011 },
                    { 19, 1011 },
                    { 4, 1012 },
                    { 7, 1012 },
                    { 11, 1012 },
                    { 4, 1013 },
                    { 5, 1013 },
                    { 7, 1013 },
                    { 1, 1014 },
                    { 4, 1014 },
                    { 15, 1014 },
                    { 5, 1015 },
                    { 11, 1015 },
                    { 19, 1015 },
                    { 11, 1016 },
                    { 19, 1016 },
                    { 2, 1017 },
                    { 10, 1017 },
                    { 11, 1017 },
                    { 11, 1020 }
                });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[] { new Guid("a1ec0f48-25a2-495a-a8be-1ecf98590e2f"), null, null, 2, "https://dl.dropboxusercontent.com/s/kban35sz6tw0d0x/Indiana_Jones_and_the_Dial_of_Destiny_cover_image.jpg" });

            migrationBuilder.InsertData(
                table: "Videos",
                columns: new[] { "Id", "ActorId", "DirectorId", "MovieId", "Url" },
                values: new object[] { new Guid("d10e42ca-1961-4ffc-8b5d-fb1e662a26c1"), null, null, 2, "https://dl.dropboxusercontent.com/s/evfvsgzlf9f4vok/1434659607842-pgv4ql-1680875129724_trailer.mp4" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CriticId",
                table: "AspNetUsers",
                column: "CriticId",
                unique: true,
                filter: "[CriticId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_GroupId",
                table: "Chats",
                column: "GroupId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ReviewId",
                table: "Comments",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_WriterId",
                table: "Comments",
                column: "WriterId");

            migrationBuilder.CreateIndex(
                name: "IX_Dislikes_ReviewId",
                table: "Dislikes",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Dislikes_UserId",
                table: "Dislikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsUsers_GroupId",
                table: "GroupsUsers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_ReviewId",
                table: "Likes",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_DirectorId",
                table: "Movies",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesActors_ActorId",
                table: "MoviesActors",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_MoviesGenres_GenreId",
                table: "MoviesGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_PassedMovies_MovieId",
                table: "PassedMovies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ActorId",
                table: "Photos",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_DirectorId",
                table: "Photos",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_MovieId",
                table: "Photos",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CriticId",
                table: "Reviews",
                column: "CriticId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieId",
                table: "Reviews",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRatings_MovieId",
                table: "UserRatings",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRatings_UserId",
                table: "UserRatings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersMovies_MovieId",
                table: "UsersMovies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ActorId",
                table: "Videos",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_DirectorId",
                table: "Videos",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_MovieId",
                table: "Videos",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Dislikes");

            migrationBuilder.DropTable(
                name: "GroupsUsers");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "MoviesActors");

            migrationBuilder.DropTable(
                name: "MoviesGenres");

            migrationBuilder.DropTable(
                name: "PassedMovies");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "UserRatings");

            migrationBuilder.DropTable(
                name: "UsersMovies");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Critics");

            migrationBuilder.DropTable(
                name: "Directors");
        }
    }
}

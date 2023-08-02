namespace FilmIdea.Data;

using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Join_Tables;

public class FilmIdeaDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public FilmIdeaDbContext(DbContextOptions<FilmIdeaDbContext> options)
        : base(options)
    {
    }


    // Entity Models
    public DbSet<Actor> Actors { get; set; } = null!;

    public DbSet<Chat> Chats { get; set; } = null!;

    public DbSet<Comment> Comments { get; set; } = null!;

    public DbSet<Critic> Critics { get; set; } = null!;

    public DbSet<Director> Directors { get; set; } = null!;

    public DbSet<Genre> Genres { get; set; } = null!;

    public DbSet<Group> Groups { get; set; } = null!;

    public DbSet<Message> Messages { get; set; } = null!;

    public DbSet<Movie> Movies { get; set; } = null!;

    public DbSet<Review> Reviews { get; set; } = null!;

    public DbSet<UserRating> UserRatings { get; set; } = null!;

    public DbSet<Photo> Photos { get; set; } = null!;

    public DbSet<Video> Videos { get; set; } = null!;

    public DbSet<Like> Likes { get; set; } = null!;

    public DbSet<Dislike> Dislikes { get; set; } = null!;


    // Join Tables

    public DbSet<GroupUser> GroupsUsers { get; set; } = null!;

    public DbSet<MovieActor> MoviesActors { get; set; } = null!;

    public DbSet<MovieGenre> MoviesGenres { get; set; } = null!;

    public DbSet<UserMovie> UsersMovies { get; set; } = null!;

    public DbSet<PassedMovie> PassedMovies { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var configurationAssembly = Assembly.GetAssembly(typeof(FilmIdeaDbContext)) ??
                                    Assembly.GetExecutingAssembly();

        builder.ApplyConfigurationsFromAssembly(configurationAssembly);

        base.OnModelCreating(builder);
    }
}
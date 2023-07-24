namespace FilmIdea.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Models;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Movie> builder)
    {
        builder.HasMany(m => m.Ratings)
            .WithOne(r => r.Movie)
            .HasForeignKey(r => r.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m=>m.Reviews)
            .WithOne(r => r.Movie)
            .HasForeignKey(r => r.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m=>m.Actors)
            .WithOne(ma => ma.Movie)
            .HasForeignKey(ma => ma.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m=>m.Genres)
            .WithOne(mg => mg.Movie)
            .HasForeignKey(mg => mg.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.GroupsWatchlists)
            .WithOne(gw => gw.Movie)
            .HasForeignKey(gw => gw.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.UsersWatchlists)
            .WithOne(um => um.Movie)
            .HasForeignKey(um => um.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.Photos)
            .WithOne(p => p.Movie)
            .HasForeignKey(p => p.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.Videos)
            .WithOne(v => v.Movie)
            .HasForeignKey(p => p.MovieId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

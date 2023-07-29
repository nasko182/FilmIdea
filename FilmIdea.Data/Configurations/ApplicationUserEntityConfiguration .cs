namespace FilmIdea.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;
using Seed;


public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    private readonly ApplicationUserSeeder _seeder;

    public ApplicationUserEntityConfiguration()
    {
        this._seeder = new ApplicationUserSeeder();
    }
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasMany(u => u.Ratings)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Comments)
            .WithOne(c => c.Writer)
            .HasForeignKey(c => c.WriterId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Groups)
            .WithOne(g => g.User)
            .HasForeignKey(g => g.UserId)
                        .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Watchlist)
            .WithOne(um => um.User)
            .HasForeignKey(um => um.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.PassedMovies)
            .WithOne(um => um.User)
            .HasForeignKey(um => um.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u=>u.Messages)
            .WithOne(m=>m.Sender)
            .HasForeignKey(m=>m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u=>u.Critic)
            .WithOne(c=>c.User)
            .HasForeignKey<ApplicationUser>(u=>u.CriticId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(this._seeder.GenerateUsers());
    }
}
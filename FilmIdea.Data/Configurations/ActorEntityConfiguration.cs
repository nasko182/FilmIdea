namespace FilmIdea.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;
using Seed;

public class ActorEntityConfiguration : IEntityTypeConfiguration<Actor>
{
    private readonly ActorSeeder _seeder;

    public ActorEntityConfiguration()
    {
        this._seeder = new ActorSeeder();
    }
    public void Configure(EntityTypeBuilder<Actor> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasMany(a => a.Movies)
            .WithOne(ma => ma.Actor)
            .HasForeignKey(ma => ma.ActorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a => a.Photos)
            .WithOne(p => p.Actor)
            .HasForeignKey(p => p.ActorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(a=>a.Videos)
            .WithOne(v=>v.Actor)
            .HasForeignKey(p => p.ActorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(this._seeder.GenerateActors());

    }
}
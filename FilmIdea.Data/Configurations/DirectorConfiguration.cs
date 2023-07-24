namespace FilmIdea.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Models;
using Seed;

public class DirectorConfiguration : IEntityTypeConfiguration<Director>
{
    private readonly DirectorSeeder _seeder;

    public DirectorConfiguration()
    {
        this._seeder = new DirectorSeeder();
    }
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Director> builder)
    {
        builder.HasKey(d=> d.Id);

        builder.HasMany(d => d.Movies)
            .WithOne(m => m.Director)
            .HasForeignKey(m => m.DirectorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(d => d.Photos)
            .WithOne(p => p.Director)
            .HasForeignKey(p => p.DirectorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(d => d.Videos)
            .WithOne(v => v.Director)
            .HasForeignKey(p => p.DirectorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(this._seeder.GenerateDirectors());
    }
}

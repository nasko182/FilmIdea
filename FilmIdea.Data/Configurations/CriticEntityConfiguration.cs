namespace FilmIdea.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Seed;
using Models;

public class CriticEntityConfiguration : IEntityTypeConfiguration<Critic>
{
    private readonly CriticSeeder _seeder;

    public CriticEntityConfiguration()
    {
        this._seeder = new CriticSeeder();
    }
    public void Configure(EntityTypeBuilder<Critic> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasMany(c => c.Reviews)
            .WithOne(r => r.Critic)
            .HasForeignKey(r => r.CriticId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(this._seeder.GenerateCritics());
    }
}
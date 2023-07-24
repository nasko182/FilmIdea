namespace FilmIdea.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;
using Seed;

public class GenreEntityConfiguration : IEntityTypeConfiguration<Genre>
    {
        private readonly GenreSeeder _genreSeeder;

        public GenreEntityConfiguration()
        {
            this._genreSeeder = new GenreSeeder();
        }

        public void Configure(EntityTypeBuilder<Genre> builder)
        {
    builder.HasKey(g => g.Id);

            builder.HasMany(g => g.Movies)
                .WithOne(mg => mg.Genre)
                .HasForeignKey(mg => mg.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(this._genreSeeder.GenerateGenres());
    }
    }


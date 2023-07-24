namespace FilmIdea.Data.Configurations;

using Microsoft.EntityFrameworkCore;

using Models.Join_Tables;
using Seed;

public class MovieGenreEntityConfiguration : IEntityTypeConfiguration<MovieGenre>
{
    private readonly MovieGenreSeeder _seeder;

    public MovieGenreEntityConfiguration()
    {
        this._seeder = new MovieGenreSeeder();
    }
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MovieGenre> builder)
    {
        builder.HasKey(mg => new { mg.MovieId, mg.GenreId });

        builder.HasData(this._seeder.GenerateMovieGenres());
    }
}

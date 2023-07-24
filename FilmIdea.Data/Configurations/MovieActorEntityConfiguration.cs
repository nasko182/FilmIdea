namespace FilmIdea.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Models.Join_Tables;
using Seed;

public class MovieActorEntityConfiguration : IEntityTypeConfiguration<MovieActor>
{
    private readonly MovieActorSeeder _seeder;

    public MovieActorEntityConfiguration()
    {
        this._seeder = new MovieActorSeeder();
    }
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<MovieActor> builder)
    {
        builder.HasKey(ma => new { ma.MovieId, ma.ActorId });

        builder.HasData(this._seeder.GenerateMovieActors());
    }
}

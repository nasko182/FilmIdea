namespace FilmIdea.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Models.Join_Tables;

public class PassedMovieEntityConfiguration : IEntityTypeConfiguration<PassedMovie>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PassedMovie> builder)
    {
        builder.HasKey(um => new { um.UserId, um.MovieId });
    }
}

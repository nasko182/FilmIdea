namespace FilmIdea.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Models.Join_Tables;

public class UserMovieEntityConfiguration : IEntityTypeConfiguration<UserMovie>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserMovie> builder)
    {
        builder.HasKey(um => new { um.UserId, um.MovieId });
    }
}

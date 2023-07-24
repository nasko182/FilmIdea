namespace FilmIdea.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Models.Join_Tables;

public class GroupMovieConfiguration : IEntityTypeConfiguration<GroupMovie>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<GroupMovie> builder)
    {
        builder.HasKey(gm => new { gm.GroupId, gm.MovieId });
    }
}

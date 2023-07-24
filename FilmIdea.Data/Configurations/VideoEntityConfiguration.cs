namespace FilmIdea.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using Seed;

public class VideoEntityConfiguration : IEntityTypeConfiguration<Video>
{
    private readonly VideoSeeder _seeder;

    public VideoEntityConfiguration()
    {
        this._seeder = new VideoSeeder();
    }
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasData(this._seeder.GenerateVideos());
    }
}

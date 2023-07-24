namespace FilmIdea.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using Seed;

public class PhotoEntityConfiguration : IEntityTypeConfiguration<Photo>
{
    private readonly PhotoSeeder _seeder;

    public PhotoEntityConfiguration()
    {
        this._seeder = new PhotoSeeder();
    }
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasData(this._seeder.GeneratePhotos());
    }
}

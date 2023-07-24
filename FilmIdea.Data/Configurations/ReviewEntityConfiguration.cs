namespace FilmIdea.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;
using Seed;

public class ReviewEntityConfiguration : IEntityTypeConfiguration<Review>
{
    private readonly ReviewSeeder _seeder;

    public ReviewEntityConfiguration()
    {
        this._seeder= new ReviewSeeder();
    }
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasMany(r => r.Comments)
            .WithOne(c => c.Review)
            .HasForeignKey(c => c.ReviewId)
            .OnDelete(DeleteBehavior.Restrict);

        //builder.HasData(this._seeder.GenerateReviews());
    }
}
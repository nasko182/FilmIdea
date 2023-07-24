namespace FilmIdea.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;
using Seed;

public class CommentEntityConfiguration : IEntityTypeConfiguration<Comment>
{
    private readonly CommentSeeder _seeder;

    public CommentEntityConfiguration()
    {
        this._seeder = new CommentSeeder();
    }
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasData(this._seeder.GenerateComments());
    }
}
    namespace FilmIdea.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models;

public class CriticEntityConfiguration : IEntityTypeConfiguration<Critic>
{
    public void Configure(EntityTypeBuilder<Critic> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasMany(c => c.Reviews)
            .WithOne(r => r.Critic)
            .HasForeignKey(r => r.CriticId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
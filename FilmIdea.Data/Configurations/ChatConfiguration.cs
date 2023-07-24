namespace FilmIdea.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Models;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Chat> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasMany(c => c.Messages)
            .WithOne(m => m.Chat)
            .HasForeignKey(m => m.ChatId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c=>c.Group)
            .WithOne(g=>g.Chat)
            .HasForeignKey<Chat>(c=>c.GroupId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

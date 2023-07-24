namespace FilmIdea.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Models.Join_Tables;

public class GroupUserConfiguration : IEntityTypeConfiguration<GroupUser>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<GroupUser> builder)
    {
        builder.HasKey(gu => new { gu.UserId, gu.GroupId });
    }
}
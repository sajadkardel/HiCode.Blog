using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HC.Shared.Extensions;

namespace HC.Data.Entities.Blog;

public class Tag : BaseEntity
{
    public string Name { get; set; } = default!;

    // Relations
    public ICollection<PostTag> PostTags { get; set; }
}

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable(nameof(Tag), typeof(Tag).GetParentFolderName());

        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
    }
}

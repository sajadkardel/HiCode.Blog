using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HC.Shared.Extensions;

namespace HC.Data.Entities.Blog;

public class Category : BaseEntity
{
    public string Name { get; set; } = default!;

    // Relations
    public ICollection<Post>? Posts { get; set; }
}

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(nameof(Category), typeof(Category).GetParentFolderName());

        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
    }
}
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HC.Shared.Extensions;

namespace HC.Data.Entities.Blog;

public class Category : BaseEntity
{
    public string Name { get; set; } = default!;
    public string? IconName { get; set; }

    public int? ParentCategoryId { get; set; }

    // Relations
    public ICollection<Post>? Posts { get; set; }
    public Category? ParentCategory { get; set; }
    public ICollection<Category>? ChildCategories { get; set; }
}

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(nameof(Category), typeof(Category).GetParentFolderName());

        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);

        builder.HasOne(p => p.ParentCategory).WithMany(p => p.ChildCategories);
    }
}
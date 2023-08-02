using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HC.Shared.Extensions;

namespace HC.Data.Entities.Blog;

public class Post : BaseEntity
{
    public string? Title { get; set; } = default!;
    public string? Description { get; set; }
    public string? PreviewImageName { get; set; }
    public string? Content { get; set; }
    public DateTime? ScheduledPublishDate { get; set; }
    public bool IsPublished { get; set; }
    public int LikeCount { get; set; }
    public int CategoryId { get; set; }
    public int AuthorUserId { get; set; }

    // Relations
    public Category Category { get; set; } = default!;
    public Identity.User AuthorUser { get; set; } = default!;
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<PostTag>? PostTags { get; set; }
}

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable(nameof(Post), typeof(Post).GetParentFolderName());

        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(2000);
        builder.Property(x => x.LikeCount).HasDefaultValue(0);

        builder.HasOne(x => x.Category).WithMany(x => x.Posts).HasForeignKey(x => x.CategoryId);
        builder.HasOne(x => x.AuthorUser).WithMany(x => x.Posts).HasForeignKey(x => x.AuthorUserId);
    }
}
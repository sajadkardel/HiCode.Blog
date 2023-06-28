using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HC.Shared.Extensions;

namespace HC.Data.Entities.Blog;

public class Post : BaseEntity
{
    public string Title { get; set; } = default!;
    public string? ShortDescription { get; set; }
    public string? PreviewImageUrl { get; set; }
    public string? SubTitle { get; set; }
    public string? Content { get; set; }
    public DateTime? ScheduledPublishDate { get; set; }
    public bool IsPublished { get; set; }
    public int LikeCount { get; set; }

    // Relations
    public int CategoryId { get; set; }
    public int AuthorUserId { get; set; }
    public Category Category { get; set; } = default!;
    public Identity.User User { get; set; } = default!;
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<PostTag>? PostTags { get; set; }
}

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable(nameof(Post), typeof(Post).GetParentFolderName());

        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x => x.SubTitle).HasMaxLength(100);
        builder.Property(x => x.ShortDescription).HasMaxLength(2000);

        builder.HasOne(x => x.Category).WithMany(x => x.Posts).HasForeignKey(x => x.CategoryId);
        builder.HasOne(x => x.User).WithMany(x => x.Posts).HasForeignKey(x => x.AuthorUserId);
    }
}
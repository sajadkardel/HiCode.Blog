using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HC.Shared.Extensions;

namespace HC.Data.Entities.Blog;

public class Comment : BaseEntity
{
    public string Content { get; set; } = default!;
    public int LikeCount { get; set; }

    // Relations
    public int? ParentCommentId { get; set; }
    public int PostId { get; set; }
    public int AuthorUserId { get; set; }
    public Post Post { get; set; }
    public Identity.User User { get; set; }
    public Comment ParentComment { get; set; }
    public ICollection<Comment> ChildComments { get; set; }
}

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable(nameof(Comment), typeof(Comment).GetParentFolderName());

        builder.Property(x => x.Content).IsRequired().HasMaxLength(3000);

        builder.HasOne(x => x.Post).WithMany(x => x.Comments).HasForeignKey(x => x.PostId);
        builder.HasOne(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.AuthorUserId);
        builder.HasOne(x => x.ParentComment).WithMany(x => x.ChildComments).HasForeignKey(x => x.ParentCommentId);
    }
}

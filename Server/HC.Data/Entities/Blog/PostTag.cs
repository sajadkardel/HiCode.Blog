using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HC.Shared.Extensions;

namespace HC.Data.Entities.Blog;

public class PostTag : BaseEntity
{
    public int PostId { get; set; }
    public int TagId { get; set; }

    // Relations
    public Post Post { get; set; }
    public Tag Tag { get; set; }
}

public class PostTagConfiguration : IEntityTypeConfiguration<PostTag>
{
    public void Configure(EntityTypeBuilder<PostTag> builder)
    {
        builder.ToTable(nameof(PostTag), typeof(PostTag).GetParentFolderName());

        builder.HasOne(x => x.Post).WithMany(x => x.PostTags).HasForeignKey(x => x.PostId);
        builder.HasOne(x => x.Tag).WithMany(x => x.PostTags).HasForeignKey(x => x.TagId);
    }
}

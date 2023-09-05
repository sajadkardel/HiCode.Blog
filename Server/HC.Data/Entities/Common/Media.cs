using HC.Shared.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using HC.Shared.Extensions;

namespace HC.Data.Entities.Common;

public class Media : BaseEntity
{
    public string Name { get; set; } = default!;
    public string? Alt { get; set; }
    public string PhysicalPath { get; set; } = default!;
    public long Size { get; set; }
    public MediaSystemType MediaSystemType { get; set; }
    public MediaFileType MediaFileType { get; set; }
    public string MediaFileExtension { get; set; } = default!;
}


public class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.ToTable(nameof(Media), typeof(Media).GetParentFolderName());

        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
        builder.Property(x => x.PhysicalPath).IsRequired().HasMaxLength(3000);
        builder.Property(x => x.Size).IsRequired();
        builder.Property(x => x.MediaSystemType).IsRequired();
        builder.Property(x => x.MediaFileType).IsRequired();
        builder.Property(x => x.MediaFileExtension).IsRequired().HasMaxLength(12);
    }
}
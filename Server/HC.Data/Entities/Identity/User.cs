using HC.Data.Entities.Blog;
using HC.Shared.Enums;
using HC.Shared.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HC.Data.Entities.Identity;

public class User : IdentityUser<int>
{
    public User()
    {
        IsActive = true;
    }

    public string? FullName { get; set; }
    public int? Age { get; set; }
    public GenderType? Gender { get; set; }
    public bool IsActive { get; set; }
    public DateTimeOffset? LastLoginDate { get; set; }

    // Relations
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Post>? Posts { get; set; }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User), typeof(User).GetParentFolderName());

        builder.Property(p => p.UserName).IsRequired().HasMaxLength(50);
        builder.Property(p => p.FullName).IsRequired().HasMaxLength(50);
    }
}

using System.ComponentModel.DataAnnotations;
using HC.Shared.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HC.DataAccess.Entities.User;

public class Role : IdentityRole<int>
{
    [Required]
    [StringLength(100)]
    public string Description { get; set; }
}

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(nameof(Role), typeof(Role).GetParentFolderName());
        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
    }
}

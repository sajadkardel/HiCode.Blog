using HC.Shared.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HC.Data.Entities.Identity;

public class UserRole : IdentityUserRole<int>
{
}

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable(nameof(UserRole), typeof(UserRole).GetParentFolderName());

        #region Create Admin
        builder.HasData(new UserRole
        {
            UserId = 1,
            RoleId = 1
        });
        #endregion
    }
}

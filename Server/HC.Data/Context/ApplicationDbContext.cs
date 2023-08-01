using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HC.Shared.Extensions;
using HC.Common.Extensions;
using HC.Data.Entities;
using HC.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace HC.Data.Context;

public class ApplicationDbContext : IdentityDbContext<User, Role, int>
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles", typeof(User).GetParentFolderName());
        builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims", typeof(User).GetParentFolderName());
        builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins", typeof(User).GetParentFolderName());
        builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens", typeof(User).GetParentFolderName());
        builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims", typeof(User).GetParentFolderName());

        var entitiesAssembly = typeof(ApplicationDbContext).Assembly;

        builder.RegisterAllEntities<BaseEntity>(entitiesAssembly);
        builder.ApplyConfigurationsFromAssembly(entitiesAssembly);
        builder.AddRestrictDeleteBehaviorConvention();
        builder.AddSequentialGuidForIdConvention();
        builder.AddPluralizingTableNameConvention();
        builder.ApplySoftQueryFilter("IsDeleted", false);
    }

    public override int SaveChanges()
    {
        _setDefaults();
        _cleanString();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        _setDefaults();
        _cleanString();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        _setDefaults();
        _cleanString();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        _setDefaults();
        _cleanString();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void _cleanString()
    {
        var changedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

        foreach (var item in changedEntities)
        {
            if (item.Entity == null) continue;

            var properties = item.Entity.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

            foreach (var property in properties)
            {
                var propName = property.Name;
                string? val = property.GetValue(item.Entity, null)?.ToString();

                if (val is not null && val.HasValue())
                {
                    var newVal = val.Fa2En().FixPersianChars();
                    if (newVal == val) continue;
                    property.SetValue(item.Entity, newVal, null);
                }
            }
        }
    }

    private void _setDefaults()
    {
        var changedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted);

        foreach (var item in changedEntities)
        {
            if (item.Entity is BaseEntity entity)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        entity.CreateDate = DateTime.Now;
                        entity.IsDeleted = false;
                        break;

                    case EntityState.Modified:
                        entity.LastModifyDate = DateTime.Now;
                        break;

                    case EntityState.Deleted:
                        item.State = EntityState.Modified;
                        entity.DeleteDate = DateTime.Now;
                        entity.IsDeleted = true;
                        break;
                }

                entity.LastChangerUserId = 1;
            }
        }
    }
}

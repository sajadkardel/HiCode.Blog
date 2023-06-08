using HC.DataAccess.Context;
using HC.Entity.Identity;
using HC.Common.Markers;
using HC.Service.Contracts;

namespace HC.Service.Implementations;

public class UserRepository : Repository<User>, IUserRepository, IScopedDependency
{
    public UserRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken)
    {
        //user.SecurityStamp = Guid.NewGuid();
        return UpdateAsync(user, cancellationToken);
    }

    public Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken)
    {
        user.LastLoginDate = DateTimeOffset.Now;
        return UpdateAsync(user, cancellationToken);
    }
}

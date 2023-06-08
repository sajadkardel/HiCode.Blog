using HC.DataAccess.Context;
using HC.Entity.Identity;
using HC.Service.Implementations;
using HC.Domain.Contracts;
using HC.Shared.Markers;

namespace HC.Domain.Implementations;

public class UserRepository : Repository<User>, IUserRepository, IScopedDependency
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken)
    {
        //user.SecurityStamp = Guid.NewGuid();
        return UpdateAsync(user, cancellationToken);
    }
}

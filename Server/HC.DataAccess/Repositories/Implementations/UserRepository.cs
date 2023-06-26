using HC.DataAccess.Context;
using HC.Shared.Markers;
using HC.DataAccess.Entities.User;
using HC.DataAccess.Repositories.Contracts;

namespace HC.DataAccess.Repositories.Implementations;

public class UserRepository : Repository<User>, IUserRepository, IScopedDependency
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}

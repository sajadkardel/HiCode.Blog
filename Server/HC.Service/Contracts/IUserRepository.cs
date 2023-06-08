using HC.Entity.Identity;

namespace HC.Service.Contracts;

public interface IUserRepository : IRepository<User>
{
    Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken);
    Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken);
}
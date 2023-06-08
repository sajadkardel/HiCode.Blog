using HC.Entity.Identity;
using HC.Service.Contracts;

namespace HC.Domain.Contracts;

public interface IUserRepository : IRepository<User>
{
    Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken);
}
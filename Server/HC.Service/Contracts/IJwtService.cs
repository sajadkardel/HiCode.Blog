using System.Threading.Tasks;
using HC.Entity.Identity;
using HC.Shared.Dtos.Identity;

namespace HC.Service.Contracts;

public interface IJwtService
{
    Task<AccessToken> GenerateTokenAsync(User user);
}
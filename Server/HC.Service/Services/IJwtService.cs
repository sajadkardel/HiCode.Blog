using System.Threading.Tasks;
using HC.Entity.Identity;

namespace HC.Service.Services;

public interface IJwtService
{
    Task<AccessToken> GenerateAsync(User user);
}
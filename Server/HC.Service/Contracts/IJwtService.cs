using System.Threading.Tasks;
using HC.Common.Models;
using HC.Entity.Identity;

namespace HC.Service.Contracts;

public interface IJwtService
{
    Task<AccessToken> GenerateAsync(User user);
}
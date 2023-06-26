using HC.DataAccess.Context;
using HC.Service.Implementations;
using HC.Domain.Contracts;
using HC.Shared.Markers;
using HC.DataAccess.Entities.User;
using HC.Shared.Dtos.User;
using Microsoft.EntityFrameworkCore;

namespace HC.Domain.Implementations;

public class UserRepository : Repository<User>, IUserRepository, IScopedDependency
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<UserResponseDto>> GetAllUser(CancellationToken cancellationToken)
    {
        var result = await TableNoTracking.ToListAsync(cancellationToken);

        List<UserResponseDto> response = result.Select(x => new UserResponseDto
        {
           UserName = x.UserName,
           FullName = x.FullName,
           Email = x.Email,
           PhoneNumber = x.PhoneNumber,
           Age = x.Age,
           Gender = x.Gender,
           IsActive = x.IsActive,
           LastLoginDate = x.LastLoginDate

        }).ToList();

        return response;
    }

    public async Task<UserResponseDto> GetUserById(CancellationToken cancellationToken, int id)
    {
        var result = await GetByIdAsync(cancellationToken ,id);

        var response = new UserResponseDto
        {
            UserName = result.UserName,
            FullName = result.FullName,
            Email = result.Email,
            PhoneNumber = result.PhoneNumber,
            Age = result.Age,
            Gender = result.Gender,
            IsActive = result.IsActive,
            LastLoginDate = result.LastLoginDate

        };

        return response;
    }
}

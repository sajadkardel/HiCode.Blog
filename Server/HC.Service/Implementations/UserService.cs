using HC.DataAccess.Repositories.Contracts;
using HC.Service.Contracts;
using HC.Shared.Dtos.User;
using HC.Shared.Markers;
using Microsoft.EntityFrameworkCore;

namespace HC.Service.Implementations;

public class UserService : IUserService, IScopedDependency
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserResponseDto>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _userRepository.TableNoTracking.ToListAsync(cancellationToken);

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

    public async Task<UserResponseDto> GetById(CancellationToken cancellationToken, int id)
    {
        var result = await _userRepository.GetByIdAsync(cancellationToken, id);

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

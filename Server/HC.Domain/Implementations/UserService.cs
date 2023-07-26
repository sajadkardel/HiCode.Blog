using HC.Data.Entities.Identity;
using HC.Data.Repositories.Contracts;
using HC.Shared.Dtos.User;
using HC.Shared.Markers;
using HC.Shared.Models;
using HC.Shared.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HC.Domain.Implementations;

public class UserService : IUserService, IScopedDependency
{
    private readonly IRepository<User> _userRepository;

    public UserService(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<List<UserResponseDto>>> GetAll(CancellationToken cancellationToken = default)
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

        return Result.Success(response);
    }

    public async Task<Result<UserResponseDto>> GetById(int id, CancellationToken cancellationToken = default)
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

        return Result.Success(response);
    }
}

using HC.Shared.Constants;
using HC.Shared.Dtos.User;
using HC.Shared.Markers;
using HC.Shared.Models;
using HC.Web.Services.Contracts;

namespace HC.Web.Services.Implementations;

public class UserService : IUserService, IScopedDependency
{
    private readonly IApiCaller _apiCaller;

    public UserService(IApiCaller apiCaller)
    {
        _apiCaller = apiCaller;
    }

    public async Task<ApiResult<List<UserResponseDto>>> GetAll()
    {
        var response = await _apiCaller.GetAsync<List<UserResponseDto>>(RoutingConstants.ServerSide.User.GetAll);
        return response;
    }

    public async Task<ApiResult<UserResponseDto>> GetById(int id)
    {
        var response = await _apiCaller.GetAsync<UserResponseDto>(RoutingConstants.ServerSide.User.GetById);
        return response;
    }
}

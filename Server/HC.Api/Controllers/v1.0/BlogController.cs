using HC.Service.Contracts;
using HC.Shared.Constants;
using HC.Shared.Dtos.User;
using HC.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HC.Api.Controllers.v1;

[ApiVersion("1.0")]
public class BlogController : BaseController
{
    private readonly IBlogService _blogService;

	public BlogController(IBlogService blogService)
	{
		_blogService = blogService;
	}

    [AllowAnonymous]
    [HttpGet(RoutingConstants.ServerSide.Blog.GetAll)]
    public virtual async Task<ApiResult<List<UserResponseDto>>> GetAll(CancellationToken cancellationToken = default)
    {
        var result = await _blogService.GetAll(cancellationToken);
        return ApiResult.Success(result);
    }

    [AllowAnonymous]
    [HttpGet(RoutingConstants.ServerSide.Blog.GetById)]
    public virtual async Task<ApiResult<UserResponseDto>> GetById([FromQuery] int id, CancellationToken cancellationToken = default)
    {
        var result = await _blogService.GetById(id, cancellationToken);
        return ApiResult.Success(result);
    }

    [HttpPost(RoutingConstants.ServerSide.Blog.Create)]
    public virtual async Task<ApiResult> Create([FromBody] UserRequestDto dto, CancellationToken cancellationToken = default)
    {
        return ApiResult.Success();
    }

    [HttpPut(RoutingConstants.ServerSide.Blog.Update)]
    public virtual async Task<ApiResult> Update([FromQuery] int id, [FromBody] UserRequestDto dto, CancellationToken cancellationToken = default)
    {
        return ApiResult.Success();
    }

    [HttpDelete(RoutingConstants.ServerSide.Blog.Delete)]
    public virtual async Task<ApiResult> Delete([FromQuery] int id, CancellationToken cancellationToken = default)
    {
        return ApiResult.Success();
    }
}

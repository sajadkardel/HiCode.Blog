using HC.Service.Contracts;
using HC.Shared.Constants;
using HC.Shared.Dtos.Blog;
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

    #region Post
    [AllowAnonymous]
    [HttpGet(RoutingConstants.ServerSide.Blog.GetAllPost)]
    public virtual async Task<Result<List<PostResponseDto>>> GetAllPost(CancellationToken cancellationToken = default)
    {
        var result = await _blogService.GetAllPost(cancellationToken);
        return Result.Success(result.Data);
    }

    [AllowAnonymous]
    [HttpGet(RoutingConstants.ServerSide.Blog.GetPostById)]
    public virtual async Task<Result<PostResponseDto>> GetPostById([FromQuery] int id, CancellationToken cancellationToken = default)
    {
        var result = await _blogService.GetPostById(id, cancellationToken);
        return Result.Success(result.Data);
    }

    [HttpPost(RoutingConstants.ServerSide.Blog.CreatePost)]
    public virtual async Task<Result> CreatePost([FromBody] PostRequestDto dto, CancellationToken cancellationToken = default)
    {
        return Result.Success();
    }

    [HttpPut(RoutingConstants.ServerSide.Blog.UpdatePost)]
    public virtual async Task<Result> UpdatePost([FromQuery] int id, [FromBody] PostRequestDto dto, CancellationToken cancellationToken = default)
    {
        return Result.Success();
    }

    [HttpDelete(RoutingConstants.ServerSide.Blog.DeletePost)]
    public virtual async Task<Result> DeletePost([FromQuery] int id, CancellationToken cancellationToken = default)
    {
        return Result.Success();
    }
    #endregion
}

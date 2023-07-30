using HC.Shared.Constants;
using HC.Shared.Dtos.Blog;
using HC.Shared.Models;
using HC.Shared.Services.Contracts;
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

    #region Category
    [AllowAnonymous]
    [HttpGet(RoutingConstants.ServerSide.Blog.GetAllCategory)]
    public virtual async Task<Result<IEnumerable<CategoryResponseDto>>> GetAllCategory(CancellationToken cancellationToken = default)
    {
        var result = await _blogService.GetAllCategory(cancellationToken);
        if (result.IsSucceed is false) return Result.Failed<IEnumerable<CategoryResponseDto>>(result.Message);

        return Result.Success(result.Data);
    }

    [HttpPost(RoutingConstants.ServerSide.Blog.CreateCategory)]
    public virtual async Task<Result> CreateCategory(CategoryRequestDto request, CancellationToken cancellationToken = default)
    {
        var result = await _blogService.CreateCategory(request, cancellationToken);
        if (result.IsSucceed is false) return Result.Failed(result.Message);

        return Result.Success();
    }
    #endregion

    #region Post
    [AllowAnonymous]
    [HttpGet(RoutingConstants.ServerSide.Blog.GetAllPost)]
    public virtual async Task<Result<IEnumerable<PostResponseDto>>> GetAllPost(CancellationToken cancellationToken = default)
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
        await _blogService.CreatePost(dto, cancellationToken);
        return Result.Success();
    }

    [HttpPut(RoutingConstants.ServerSide.Blog.UpdatePost)]
    public virtual async Task<Result> UpdatePost([FromBody] PostRequestDto dto, CancellationToken cancellationToken = default)
    {
        await _blogService.UpdatePost(dto, cancellationToken);
        return Result.Success();
    }

    [HttpDelete(RoutingConstants.ServerSide.Blog.DeletePost)]
    public virtual async Task<Result> DeletePost([FromQuery] int id, CancellationToken cancellationToken = default)
    {
        await _blogService.DeletePost(id, cancellationToken);
        return Result.Success();
    }
    #endregion

    #region Tag
    #endregion

    #region Comment
    #endregion
}

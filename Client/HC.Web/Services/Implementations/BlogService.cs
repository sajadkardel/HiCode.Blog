using HC.Shared.Constants;
using HC.Shared.Dtos.Blog;
using HC.Shared.Markers;
using HC.Shared.Models;
using HC.Shared.Services.Contracts;
using HC.Web.Services.Contracts;

namespace HC.Web.Services.Implementations;

public class BlogService : IBlogService, IScopedDependency
{
    private readonly IApiCaller _apiCaller;
    public BlogService(IApiCaller apiCaller)
    {
        _apiCaller = apiCaller;
    }

    #region Category
    public async Task<Result<IEnumerable<CategoryResponseDto>>> GetAllCategory(CancellationToken cancellationToken = default)
    {
        var response = await _apiCaller.GetAsync<IEnumerable<CategoryResponseDto>>(RoutingConstants.ServerSide.Blog.GetAllCategory, cancelationToken: cancellationToken);
        if (response.IsSucceed is false) return Result.Failed<IEnumerable<CategoryResponseDto>>(response.Message);
        return response;
    }

    public async Task<Result> CreateCategory(CategoryRequestDto request, CancellationToken cancellationToken = default)
    {
        var response = await _apiCaller.PostAsync<CategoryResponseDto, CategoryRequestDto>(RoutingConstants.ServerSide.Blog.CreateCategory, request, cancelationToken: cancellationToken);
        if (response.IsSucceed is false) return Result.Failed(response.Message);
        return response;
    }
    #endregion

    #region Post
    public Task<Result<List<PostResponseDto>>> GetAllPost(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<PostResponseDto>> GetPostById(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> CreatePost(PostRequestDto dto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdatePost(int id, PostRequestDto dto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeletePost(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    #endregion
}

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
        var url = RoutingConstants.ServerSide.Blog.GetAllCategory;
        var response = await _apiCaller.GetAsync<IEnumerable<CategoryResponseDto>>(url, cancelationToken: cancellationToken);
        if (response.IsSucceed is false) return Result.Failed<IEnumerable<CategoryResponseDto>>(response.Message);
        return response;
    }

    public async Task<Result> CreateCategory(CategoryRequestDto request, CancellationToken cancellationToken = default)
    {
        var url = RoutingConstants.ServerSide.Blog.CreateCategory;
        var response = await _apiCaller.PostAsync(url, request, cancelationToken: cancellationToken);
        if (response.IsSucceed is false) return Result.Failed(response.Message);
        return response;
    }

    public async Task<Result> UpdateCategory(CategoryRequestDto request, CancellationToken cancellationToken = default)
    {
        var url = RoutingConstants.ServerSide.Blog.UpdateCategory;
        var response = await _apiCaller.PutAsync(url, request, cancelationToken: cancellationToken);
        if (response.IsSucceed is false) return Result.Failed(response.Message);
        return response;
    }

    public async Task<Result> DeleteCategory(int id, CancellationToken cancellationToken = default)
    {
        var url = $"{RoutingConstants.ServerSide.Blog.DeleteCategory}?id={id}";
        var response = await _apiCaller.DeleteAsync(url, cancelationToken: cancellationToken);
        if (response.IsSucceed is false) return Result.Failed(response.Message);
        return response;
    }
    #endregion

    #region Post
    public async Task<Result<IEnumerable<PostResponseDto>>> GetAllPost(CancellationToken cancellationToken = default)
    {
        var url = RoutingConstants.ServerSide.Blog.GetAllPost;
        var response = await _apiCaller.GetAsync<IEnumerable<PostResponseDto>>(url, cancelationToken: cancellationToken);
        if (response.IsSucceed is false) return Result.Failed<IEnumerable<PostResponseDto>>(response.Message);
        return response;
    }

    public async Task<Result<PostResponseDto>> GetPostById(int id, CancellationToken cancellationToken = default)
    {
        var url = RoutingConstants.ServerSide.Blog.GetPostById;
        var response = await _apiCaller.GetAsync<PostResponseDto>(url, cancelationToken: cancellationToken);
        if (response.IsSucceed is false) return Result.Failed<PostResponseDto>(response.Message);
        return response;
    }

    public async Task<Result> CreatePost(PostRequestDto dto, CancellationToken cancellationToken = default)
    {
        var url = RoutingConstants.ServerSide.Blog.CreatePost;
        var response = await _apiCaller.PostAsync(url, dto, cancelationToken: cancellationToken);
        if (response.IsSucceed is false) return Result.Failed(response.Message);
        return response;
    }

    public async Task<Result> UpdatePost(PostRequestDto dto, CancellationToken cancellationToken = default)
    {
        var url = RoutingConstants.ServerSide.Blog.UpdatePost;
        var response = await _apiCaller.PutAsync(url, dto, cancelationToken: cancellationToken);
        if (response.IsSucceed is false) return Result.Failed(response.Message);
        return response;
    }

    public async Task<Result> DeletePost(int id, CancellationToken cancellationToken = default)
    {
        var url = $"{RoutingConstants.ServerSide.Blog.DeletePost}?id={id}";
        var response = await _apiCaller.DeleteAsync(url, cancelationToken: cancellationToken);
        if (response.IsSucceed is false) return Result.Failed(response.Message);
        return response;
    }
    #endregion

    #region Tag
    #endregion

    #region Comment
    #endregion
}

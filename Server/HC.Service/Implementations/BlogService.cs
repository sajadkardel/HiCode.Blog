using HC.Data.Entities.Blog;
using HC.Data.Repositories.Contracts;
using HC.Service.Contracts;
using HC.Shared.Dtos.Blog;
using HC.Shared.Markers;
using HC.Shared.Models;

namespace HC.Service.Implementations;

public class BlogService : IBlogService, IScopedDependency
{
    private readonly IRepository<Post> _postRepository;

    public BlogService(IRepository<Post> postRepository)
    {
        _postRepository = postRepository;
    }

    #region Category
    public Task<Result<List<PostResponseDto>>> GetAllCategory(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Post
    public async Task<Result> CreatePost(PostRequestDto dto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> DeletePost(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<PostResponseDto>>> GetAllPost(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PostResponseDto>> GetPostById(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> UpdatePost(int id, PostRequestDto dto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region Tag
    #endregion

    #region Comment
    #endregion
}

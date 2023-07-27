using HC.Shared.Dtos.Blog;
using HC.Shared.Dtos.User;
using HC.Shared.Models;

namespace HC.Shared.Services.Contracts;

public interface IBlogService
{
    #region Category
    public Task<Result<IEnumerable<CategoryResponseDto>>> GetAllCategory(CancellationToken cancellationToken = default);
    public Task<Result> CreateCategory(CategoryRequestDto request, CancellationToken cancellationToken = default);
    public Task<Result> DeleteCategory(int id, CancellationToken cancellationToken = default);
    public Task<Result> UpdateCategory(int id, CategoryRequestDto request, CancellationToken cancellationToken = default);
    #endregion

    #region Post
    public Task<Result<List<PostResponseDto>>> GetAllPost(CancellationToken cancellationToken = default);
    public Task<Result<PostResponseDto>> GetPostById(int id, CancellationToken cancellationToken = default);
    public Task<Result> CreatePost(PostRequestDto dto, CancellationToken cancellationToken = default);
    public Task<Result> UpdatePost(int id, PostRequestDto dto, CancellationToken cancellationToken = default);
    public Task<Result> DeletePost(int id, CancellationToken cancellationToken = default);
    #endregion

    #region Tag
    #endregion

    #region Comment
    #endregion
}

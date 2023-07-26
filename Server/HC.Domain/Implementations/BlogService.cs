using HC.Data.Entities.Blog;
using HC.Data.Repositories.Contracts;
using HC.Shared.Dtos.Blog;
using HC.Shared.Markers;
using HC.Shared.Models;
using HC.Shared.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace HC.Domain.Implementations;

public class BlogService : IBlogService, IScopedDependency
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IRepository<Post> _postRepository;

    public BlogService(IRepository<Post> postRepository, IRepository<Category> categoryRepository)
    {
        _postRepository = postRepository;
        _categoryRepository = categoryRepository;
    }

    #region Category
    public async Task<Result<IEnumerable<CategoryResponseDto>>> GetAllCategory(CancellationToken cancellationToken = default)
    {
        // Just in 2 level
        // Category => Sub-Categories

        var categories = await _categoryRepository.TableNoTracking
            .Include(x => x.ChildCategories)
            .ToListAsync(cancellationToken);

        var result = categories.Select(x => new CategoryResponseDto
        {
            Id = x.Id,
            Name = x.Name,
            IconName = x.IconName,
            ChildCategories = x.ChildCategories.Select(x => new CategoryResponseDto 
            {
                Id = x.Id,
                Name = x.Name,
                IconName = x.IconName

            }).ToList(),
        });

        return Result.Success(result);
    }

    public async Task<Result> CreateCategory(CategoryRequestDto request, CancellationToken cancellationToken = default)
    {
        await _categoryRepository.AddAsync(new Category
        {
            ParentCategoryId = request.ParentId,
            Name = request.Name,
            IconName = request.IconName
        });

        return Result.Success();
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

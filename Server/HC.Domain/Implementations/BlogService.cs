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
        if (categories is null) return Result.Failed<IEnumerable<CategoryResponseDto>>("موردی یافت نشد.");

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
        }, cancellationToken: cancellationToken);

        return Result.Success();
    }

    public async Task<Result> UpdateCategory(CategoryRequestDto request, CancellationToken cancellationToken = default)
    {
        await _categoryRepository.UpdateAsync(new Category
        {
            Id = request.Id,
            Name = request.Name,
            IconName = request.IconName
        }, cancellationToken: cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeleteCategory(int id, CancellationToken cancellationToken = default)
    {
        await _categoryRepository.DeleteAsync(new Category { Id = id }, cancellationToken: cancellationToken);

        return Result.Success();
    }
    #endregion

    #region Post
    public async Task<Result<IEnumerable<PostResponseDto>>> GetAllPost(CancellationToken cancellationToken = default)
    {
        var posts = await _postRepository.TableNoTracking.Include(x => x.AuthorUser).ToListAsync(cancellationToken);
        if (posts is null) return Result.Failed<IEnumerable<PostResponseDto>>("موردی یافت نشد.");

        var result = posts.Select(x => new PostResponseDto
        {
            Title = x.Title,
            Description = x.Description,
            Content = x.Content,
            //PreviewImage = new byte[0],
            CategoryId = x.CategoryId,
            PublishDate = x.PublishDate,
            LikeCount = x.LikeCount,
            AuthorName = x.AuthorUser.FullName
        });

        return Result.Success(result);
    }

    public async Task<Result<PostResponseDto>> GetPostById(int id, CancellationToken cancellationToken = default)
    {
        var post = await _postRepository.GetByIdAsync(cancellationToken, id);
        if (post is null) return Result.Failed<PostResponseDto>("موردی یافت نشد.");

        var result = new PostResponseDto
        {
            Title = post.Title,
            Description = post.Description,
            Content = post.Content,
            //PreviewImage = new byte[0],
            CategoryId = post.CategoryId,
            PublishDate = post.PublishDate,
            LikeCount = post.LikeCount,
            AuthorName = post.AuthorUser.FullName
        };

        return Result.Success(result);
    }

    public async Task<Result> CreatePost(PostRequestDto request, CancellationToken cancellationToken = default)
    {
        await _postRepository.AddAsync(new Post
        {
            Title = request.Title,
            Description = request.Description,
            Content = request.Content,
            CategoryId = request.CategoryId,
            PreviewImageName = request.PreviewImageName,
            ScheduledPublishDate = request.ScheduledPublishDate
        }, cancellationToken: cancellationToken);

        return Result.Success();
    }

    public async Task<Result> UpdatePost(PostRequestDto request, CancellationToken cancellationToken = default)
    {
        await _postRepository.UpdateAsync(new Post
        {
            Id = request.Id,
            Title = request.Title,
            Description = request.Description,
            Content = request.Content,
            CategoryId = request.CategoryId,
            PreviewImageName = request.PreviewImageName,
            ScheduledPublishDate = request.ScheduledPublishDate
        }, cancellationToken: cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeletePost(int id, CancellationToken cancellationToken = default)
    {
        await _postRepository.DeleteAsync(new Post { Id = id }, cancellationToken: cancellationToken);

        return Result.Success();
    }
    #endregion

    #region Tag
    #endregion

    #region Comment
    #endregion
}

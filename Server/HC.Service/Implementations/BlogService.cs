using HC.Data.Entities.Blog;
using HC.Data.Repositories.Contracts;
using HC.Service.Contracts;
using HC.Shared.Dtos.User;
using HC.Shared.Markers;

namespace HC.Service.Implementations;

public class BlogService : IBlogService, IScopedDependency
{
    private readonly IRepository<Post> _postRepository;

    public BlogService(IRepository<Post> postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<List<UserResponseDto>> GetAll(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<UserResponseDto> GetById(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}

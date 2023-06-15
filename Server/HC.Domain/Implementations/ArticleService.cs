using HC.DataAccess.Context;
using HC.DataAccess.Entities.Blog;
using HC.Service.Implementations;
using HC.Shared.Markers;

namespace HC.Domain.Implementations;

public class ArticleService : Repository<Article>, IScopedDependency
{
    public ArticleService(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}

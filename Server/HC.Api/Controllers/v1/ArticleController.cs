using AutoMapper;
using HC.Api.Dto.Product;
using HC.DataAccess.Contracts;
using HC.Entity.Blog;
using HC.Infrastructure.Api;

namespace HC.Api.Controllers.v1;

public class ArticleController : CrudController<ArticleDto, ArticleSelectDto, Article, int>
{
    public ArticleController(IRepository<Article> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}

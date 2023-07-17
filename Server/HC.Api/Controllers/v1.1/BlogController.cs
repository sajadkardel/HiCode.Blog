using HC.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HC.Api.Controllers.v2;

[ApiVersion("1.1")]
public class BlogController : v1.BlogController
{
    public BlogController(IBlogService blogService) : base(blogService)
    {
    }
}

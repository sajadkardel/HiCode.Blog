using HC.Web.Shared;
using Microsoft.AspNetCore.Components;

namespace HC.Web.Pages.Blog;

public partial class ReadPost : AppBaseComponent
{
    [Parameter] public int PostId { get; set; }
}

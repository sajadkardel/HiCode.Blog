using HC.Web.Shared;
using Microsoft.AspNetCore.Components;

namespace HC.Web.Pages;

public partial class BlogPost : AppBaseComponent
{
    [Parameter] public int Id { get; set; }
}

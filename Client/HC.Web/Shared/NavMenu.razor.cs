using HC.Shared.Dtos.Blog;
using HC.Shared.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;

namespace HC.Web.Shared;

public partial class NavMenu : AppBaseComponent
{
    [Parameter] public bool IsOpenNav { get; set; }
    [Parameter] public EventCallback OnToggleNav { get; set; }

    private async Task ToggleNav()
    {
        //await OnToggleNav.InvokeAsync();
    }
}

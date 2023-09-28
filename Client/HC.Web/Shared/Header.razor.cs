
using Microsoft.AspNetCore.Components;

namespace HC.Web.Shared;

public partial class Header : AppBaseComponent
{
    [Parameter] public bool IsOpenNav { get; set; }
    [Parameter] public EventCallback OnToggleNav { get; set; }

    private async Task ToggleNav()
    {
        await OnToggleNav.InvokeAsync();
    }
}

using HC.Web.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace HC.Web.Shared;

public partial class MainLayout
{
    private bool _isOpenNav;

    [Inject] private IJSRuntime _jsRuntime { get; set; } = default!;

    private async Task ToggleNavHandler()
    {
        _isOpenNav = !_isOpenNav;

        await _jsRuntime.SetBodyOverflow(_isOpenNav);
    }
}

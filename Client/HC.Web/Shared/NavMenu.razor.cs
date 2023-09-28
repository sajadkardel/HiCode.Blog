using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace HC.Web.Shared;

public partial class NavMenu : AppBaseComponent, IDisposable
{
    [Inject] public NavigationManager Navigation { get; set; } = default!;

    [Parameter] public bool IsOpenNav { get; set; }
    [Parameter] public EventCallback OnToggleNav { get; set; }

    protected override void OnInitialized()
    {
        Navigation.LocationChanged += HandleLocationChanged;
    }

    private void HandleLocationChanged(object sender, LocationChangedEventArgs args)
    {
        OnToggleNav.InvokeAsync().GetAwaiter();
    }

    public void Dispose()
    {
        Navigation.LocationChanged -= HandleLocationChanged;
    }
}

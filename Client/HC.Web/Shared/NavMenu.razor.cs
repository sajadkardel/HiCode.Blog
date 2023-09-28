<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
=======
﻿using HC.Shared.Dtos.Blog;
using HC.Shared.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;
>>>>>>> d19f618a4461ed9ca1f7a03b5263c4377b1828ae

namespace HC.Web.Shared;

public partial class NavMenu : AppBaseComponent, IDisposable
{
<<<<<<< HEAD
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
=======
    [Parameter] public bool IsOpenNav { get; set; }
    [Parameter] public EventCallback OnToggleNav { get; set; }

    private async Task ToggleNav()
    {
        //await OnToggleNav.InvokeAsync();
>>>>>>> d19f618a4461ed9ca1f7a03b5263c4377b1828ae
    }
}

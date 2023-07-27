using Microsoft.AspNetCore.Components;

namespace HC.Web.Shared;

public partial class LoadingComponent : AppBaseComponent
{
    [Parameter] public string Color { get; set; } = string.Empty;
}

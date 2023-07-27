using HC.Shared.Dtos.Blog;
using HC.Shared.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace HC.Web.Shared;

public partial class NavMenu : AppBaseComponent
{
    [Inject] protected IBlogService _blogService { get; set; } = default!;

    private string? _error;
    private IEnumerable<CategoryResponseDto>? _categories;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
    }
}

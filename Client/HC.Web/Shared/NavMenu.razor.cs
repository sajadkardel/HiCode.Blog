using HC.Shared.Dtos.Blog;
using HC.Shared.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace HC.Web.Shared;

public partial class NavMenu
{
    [Inject] protected IBlogService _blogService { get; set; } = default!;

    private string? _error;
    private IEnumerable<CategoryResponseDto>? _categories;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        var categories = await _blogService.GetAllCategory();
        if (categories.IsSucceed is false) _error = categories.Message;
        else _categories = categories.Data;

        StateHasChanged();
    }
}

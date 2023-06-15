using HC.Shared.Markers;
using HC.Web.Services.Contracts;
using Microsoft.JSInterop;

namespace HC.Web.Services.Implementations;

public class StorageService : IStorageService, IScopedDependency
{
    private readonly IJSRuntime _jSRuntime;

    public StorageService(IJSRuntime jSRuntime)
    {
        _jSRuntime = jSRuntime;
    }

    // Local Storage
    public async Task<string> GetFromLocalStorageAsync(string key)
    {
        return await _jSRuntime.InvokeAsync<string>("", key);
    }

    public async Task SetToLocalStorageAsync(string key, string value)
    {
        await _jSRuntime.InvokeVoidAsync("", key, value);
    }

    public async Task RemoveFromLocalStorageAsync(string key)
    {
        await _jSRuntime.InvokeVoidAsync("", key);
    }

    // Cookie
    public async Task SetToCookieAsync(string key, string value)
    {
        await _jSRuntime.InvokeVoidAsync("App.setCookie", key, value);
    }

    public async Task<string> GetFromCookieAsync(string key)
    {
        return await _jSRuntime.InvokeAsync<string>("App.getCookie", key);
    }

    public async Task RemoveFromCookieAsync(string key)
    {
        await _jSRuntime.InvokeVoidAsync("App.removeCookie", key);
    }
}

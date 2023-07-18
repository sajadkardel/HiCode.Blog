using HC.Shared.Markers;
using HC.Shared.Models;
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
    public async Task<Result<string>> GetFromLocalStorageAsync(string key)
    {
        string result = string.Empty;

        try
        {
            result = await _jSRuntime.InvokeAsync<string>("localStorage.getItem", key);
        }
        catch (Exception ex)
        {
            return Result.Failed<string>(ex.Message);
        }

        return Result.Success<string>(result);
    }

    public async Task<Result> SetToLocalStorageAsync(string key, string value)
    {
        try
        {
            await _jSRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
        }
        catch (Exception ex)
        {
            return Result.Failed(ex.Message);
        }
        
        return Result.Success();
    }

    public async Task<Result> RemoveFromLocalStorageAsync(string key)
    {
        try
        {
            await _jSRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
        catch (Exception ex)
        {
            return Result.Failed(ex.Message);
        }
        
        return Result.Success();
    }

    // Cookie
    public async Task<Result> SetToCookieAsync(string key, string value, long expiresIn)
    {
        try
        {
            await _jSRuntime.InvokeVoidAsync("App.setCookie", key, value, expiresIn);
        }
        catch (Exception ex)
        {
            return Result.Failed(ex.Message);
        }

        return Result.Success();
    }

    public async Task<Result<string>> GetFromCookieAsync(string key)
    {
        string result = string.Empty;

        try
        {
            result = await _jSRuntime.InvokeAsync<string>("App.getCookie", key);
        }
        catch (Exception ex)
        {
            return Result.Failed<string>(ex.Message);
        }

        return Result.Success<string>(result);
    }

    public async Task<Result> RemoveFromCookieAsync(string key)
    {
        try
        {
            await _jSRuntime.InvokeVoidAsync("App.removeCookie", key);
        }
        catch (Exception ex)
        {
            return Result.Failed(ex.Message);
        }

        return Result.Success();
    }
}

using HC.Shared.Models;

namespace HC.Web.Services.Contracts;

public interface IStorageService
{
    // Local Storage
    public Task<Result> SetToLocalStorageAsync(string key, string value);
    public Task<Result<string>> GetFromLocalStorageAsync(string key);
    public Task<Result> RemoveFromLocalStorageAsync(string key);

    // Cookie
    public Task<Result> SetToCookieAsync(string key, string value, long expiresIn);
    public Task<Result<string>> GetFromCookieAsync(string key);
    public Task<Result> RemoveFromCookieAsync(string key);
}

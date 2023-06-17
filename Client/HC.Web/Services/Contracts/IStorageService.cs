
namespace HC.Web.Services.Contracts;

public interface IStorageService
{
    // Local Storage
    public Task SetToLocalStorageAsync(string key, string value);
    public Task<string> GetFromLocalStorageAsync(string key);
    public Task RemoveFromLocalStorageAsync(string key);

    // Cookie
    public Task SetToCookieAsync(string key, string value, long expiresIn);
    public Task<string> GetFromCookieAsync(string key);
    public Task RemoveFromCookieAsync(string key);
}

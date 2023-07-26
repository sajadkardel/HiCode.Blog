using HC.Shared.Models;

namespace HC.Web.Services.Contracts;

public interface IStorageService
{
    // Local Storage
    public Task<Result> SetToLocalStorageAsync(string key, string value, CancellationToken cancellationToken = default);
    public Task<Result<string>> GetFromLocalStorageAsync(string key, CancellationToken cancellationToken = default);
    public Task<Result> RemoveFromLocalStorageAsync(string key, CancellationToken cancellationToken = default);

    // Cookie
    public Task<Result> SetToCookieAsync(string key, string value, long expiresIn, CancellationToken cancellationToken = default);
    public Task<Result<string>> GetFromCookieAsync(string key, CancellationToken cancellationToken = default);
    public Task<Result> RemoveFromCookieAsync(string key, CancellationToken cancellationToken = default);
}

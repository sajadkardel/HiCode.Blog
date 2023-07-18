using HC.Shared.Constants;
using HC.Shared.Models;

namespace HC.Web.Services.Contracts;

public interface IApiCaller
{
    public Result<T> Get<T>
        (string url, Dictionary<string, string>? headers = null)
        where T : class;

    public Task<Result<T>> GetAsync<T>
        (string url, Dictionary<string, string>? headers = null, CancellationToken cancelationToken = default)
        where T : class;

    public Result<T> Post<T, TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json) 
        where T: class where TU: class;

    public Task<Result<T>> PostAsync<T, TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json, CancellationToken cancelationToken = default)
        where T : class where TU : class;

    public Result<T> Put<T, TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json) 
        where T : class where TU : class;

    public Task<Result<T>> PutAsync<T, TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json, CancellationToken cancelationToken = default) 
        where T : class where TU : class;

    public Result<T> Delete<T>
        (string url, Dictionary<string, string>? headers = null) 
        where T : class;

    public Task<Result<T>> DeleteAsync<T>
        (string url, Dictionary<string, string>? headers = null, CancellationToken cancelationToken = default)
        where T : class;
}

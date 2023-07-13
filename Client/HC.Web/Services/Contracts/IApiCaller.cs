using HC.Shared.Constants;
using HC.Shared.Models;

namespace HC.Web.Services.Contracts;

public interface IApiCaller
{
    public ApiResult<T> Get<T>
        (string url, Dictionary<string, string>? headers = null)
        where T : class;

    public Task<ApiResult<T>> GetAsync<T>
        (string url, Dictionary<string, string>? headers = null, CancellationToken cancelationToken = default)
        where T : class;

    public ApiResult<T> Post<T, TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json) 
        where T: class where TU: class;

    public Task<ApiResult<T>> PostAsync<T, TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json, CancellationToken cancelationToken = default)
        where T : class where TU : class;

    public ApiResult<T> Put<T, TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json) 
        where T : class where TU : class;

    public Task<ApiResult<T>> PutAsync<T, TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json, CancellationToken cancelationToken = default) 
        where T : class where TU : class;

    public ApiResult<T> Delete<T>
        (string url, Dictionary<string, string>? headers = null) 
        where T : class;

    public Task<ApiResult<T>> DeleteAsync<T>
        (string url, Dictionary<string, string>? headers = null, CancellationToken cancelationToken = default)
        where T : class;
}

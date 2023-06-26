using HC.Shared.Constants;
using HC.Web.Models;
using static HC.Shared.Constants.RoutingConstants;

namespace HC.Web.Services.Contracts;

public interface IApiCaller
{
    public ClientSideApiResult<T> Get<T>
        (string url, Dictionary<string, string>? headers = null)
        where T : class;

    public Task<ClientSideApiResult<T>> GetAsync<T>
        (string url, Dictionary<string, string>? headers = null, CancellationToken cancelationToken = default)
        where T : class;

    public ClientSideApiResult<T> Post<T, TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json) 
        where T: class where TU: class;

    public Task<ClientSideApiResult<T>> PostAsync<T, TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json, CancellationToken cancelationToken = default)
        where T : class where TU : class;

    public ClientSideApiResult<T> Put<T, TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json) 
        where T : class where TU : class;

    public Task<ClientSideApiResult<T>> PutAsync<T, TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json, CancellationToken cancelationToken = default) 
        where T : class where TU : class;

    public ClientSideApiResult<T> Delete<T>
        (string url, Dictionary<string, string>? headers = null) 
        where T : class;

    public Task<ClientSideApiResult<T>> DeleteAsync<T>
        (string url, Dictionary<string, string>? headers = null, CancellationToken cancelationToken = default)
        where T : class;
}

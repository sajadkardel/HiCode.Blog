using HC.Web.Models;

namespace HC.Web.Services.Contracts;

public interface IApiCaller
{
    public ClientSideApiResult<T> Get<T>
        (string url, Dictionary<string, string>? headers = null)
        where T : class;

    public Task<ClientSideApiResult<T>> GetAsync<T>
        (string url, Dictionary<string, string>? headers = null, CancellationToken cancelationToken = default)
        where T : class;

    public ClientSideApiResult Post<TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null)
        where TU : class;

    public ClientSideApiResult<T> Post<T, TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null) 
        where T: class where TU: class;

    public Task<ClientSideApiResult> PostAsync<TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null, CancellationToken cancelationToken = default)
        where TU : class;

    public Task<ClientSideApiResult<T>> PostAsync<T, TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null, CancellationToken cancelationToken = default)
        where T : class where TU : class;

    public ClientSideApiResult Put<TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null)
        where TU : class;

    public ClientSideApiResult<T> Put<T, TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null) 
        where T : class where TU : class;

    public Task<ClientSideApiResult> PutAsync<TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null, CancellationToken cancelationToken = default)
        where TU : class;

    public Task<ClientSideApiResult<T>> PutAsync<T, TU>
        (string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null, CancellationToken cancelationToken = default) 
        where T : class where TU : class;

    public ClientSideApiResult<T> Delete<T>
        (string url, Dictionary<string, string>? headers = null) 
        where T : class;

    public Task<ClientSideApiResult<T>> DeleteAsync<T>
        (string url, Dictionary<string, string>? headers = null, CancellationToken cancelationToken = default)
        where T : class;
}

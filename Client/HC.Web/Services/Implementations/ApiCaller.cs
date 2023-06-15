using HC.Shared.Markers;
using HC.Web.Models;
using HC.Web.Services.Contracts;
using System.Net.Http.Json;

namespace HC.Web.Services.Implementations;

public class ApiCaller : IApiCaller, ISingletonDependency
{
    private readonly HttpClient _httpClient;
    public ApiCaller(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public ClientSideApiResult<T> Get<T>(string url, Dictionary<string, string>? headers = null)
        where T : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        var result = _httpClient.GetFromJsonAsync<ClientSideApiResult<T>>(url).Result;
        return result;
    }

    public async Task<ClientSideApiResult<T>> GetAsync<T>(string url, Dictionary<string, string>? headers = null, CancellationToken cancelationToken = default)
        where T : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        var result = await _httpClient.GetFromJsonAsync<ClientSideApiResult<T>>(url);
        return result;
    }

    public ClientSideApiResult<T> Post<T, TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null)
        where T : class
        where TU : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        var result = _httpClient.PostAsJsonAsync<TU>(url, requestModel).Result;
        var response = result.Content.ReadFromJsonAsync<ClientSideApiResult<T>>().Result;
        return response;
    }

    public async Task<ClientSideApiResult<T>> PostAsync<T, TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null, CancellationToken cancelationToken = default)
        where T : class
        where TU : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        var result = await _httpClient.PostAsJsonAsync<TU>(url, requestModel);
        var response = await result.Content.ReadFromJsonAsync<ClientSideApiResult<T>>();
        return response;
    }

    public ClientSideApiResult<T> Put<T, TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null)
        where T : class
        where TU : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        var result = _httpClient.PutAsJsonAsync<TU>(url, requestModel).Result;
        var response = result.Content.ReadFromJsonAsync<ClientSideApiResult<T>>().Result;
        return response;
    }

    public async Task<ClientSideApiResult<T>> PutAsync<T, TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null, CancellationToken cancelationToken = default)
        where T : class
        where TU : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        var result = await _httpClient.PutAsJsonAsync<TU>(url, requestModel);
        var response = await result.Content.ReadFromJsonAsync<ClientSideApiResult<T>>();
        return response;
    }

    public ClientSideApiResult<T> Delete<T>(string url, Dictionary<string, string>? headers = null)
        where T : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        var result = _httpClient.DeleteFromJsonAsync<ClientSideApiResult<T>>(url).Result;
        return result;
    }

    public async Task<ClientSideApiResult<T>> DeleteAsync<T>(string url, Dictionary<string, string>? headers = null, CancellationToken cancelationToken = default)
        where T : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        var result = await _httpClient.DeleteFromJsonAsync<ClientSideApiResult<T>>(url);
        return result;
    }
}

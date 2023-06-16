using HC.Shared.Markers;
using HC.Web.Models;
using HC.Web.Services.Contracts;
using System.Net.Http.Json;
using System.Text.Json;

namespace HC.Web.Services.Implementations;

public class ApiCaller : IApiCaller, IScopedDependency
{
    private readonly HttpClient _httpClient;
    public ApiCaller(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public ClientSideApiResult<T> Get<T>(string url, Dictionary<string, string>? headers = null)
        where T : class
    {
        ClientSideApiResult<T> response = new();

        try
        {
            if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            var result = _httpClient.GetAsync(url).Result;
            var rawStringResponse = result.Content.ReadAsStringAsync().Result;
            response = JsonSerializer.Deserialize<ClientSideApiResult<T>>(rawStringResponse) ?? new();
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
            response.StatusCode = HC.Shared.Enums.ApiResultStatusCode.HTTPConnection;
        }

        return response;
    }

    public async Task<ClientSideApiResult<T>> GetAsync<T>(string url, Dictionary<string, string>? headers = null, CancellationToken cancelationToken = default)
        where T : class
    {
        ClientSideApiResult<T> response = new();

        try
        {
            if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            var result = await _httpClient.GetAsync(url);
            var rawStringResponse = await result.Content.ReadAsStringAsync();
            response = JsonSerializer.Deserialize<ClientSideApiResult<T>>(rawStringResponse) ?? new();
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
            response.StatusCode = HC.Shared.Enums.ApiResultStatusCode.HTTPConnection;
        }

        return response;
    }

    public ClientSideApiResult Post<TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null)
        where TU : class
    {
        ClientSideApiResult response = new();

        try
        {
            if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            var request = JsonContent.Create(requestModel);
            var result = _httpClient.PostAsync(url, request).Result;
            var rawStringResponse = result.Content.ReadAsStringAsync().Result;
            response = JsonSerializer.Deserialize<ClientSideApiResult>(rawStringResponse) ?? new();
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
            response.StatusCode = HC.Shared.Enums.ApiResultStatusCode.HTTPConnection;
        }

        return response;
    }

    public ClientSideApiResult<T> Post<T, TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null)
        where T : class
        where TU : class
    {
        ClientSideApiResult<T> response = new();

        try
        {
            if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            var request = JsonContent.Create(requestModel);
            var result = _httpClient.PostAsync(url, request).Result;
            var rawStringResponse = result.Content.ReadAsStringAsync().Result;
            response = JsonSerializer.Deserialize<ClientSideApiResult<T>>(rawStringResponse) ?? new();
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
            response.StatusCode = HC.Shared.Enums.ApiResultStatusCode.HTTPConnection;
        }

        return response;
    }

    public async Task<ClientSideApiResult> PostAsync<TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null, CancellationToken cancelationToken = default)
        where TU : class
    {
        ClientSideApiResult response = new();

        try
        {
            if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            var request = JsonContent.Create(requestModel);
            var result = await _httpClient.PostAsync(url, request);
            var rawStringResponse = await result.Content.ReadAsStringAsync();
            response = JsonSerializer.Deserialize<ClientSideApiResult>(rawStringResponse) ?? new();
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
            response.StatusCode = HC.Shared.Enums.ApiResultStatusCode.HTTPConnection;
        }

        return response;
    }

    public async Task<ClientSideApiResult<T>> PostAsync<T, TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null, CancellationToken cancelationToken = default)
        where T : class
        where TU : class
    {
        ClientSideApiResult<T> response = new();

        try
        {
            if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            var request = JsonContent.Create(requestModel);
            var result = await _httpClient.PostAsync(url, request);
            var rawStringResponse = await result.Content.ReadAsStringAsync();
            response = JsonSerializer.Deserialize<ClientSideApiResult<T>>(rawStringResponse) ?? new();
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
            response.StatusCode = HC.Shared.Enums.ApiResultStatusCode.HTTPConnection;
        }

        return response;
    }

    public ClientSideApiResult Put<TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null) 
        where TU : class
    {
        ClientSideApiResult response = new();

        try
        {
            if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            var request = JsonContent.Create(requestModel);
            var result = _httpClient.PutAsync(url, request).Result;
            var rawStringResponse = result.Content.ReadAsStringAsync().Result;
            response = JsonSerializer.Deserialize<ClientSideApiResult>(rawStringResponse) ?? new();
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
            response.StatusCode = HC.Shared.Enums.ApiResultStatusCode.HTTPConnection;
        }

        return response;
    }

    public ClientSideApiResult<T> Put<T, TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null)
        where T : class
        where TU : class
    {
        ClientSideApiResult<T> response = new();

        try
        {
            if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            var request = JsonContent.Create(requestModel);
            var result = _httpClient.PutAsync(url, request).Result;
            var rawStringResponse = result.Content.ReadAsStringAsync().Result;
            response = JsonSerializer.Deserialize<ClientSideApiResult<T>>(rawStringResponse) ?? new();
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
            response.StatusCode = HC.Shared.Enums.ApiResultStatusCode.HTTPConnection;
        }

        return response;
    }

    public async Task<ClientSideApiResult> PutAsync<TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null, CancellationToken cancelationToken = default) 
        where TU : class
    {
        ClientSideApiResult response = new();

        try
        {
            if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            var request = JsonContent.Create(requestModel);
            var result = await _httpClient.PutAsync(url, request);
            var rawStringResponse = await result.Content.ReadAsStringAsync();
            response = JsonSerializer.Deserialize<ClientSideApiResult>(rawStringResponse) ?? new();
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
            response.StatusCode = HC.Shared.Enums.ApiResultStatusCode.HTTPConnection;
        }

        return response;
    }

    public async Task<ClientSideApiResult<T>> PutAsync<T, TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string? contentType = null, CancellationToken cancelationToken = default)
        where T : class
        where TU : class
    {
        ClientSideApiResult<T> response = new();

        try
        {
            if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            var request = JsonContent.Create(requestModel);
            var result = await _httpClient.PutAsync(url, request);
            var rawStringResponse = await result.Content.ReadAsStringAsync();
            response = JsonSerializer.Deserialize<ClientSideApiResult<T>>(rawStringResponse) ?? new();
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
            response.StatusCode = HC.Shared.Enums.ApiResultStatusCode.HTTPConnection;
        }

        return response;
    }

    public ClientSideApiResult<T> Delete<T>(string url, Dictionary<string, string>? headers = null)
        where T : class
    {
        ClientSideApiResult<T> response = new();

        try
        {
            if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            var result = _httpClient.DeleteAsync(url).Result;
            var rawStringResponse = result.Content.ReadAsStringAsync().Result;
            response = JsonSerializer.Deserialize<ClientSideApiResult<T>>(rawStringResponse) ?? new();
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
            response.StatusCode = HC.Shared.Enums.ApiResultStatusCode.HTTPConnection;
        }

        return response;
    }

    public async Task<ClientSideApiResult<T>> DeleteAsync<T>(string url, Dictionary<string, string>? headers = null, CancellationToken cancelationToken = default)
        where T : class
    {
        ClientSideApiResult<T> response = new();

        try
        {
            if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            var result = await _httpClient.DeleteAsync(url);
            var rawStringResponse = await result.Content.ReadAsStringAsync();
            response = JsonSerializer.Deserialize<ClientSideApiResult<T>>(rawStringResponse) ?? new();
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
            response.StatusCode = HC.Shared.Enums.ApiResultStatusCode.HTTPConnection;
        }

        return response;
    }
}

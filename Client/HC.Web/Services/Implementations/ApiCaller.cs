using HC.Shared.Constants;
using HC.Shared.Markers;
using HC.Shared.Models;
using HC.Web.Services.Contracts;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HC.Web.Services.Implementations;

public class ApiCaller : IApiCaller, IScopedDependency
{
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions _serializerOption = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public ApiCaller(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public ApiResult<T> Get<T>(string url, Dictionary<string, string>? headers = null)
        where T : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

        var result = _httpClient.GetAsync(url).Result;
        var resultContent = result.Content.ReadAsStringAsync().Result;
        var response = JsonSerializer.Deserialize<ApiResult<T>>(resultContent, _serializerOption) ?? ApiResult<T>.Success();

        return response;
    }

    public async Task<ApiResult<T>> GetAsync<T>(string url, Dictionary<string, string>? headers = null, CancellationToken cancelationToken = default)
        where T : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

        var result = await _httpClient.GetAsync(url);
        var resultContent = await result.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<ApiResult<T>>(resultContent, _serializerOption) ?? ApiResult<T>.Success();

        return response;
    }

    public ApiResult<T> Post<T, TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json)
        where T : class
        where TU : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

        HttpContent httpContent = default!;

        if (contentType is HttpRequestContentTypeConstants.Json)
        {
            var requestString = JsonSerializer.Serialize(requestModel, _serializerOption);
            httpContent = new StringContent(requestString, Encoding.GetEncoding(encoding), contentType);
        }
        else if (contentType is HttpRequestContentTypeConstants.UrlEncode)
        {
            var requestUrlEncode = CreateUrlEncode(requestModel);
            httpContent = new FormUrlEncodedContent(requestUrlEncode);
        }

        var result = _httpClient.PostAsync(url, httpContent).Result;
        var resultContent = result.Content.ReadAsStringAsync().Result;
        var response = JsonSerializer.Deserialize<ApiResult<T>>(resultContent, _serializerOption) ?? ApiResult<T>.Success();

        return response;
    }

    public async Task<ApiResult<T>> PostAsync<T, TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json, CancellationToken cancelationToken = default)
        where T : class
        where TU : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

        HttpContent httpContent = default!;

        if (contentType is HttpRequestContentTypeConstants.Json)
        {
            var requestString = JsonSerializer.Serialize(requestModel, _serializerOption);
            httpContent = new StringContent(requestString, Encoding.GetEncoding(encoding), contentType);
        }
        else if (contentType is HttpRequestContentTypeConstants.UrlEncode)
        {
            var requestUrlEncode = CreateUrlEncode(requestModel);
            httpContent = new FormUrlEncodedContent(requestUrlEncode);
        }

        var result = await _httpClient.PostAsync(url, httpContent);
        var resultContent = await result.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<ApiResult<T>>(resultContent, _serializerOption) ?? ApiResult<T>.Success();

        return response;
    }

    public ApiResult<T> Put<T, TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json)
        where T : class
        where TU : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

        HttpContent httpContent = default!;

        if (contentType is HttpRequestContentTypeConstants.Json)
        {
            var requestString = JsonSerializer.Serialize(requestModel, _serializerOption);
            httpContent = new StringContent(requestString, Encoding.GetEncoding(encoding), contentType);
        }
        else if (contentType is HttpRequestContentTypeConstants.UrlEncode)
        {
            var requestUrlEncode = CreateUrlEncode(requestModel);
            httpContent = new FormUrlEncodedContent(requestUrlEncode);
        }

        var result = _httpClient.PutAsync(url, httpContent).Result;
        var resultContent = result.Content.ReadAsStringAsync().Result;
        var response = JsonSerializer.Deserialize<ApiResult<T>>(resultContent, _serializerOption) ?? ApiResult<T>.Success();

        return response;
    }

    public async Task<ApiResult<T>> PutAsync<T, TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json, CancellationToken cancelationToken = default)
        where T : class
        where TU : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

        HttpContent httpContent = default!;

        if (contentType is HttpRequestContentTypeConstants.Json)
        {
            var requestString = JsonSerializer.Serialize(requestModel, _serializerOption);
            httpContent = new StringContent(requestString, Encoding.GetEncoding(encoding), contentType);
        }
        else if (contentType is HttpRequestContentTypeConstants.UrlEncode)
        {
            var requestUrlEncode = CreateUrlEncode(requestModel);
            httpContent = new FormUrlEncodedContent(requestUrlEncode);
        }

        var result = await _httpClient.PutAsync(url, httpContent);
        var resultContent = await result.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<ApiResult<T>>(resultContent, _serializerOption) ?? ApiResult<T>.Success();

        return response;
    }

    public ApiResult<T> Delete<T>(string url, Dictionary<string, string>? headers = null)
        where T : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        var result = _httpClient.DeleteAsync(url).Result;
        var resultContent = result.Content.ReadAsStringAsync().Result;
        var response = JsonSerializer.Deserialize<ApiResult<T>>(resultContent, _serializerOption) ?? ApiResult<T>.Success();

        return response;
    }

    public async Task<ApiResult<T>> DeleteAsync<T>(string url, Dictionary<string, string>? headers = null, CancellationToken cancelationToken = default)
        where T : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        var result = await _httpClient.DeleteAsync(url);
        var resultContent = await result.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<ApiResult<T>>(resultContent, _serializerOption) ?? ApiResult<T>.Success();

        return response;
    }

    // Methods
    private IEnumerable<KeyValuePair<string, string>> CreateUrlEncode<T>(T requestModel)
    {
        List<KeyValuePair<string, string>> data = new();

        if (requestModel is null) return data;

        Type t = requestModel.GetType();
        PropertyInfo[] props = t.GetProperties();
        foreach (var prop in props)
        {
            string propName;

            var jsonPropertyAttribute = prop.GetCustomAttribute<JsonPropertyNameAttribute>();
            if (jsonPropertyAttribute is not null) propName = jsonPropertyAttribute.Name;
            else propName = prop.Name;
            data.Add(new KeyValuePair<string, string>(propName, prop.GetValue(requestModel)?.ToString() ?? ""));
        }

        return data;
    }
}

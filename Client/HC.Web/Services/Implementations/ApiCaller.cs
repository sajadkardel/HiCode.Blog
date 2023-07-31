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

    #region Get
    public Result<T> Get<T>(string url, Dictionary<string, string>? headers = null)
    where T : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

        var result = _httpClient.GetAsync(url).Result;
        var resultContent = result.Content.ReadAsStringAsync().Result;
        var response = JsonSerializer.Deserialize<Result<T>>(resultContent, _serializerOption);
        if (response is null) return Result.Failed<T>("");

        return response;
    }

    public async Task<Result<T>> GetAsync<T>(string url, Dictionary<string, string>? headers = null, CancellationToken cancelationToken = default)
        where T : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

        var result = await _httpClient.GetAsync(url);
        var resultContent = await result.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<Result<T>>(resultContent, _serializerOption);
        if (response is null) return Result.Failed<T>("");

        return response;
    }
    #endregion

    #region Post
    public Result<T> Post<T, TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json)
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
        var response = JsonSerializer.Deserialize<Result<T>>(resultContent, _serializerOption);
        if (response is null) return Result.Failed<T>("");

        return response;
    }

    public async Task<Result<T>> PostAsync<T, TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json, CancellationToken cancelationToken = default)
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
        var response = JsonSerializer.Deserialize<Result<T>>(resultContent, _serializerOption);
        if (response is null) return Result.Failed<T>("");

        return response;
    }

    public Result Post<TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = "application/json") where TU : class
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
        var response = JsonSerializer.Deserialize<Result>(resultContent, _serializerOption);
        if (response is null) return Result.Failed("");

        return response;
    }

    public async Task<Result> PostAsync<TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = "application/json", CancellationToken cancelationToken = default) where TU : class
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
        var response = JsonSerializer.Deserialize<Result>(resultContent, _serializerOption);
        if (response is null) return Result.Failed("");

        return response;
    }
    #endregion

    #region Put
    public Result<T> Put<T, TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json)
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
        var response = JsonSerializer.Deserialize<Result<T>>(resultContent, _serializerOption);
        if (response is null) return Result.Failed<T>("");

        return response;
    }

    public async Task<Result<T>> PutAsync<T, TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = HttpRequestContentTypeConstants.Json, CancellationToken cancelationToken = default)
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
        var response = JsonSerializer.Deserialize<Result<T>>(resultContent, _serializerOption);
        if (response is null) return Result.Failed<T>("");

        return response;
    }

    public Result Put<TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = "application/json") where TU : class
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
        var response = JsonSerializer.Deserialize<Result>(resultContent, _serializerOption);
        if (response is null) return Result.Failed("");

        return response;
    }

    public async Task<Result> PutAsync<TU>(string url, TU requestModel, int encoding = 65001, Dictionary<string, string>? headers = null, string contentType = "application/json", CancellationToken cancelationToken = default) where TU : class
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
        var response = JsonSerializer.Deserialize<Result>(resultContent, _serializerOption);
        if (response is null) return Result.Failed("");

        return response;
    }
    #endregion

    #region Delete
    public Result<T> Delete<T>(string url, Dictionary<string, string>? headers = null)
        where T : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        var result = _httpClient.DeleteAsync(url).Result;
        var resultContent = result.Content.ReadAsStringAsync().Result;
        var response = JsonSerializer.Deserialize<Result<T>>(resultContent, _serializerOption);
        if (response is null) return Result.Failed<T>("");

        return response;
    }

    public async Task<Result<T>> DeleteAsync<T>(string url, Dictionary<string, string>? headers = null, CancellationToken cancelationToken = default)
        where T : class
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        var result = await _httpClient.DeleteAsync(url);
        var resultContent = await result.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<Result<T>>(resultContent, _serializerOption);
        if (response is null) return Result.Failed<T>("");

        return response;
    }

    public Result Delete(string url, Dictionary<string, string>? headers = null)
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        var result = _httpClient.DeleteAsync(url).Result;
        var resultContent = result.Content.ReadAsStringAsync().Result;
        var response = JsonSerializer.Deserialize<Result>(resultContent, _serializerOption);
        if (response is null) return Result.Failed("");

        return response;
    }

    public async Task<Result> DeleteAsync(string url, Dictionary<string, string>? headers = null, CancellationToken cancelationToken = default)
    {
        if (headers is not null) foreach (var header in headers) _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        var result = await _httpClient.DeleteAsync(url);
        var resultContent = await result.Content.ReadAsStringAsync();
        var response = JsonSerializer.Deserialize<Result>(resultContent, _serializerOption);
        if (response is null) return Result.Failed("");

        return response;
    }
    #endregion

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

using HC.Web.Services.Contracts;
using System.Net.Http.Headers;

namespace HC.Web.Services.Implementations;

public class AppHttpClientHandler : HttpClientHandler
{
    private readonly IStorageService _storageService;

    public AppHttpClientHandler(IStorageService storageService)
    {
        _storageService = storageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        if (request.Headers.Authorization is null)
        {
            var access_token = await _storageService.GetFromCookieAsync("access_token", cancellationToken);
            if (access_token.IsSucceed && access_token.Data is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", access_token.Data);
            }
        }

        var response = await base.SendAsync(request, cancellationToken);

        return response;
    }
}

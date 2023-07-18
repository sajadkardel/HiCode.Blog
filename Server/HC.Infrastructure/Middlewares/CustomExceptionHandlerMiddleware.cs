using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using HC.Shared.Enums;
using System.Text.Json;
using HC.Shared.Constants;
using HC.Shared.Models;
using HC.Shared.Extensions;

namespace HC.Infrastructure.Middlewares;

public static class CustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;

    public CustomExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env)
    {
        _next = next;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        ResultStatusCode apiStatusCode = ResultStatusCode.InternalServerError;
        string message = ResultStatusCode.InternalServerError.ToDisplay();

        try
        {
            await _next(context);
        }
        catch (SecurityTokenExpiredException exception)
        {
            SetUnAuthorizeResponse(exception);
            await WriteToResponseAsync();
        }
        catch (UnauthorizedAccessException exception)
        {
            SetUnAuthorizeResponse(exception);
            await WriteToResponseAsync();
        }
        catch (Exception exception)
        {
            if (_env.IsDevelopment())
            {
                var dic = new Dictionary<string, string>
                {
                    ["Exception"] = exception.Message,
                    ["StackTrace"] = exception.StackTrace ?? ""
                };
                message = JsonSerializer.Serialize(dic);
            }
            await WriteToResponseAsync();
        }

        async Task WriteToResponseAsync()
        {
            if (context.Response.HasStarted) 
                Result.Failed("The response has already started, the http status code middleware will not be executed.");

            var result = Result.Failed(apiStatusCode, message);
            var json = JsonSerializer.Serialize(result);
            context.Response.ContentType = HttpRequestContentTypeConstants.Json;
            await context.Response.WriteAsync(json);
        }

        void SetUnAuthorizeResponse(Exception exception)
        {
            apiStatusCode = ResultStatusCode.Unauthorized;

            if (_env.IsDevelopment())
            {
                var dic = new Dictionary<string, string>
                {
                    ["Exception"] = exception.Message,
                    ["StackTrace"] = exception.StackTrace ?? ""
                };
                if (exception is SecurityTokenExpiredException tokenException)
                    dic.Add("Expires", tokenException.Expires.ToString());

                message = JsonSerializer.Serialize(dic);
            }
        }
    }
}

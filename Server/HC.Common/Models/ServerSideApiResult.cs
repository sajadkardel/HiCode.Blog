using HC.Shared.Dtos;
using HC.Shared.Enums;
using HC.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HC.Common.Models;

public class ServerSideApiResult : ApiResult
{
    public ServerSideApiResult(bool isSuccess, ApiResultStatusCode statusCode, string? message = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message ?? statusCode.ToDisplay();
    }

    #region Implicit Operators

    public static implicit operator ServerSideApiResult(OkResult result)
    {
        return new ServerSideApiResult(true, ApiResultStatusCode.Success);
    }

    public static implicit operator ServerSideApiResult(BadRequestResult result)
    {
        return new ServerSideApiResult(false, ApiResultStatusCode.BadRequest);
    }

    public static implicit operator ServerSideApiResult(BadRequestObjectResult result)
    {
        var message = result.Value?.ToString();
        if (result.Value is SerializableError errors)
        {
            var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
            message = string.Join(" | ", errorMessages);
        }
        return new ServerSideApiResult(false, ApiResultStatusCode.BadRequest, message);
    }

    public static implicit operator ServerSideApiResult(ContentResult result)
    {
        return new ServerSideApiResult(true, ApiResultStatusCode.Success, result.Content);
    }

    public static implicit operator ServerSideApiResult(NotFoundResult result)
    {
        return new ServerSideApiResult(false, ApiResultStatusCode.NotFound);
    }

    #endregion
}

public class ServerSideApiResult<TData> : ApiResult<TData> where TData : class
{
    public ServerSideApiResult(bool isSuccess, ApiResultStatusCode statusCode, TData data = default!, string? message = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message ?? statusCode.ToDisplay();
        Data = data;
    }

    #region Implicit Operators

    public static implicit operator ServerSideApiResult<TData>(TData data)
    {
        return new ServerSideApiResult<TData>(true, ApiResultStatusCode.Success, data);
    }

    public static implicit operator ServerSideApiResult<TData>(OkResult result)
    {
        return new ServerSideApiResult<TData>(true, ApiResultStatusCode.Success, default!);
    }

    public static implicit operator ServerSideApiResult<TData>(OkObjectResult result)
    {
        return new ServerSideApiResult<TData>(true, ApiResultStatusCode.Success, result.Value as TData ?? default!);
    }

    public static implicit operator ServerSideApiResult<TData>(BadRequestResult result)
    {
        return new ServerSideApiResult<TData>(false, ApiResultStatusCode.BadRequest, default!);
    }

    public static implicit operator ServerSideApiResult<TData>(BadRequestObjectResult result)
    {
        var message = result.Value?.ToString();
        if (result.Value is SerializableError errors)
        {
            var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
            message = string.Join(" | ", errorMessages);
        }
        return new ServerSideApiResult<TData>(false, ApiResultStatusCode.BadRequest, default!, message);
    }

    public static implicit operator ServerSideApiResult<TData>(ContentResult result)
    {
        return new ServerSideApiResult<TData>(true, ApiResultStatusCode.Success, default!, result.Content);
    }

    public static implicit operator ServerSideApiResult<TData>(NotFoundResult result)
    {
        return new ServerSideApiResult<TData>(false, ApiResultStatusCode.NotFound, default!);
    }

    public static implicit operator ServerSideApiResult<TData>(NotFoundObjectResult result)
    {
        return new ServerSideApiResult<TData>(false, ApiResultStatusCode.NotFound, result.Value as TData ?? default!);
    }

    #endregion
}
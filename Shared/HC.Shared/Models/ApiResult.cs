using HC.Shared.Enums;
using HC.Shared.Extensions;
using System.Diagnostics;

namespace HC.Shared.Models;

public class ApiResult
{
    public bool IsSucceed { get; set; }
    public ApiResultStatusCode StatusCode { get; set; }
    public string Message { get; set; }

    public ApiResult(bool isSucceed, ApiResultStatusCode statusCode = ApiResultStatusCode.Continue, string message = "")
    {
        IsSucceed = isSucceed;
        StatusCode = statusCode;
        Message = message;
    }

    #region Success
    [DebuggerStepThrough]
    public static ApiResult Success()
    {
        return new ApiResult(true, ApiResultStatusCode.OK, ApiResultStatusCode.OK.ToDisplay());
    }

    [DebuggerStepThrough]
    public static ApiResult<T> Success<T>(T data)
    {
        return new ApiResult<T>(data ,true, ApiResultStatusCode.OK, ApiResultStatusCode.OK.ToDisplay());
    }

    [DebuggerStepThrough]
    public static ApiResult Success(ApiResultStatusCode statusCode)
    {
        return new ApiResult(true, statusCode, statusCode.ToDisplay());
    }

    [DebuggerStepThrough]
    public static ApiResult<T> Success<T>(T data, ApiResultStatusCode statusCode)
    {
        return new ApiResult<T>(data, true, statusCode, statusCode.ToDisplay());
    }

    [DebuggerStepThrough]
    public static ApiResult Success(string message)
    {
        return new ApiResult(true, ApiResultStatusCode.OK, message);
    }

    [DebuggerStepThrough]
    public static ApiResult<T> Success<T>(T data, string message)
    {
        return new ApiResult<T>(data, true, ApiResultStatusCode.OK, message);
    }

    [DebuggerStepThrough]
    public static ApiResult Success(ApiResultStatusCode statusCode, string message)
    {
        return new ApiResult(true, statusCode, message);
    }

    [DebuggerStepThrough]
    public static ApiResult<T> Success<T>(T data, ApiResultStatusCode statusCode, string message)
    {
        return new ApiResult<T>(data, true, statusCode, message);
    }
    #endregion

    #region Failed
    [DebuggerStepThrough]
    public static ApiResult Failed()
    {
        return new ApiResult(false, ApiResultStatusCode.OK, ApiResultStatusCode.OK.ToDisplay());
    }

    [DebuggerStepThrough]
    public static ApiResult<T> Failed<T>()
    {
        return new ApiResult<T>(false, ApiResultStatusCode.OK, ApiResultStatusCode.OK.ToDisplay());
    }

    [DebuggerStepThrough]
    public static ApiResult Failed(ApiResultStatusCode statusCode)
    {
        return new ApiResult(false, statusCode, statusCode.ToDisplay());
    }

    [DebuggerStepThrough]
    public static ApiResult<T> Failed<T>(ApiResultStatusCode statusCode)
    {
        return new ApiResult<T>(false, statusCode, statusCode.ToDisplay());
    }

    [DebuggerStepThrough]
    public static ApiResult Failed(string message)
    {
        return new ApiResult(false, ApiResultStatusCode.OK, message);
    }

    [DebuggerStepThrough]
    public static ApiResult<T> Failed<T>(string message)
    {
        return new ApiResult<T>(false, ApiResultStatusCode.OK, message);
    }

    [DebuggerStepThrough]
    public static ApiResult Failed(ApiResultStatusCode statusCode, string message)
    {
        return new ApiResult(false, statusCode, message);
    }

    [DebuggerStepThrough]
    public static ApiResult<T> Failed<T>(ApiResultStatusCode statusCode, string message)
    {
        return new ApiResult<T>(false, statusCode, message);
    }
    #endregion
}


public class ApiResult<TData> : ApiResult
{
    public ApiResult(bool isSucceed, ApiResultStatusCode statusCode = ApiResultStatusCode.Continue, string message = "")
        : base(isSucceed, statusCode, message)
    {
    }

    public ApiResult(TData? data, bool isSucceed, ApiResultStatusCode statusCode, string message = "") 
        : base(isSucceed, statusCode, message)
    {
        Data = data;
    }

    public TData? Data { get; set; } = default;
}

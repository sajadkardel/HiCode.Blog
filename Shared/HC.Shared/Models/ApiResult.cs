using HC.Shared.Enums;
using HC.Shared.Extensions;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HC.Shared.Models;

public class ApiResult
{
    public bool IsSucceed { get; set; }
    public ApiResultStatusCode StatusCode { get; set; }
    public string Message { get; set; }

    public ApiResult(bool isSucceed, ApiResultStatusCode statusCode, string message = "")
    {
        IsSucceed = isSucceed;
        StatusCode = statusCode;
        Message = message;
    }

    #region Success
    [DebuggerStepThrough]
    public static ApiResult Success()
    {
        return new ApiResult(true, ApiResultStatusCode.Success, ApiResultStatusCode.Success.ToDisplay());
    }

    [DebuggerStepThrough]
    public static ApiResult Success(ApiResultStatusCode statusCode)
    {
        return new ApiResult(true, statusCode, statusCode.ToDisplay());
    }

    [DebuggerStepThrough]
    public static ApiResult Success(string message)
    {
        return new ApiResult(true, ApiResultStatusCode.Success, message);
    }

    [DebuggerStepThrough]
    public static ApiResult Success(ApiResultStatusCode statusCode, string message)
    {
        return new ApiResult(true, statusCode, message);
    }
    #endregion

    #region Failed
    [DebuggerStepThrough]
    public static ApiResult Failed()
    {
        return new ApiResult(false, ApiResultStatusCode.Success, ApiResultStatusCode.Success.ToDisplay());
    }

    [DebuggerStepThrough]
    public static ApiResult Failed(ApiResultStatusCode statusCode)
    {
        return new ApiResult(false, statusCode, statusCode.ToDisplay());
    }

    [DebuggerStepThrough]
    public static ApiResult Failed(string message)
    {
        return new ApiResult(false, ApiResultStatusCode.Success, message);
    }

    [DebuggerStepThrough]
    public static ApiResult Failed(ApiResultStatusCode statusCode, string message)
    {
        return new ApiResult(false, statusCode, message);
    }
    #endregion
}


public class ApiResult<TData> : ApiResult
{
    public ApiResult(bool isSucceed, ApiResultStatusCode statusCode, string message = "") 
        : base(isSucceed, statusCode, message)
    {
    }

    public TData Data { get; set; } = default!;

    #region Success
    [DebuggerStepThrough]
    public new static ApiResult<TData> Success()
    {
        return new ApiResult<TData>(true, ApiResultStatusCode.Success, ApiResultStatusCode.Success.ToDisplay());
    }

    [DebuggerStepThrough]
    public new static ApiResult<TData> Success(ApiResultStatusCode statusCode)
    {
        return new ApiResult<TData>(true, statusCode, statusCode.ToDisplay());
    }

    [DebuggerStepThrough]
    public new static ApiResult<TData> Success(string message)
    {
        return new ApiResult<TData>(true, ApiResultStatusCode.Success, message);
    }

    [DebuggerStepThrough]
    public new static ApiResult<TData> Success(ApiResultStatusCode statusCode, string message)
    {
        return new ApiResult<TData>(true, statusCode, message);
    }
    #endregion

    #region Failed
    [DebuggerStepThrough]
    public new static ApiResult<TData> Failed()
    {
        return new ApiResult<TData>(false, ApiResultStatusCode.Success, ApiResultStatusCode.Success.ToDisplay());
    }

    [DebuggerStepThrough]
    public new static ApiResult<TData> Failed(ApiResultStatusCode statusCode)
    {
        return new ApiResult<TData>(false, statusCode, statusCode.ToDisplay());
    }

    [DebuggerStepThrough]
    public new static ApiResult<TData> Failed(string message)
    {
        return new ApiResult<TData>(false, ApiResultStatusCode.Success, message);
    }

    [DebuggerStepThrough]
    public new static ApiResult<TData> Failed(ApiResultStatusCode statusCode, string message)
    {
        return new ApiResult<TData>(false, statusCode, message);
    }
    #endregion
}

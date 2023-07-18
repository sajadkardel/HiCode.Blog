using HC.Shared.Enums;
using HC.Shared.Extensions;
using System.Diagnostics;

namespace HC.Shared.Models;

public class Result
{
    public bool IsSucceed { get; set; }
    public ApiResultStatusCode StatusCode { get; set; }
    public string Message { get; set; }

    public Result(bool isSucceed, ApiResultStatusCode statusCode = ApiResultStatusCode.Continue, string message = "")
    {
        IsSucceed = isSucceed;
        StatusCode = statusCode;
        Message = message;
    }

    #region Success
    [DebuggerStepThrough]
    public static Result Success()
    {
        return new Result(true, ApiResultStatusCode.OK, ApiResultStatusCode.OK.ToDisplay());
    }

    [DebuggerStepThrough]
    public static Result<T> Success<T>(T data)
    {
        return new Result<T>(data ,true, ApiResultStatusCode.OK, ApiResultStatusCode.OK.ToDisplay());
    }

    [DebuggerStepThrough]
    public static Result Success(ApiResultStatusCode statusCode)
    {
        return new Result(true, statusCode, statusCode.ToDisplay());
    }

    [DebuggerStepThrough]
    public static Result<T> Success<T>(T data, ApiResultStatusCode statusCode)
    {
        return new Result<T>(data, true, statusCode, statusCode.ToDisplay());
    }

    [DebuggerStepThrough]
    public static Result Success(string message)
    {
        return new Result(true, ApiResultStatusCode.OK, message);
    }

    [DebuggerStepThrough]
    public static Result<T> Success<T>(T data, string message)
    {
        return new Result<T>(data, true, ApiResultStatusCode.OK, message);
    }

    [DebuggerStepThrough]
    public static Result Success(ApiResultStatusCode statusCode, string message)
    {
        return new Result(true, statusCode, message);
    }

    [DebuggerStepThrough]
    public static Result<T> Success<T>(T data, ApiResultStatusCode statusCode, string message)
    {
        return new Result<T>(data, true, statusCode, message);
    }
    #endregion

    #region Failed
    [DebuggerStepThrough]
    public static Result Failed()
    {
        return new Result(false, ApiResultStatusCode.OK, ApiResultStatusCode.OK.ToDisplay());
    }

    [DebuggerStepThrough]
    public static Result<T> Failed<T>()
    {
        return new Result<T>(false, ApiResultStatusCode.OK, ApiResultStatusCode.OK.ToDisplay());
    }

    [DebuggerStepThrough]
    public static Result Failed(ApiResultStatusCode statusCode)
    {
        return new Result(false, statusCode, statusCode.ToDisplay());
    }

    [DebuggerStepThrough]
    public static Result<T> Failed<T>(ApiResultStatusCode statusCode)
    {
        return new Result<T>(false, statusCode, statusCode.ToDisplay());
    }

    [DebuggerStepThrough]
    public static Result Failed(string message)
    {
        return new Result(false, ApiResultStatusCode.OK, message);
    }

    [DebuggerStepThrough]
    public static Result<T> Failed<T>(string message)
    {
        return new Result<T>(false, ApiResultStatusCode.OK, message);
    }

    [DebuggerStepThrough]
    public static Result Failed(ApiResultStatusCode statusCode, string message)
    {
        return new Result(false, statusCode, message);
    }

    [DebuggerStepThrough]
    public static Result<T> Failed<T>(ApiResultStatusCode statusCode, string message)
    {
        return new Result<T>(false, statusCode, message);
    }
    #endregion
}


public class Result<TData> : Result
{
    public Result(bool isSucceed, ApiResultStatusCode statusCode = ApiResultStatusCode.Continue, string message = "")
        : base(isSucceed, statusCode, message)
    {
    }

    public Result(TData? data, bool isSucceed, ApiResultStatusCode statusCode, string message = "") 
        : base(isSucceed, statusCode, message)
    {
        Data = data;
    }

    public TData? Data { get; set; } = default;
}

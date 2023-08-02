using HC.Shared.Enums;
using HC.Shared.Extensions;
using System.Diagnostics;

namespace HC.Shared.Models;

public class Result
{
    public bool Succeeded { get; set; }
    public ResultStatusCode StatusCode { get; set; }
    public string Message { get; set; }

    public Result(bool succeeded, ResultStatusCode statusCode = ResultStatusCode.Continue, string message = "")
    {
        Succeeded = succeeded;
        StatusCode = statusCode;
        Message = message;
    }

    #region Success
    [DebuggerStepThrough]
    public static Result Success()
    {
        return new Result(true, ResultStatusCode.OK, ResultStatusCode.OK.ToDisplay());
    }

    [DebuggerStepThrough]
    public static Result<T> Success<T>(T data)
    {
        return new Result<T>(data ,true, ResultStatusCode.OK, ResultStatusCode.OK.ToDisplay());
    }

    [DebuggerStepThrough]
    public static Result Success(ResultStatusCode statusCode)
    {
        return new Result(true, statusCode, statusCode.ToDisplay());
    }

    [DebuggerStepThrough]
    public static Result<T> Success<T>(T data, ResultStatusCode statusCode)
    {
        return new Result<T>(data, true, statusCode, statusCode.ToDisplay());
    }

    [DebuggerStepThrough]
    public static Result Success(string message)
    {
        return new Result(true, ResultStatusCode.OK, message);
    }

    [DebuggerStepThrough]
    public static Result<T> Success<T>(T data, string message)
    {
        return new Result<T>(data, true, ResultStatusCode.OK, message);
    }

    [DebuggerStepThrough]
    public static Result Success(ResultStatusCode statusCode, string message)
    {
        return new Result(true, statusCode, message);
    }

    [DebuggerStepThrough]
    public static Result<T> Success<T>(T data, ResultStatusCode statusCode, string message)
    {
        return new Result<T>(data, true, statusCode, message);
    }
    #endregion

    #region Failed
    [DebuggerStepThrough]
    public static Result Failed()
    {
        return new Result(false, ResultStatusCode.OK, ResultStatusCode.OK.ToDisplay());
    }

    [DebuggerStepThrough]
    public static Result<T> Failed<T>()
    {
        return new Result<T>(false, ResultStatusCode.OK, ResultStatusCode.OK.ToDisplay());
    }

    [DebuggerStepThrough]
    public static Result Failed(ResultStatusCode statusCode)
    {
        return new Result(false, statusCode, statusCode.ToDisplay());
    }

    [DebuggerStepThrough]
    public static Result<T> Failed<T>(ResultStatusCode statusCode)
    {
        return new Result<T>(false, statusCode, statusCode.ToDisplay());
    }

    [DebuggerStepThrough]
    public static Result Failed(string message)
    {
        return new Result(false, ResultStatusCode.OK, message);
    }

    [DebuggerStepThrough]
    public static Result<T> Failed<T>(string message)
    {
        return new Result<T>(false, ResultStatusCode.OK, message);
    }

    [DebuggerStepThrough]
    public static Result Failed(ResultStatusCode statusCode, string message)
    {
        return new Result(false, statusCode, message);
    }

    [DebuggerStepThrough]
    public static Result<T> Failed<T>(ResultStatusCode statusCode, string message)
    {
        return new Result<T>(false, statusCode, message);
    }
    #endregion
}


public class Result<TData> : Result
{
    public Result(bool succeeded, ResultStatusCode statusCode = ResultStatusCode.Continue, string message = "")
        : base(succeeded, statusCode, message)
    {
    }

    public Result(TData data, bool succeeded, ResultStatusCode statusCode, string message = "") 
        : base(succeeded, statusCode, message)
    {
        Data = data;
    }

    public TData Data { get; set; } = default!;
}

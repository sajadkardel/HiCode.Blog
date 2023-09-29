using HC.Shared.Enums;
using HC.Shared.Extensions;

namespace HC.Shared.Models;

public class Result
{
    public bool Succeeded { get; set; }
    public ResultStatusCode StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;

    #region Success
    public static Result Success()
    {
        return new Result
        {
            Succeeded = true,
            StatusCode = ResultStatusCode.OK,
            Message = ResultStatusCode.OK.ToDisplay()
        };
    }

    public static Result<T> Success<T>(T data)
    {
        return new Result<T>
        {
            Data = data ,
            Succeeded = true,
            StatusCode = ResultStatusCode.OK,
            Message = ResultStatusCode.OK.ToDisplay()
        };
    }

    public static Result Success(ResultStatusCode statusCode)
    {
        return new Result
        {
            Succeeded = true,
            StatusCode = statusCode,
            Message = statusCode.ToDisplay()
        };
    }

    public static Result<T> Success<T>(T data, ResultStatusCode statusCode)
    {
        return new Result<T>
        {
            Data = data,
            Succeeded = true,
            StatusCode = statusCode,
            Message = statusCode.ToDisplay()
        };
    }

    public static Result Success(string message)
    {
        return new Result
        {
            Succeeded = true,
            StatusCode = ResultStatusCode.OK,
            Message = message
        };
    }

    public static Result<T> Success<T>(T data, string message)
    {
        return new Result<T>
        {
            Data = data,
            Succeeded = true,
            StatusCode = ResultStatusCode.OK,
            Message = message
        };
    }

    public static Result Success(ResultStatusCode statusCode, string message)
    {
        return new Result
        {
            Succeeded = true,
            StatusCode = statusCode,
            Message = message
        };
    }

    public static Result<T> Success<T>(T data, ResultStatusCode statusCode, string message)
    {
        return new Result<T>
        {
            Data = data,
            Succeeded = true,
            StatusCode = statusCode,
            Message = message
        };
    }
    #endregion

    #region Failed
    public static Result Failed()
    {
        return new Result
        {
            Succeeded = false,
            StatusCode = ResultStatusCode.InternalServerError,
            Message = ResultStatusCode.InternalServerError.ToDisplay()
        };
    }

    public static Result<T> Failed<T>()
    {
        return new Result<T>
        {
            Succeeded = false,
            StatusCode = ResultStatusCode.InternalServerError,
            Message = ResultStatusCode.InternalServerError.ToDisplay()
        };
    }

    public static Result Failed(ResultStatusCode statusCode)
    {
        return new Result
        {
            Succeeded = false,
            StatusCode = statusCode,
            Message = statusCode.ToDisplay()
        };
    }

    public static Result<T> Failed<T>(ResultStatusCode statusCode)
    {
        return new Result<T>
        {
            Succeeded = false,
            StatusCode = statusCode,
            Message = statusCode.ToDisplay()
        };
    }

    public static Result Failed(string message)
    {
        return new Result
        {
            Succeeded = false,
            StatusCode = ResultStatusCode.InternalServerError,
            Message = message
        };
    }

    public static Result<T> Failed<T>(string message)
    {
        return new Result<T>
        {
            Succeeded = false,
            StatusCode = ResultStatusCode.InternalServerError,
            Message = message
        };
    }

    public static Result Failed(ResultStatusCode statusCode, string message)
    {
        return new Result
        {
            Succeeded = false,
            StatusCode = statusCode,
            Message = message
        };
    }

    public static Result<T> Failed<T>(ResultStatusCode statusCode, string message)
    {
        return new Result<T>
        {
            Succeeded = false,
            StatusCode = statusCode,
            Message = message
        };
    }
    #endregion
}

public class Result<TData> : Result
{
    public TData Data { get; set; } = default!;
}

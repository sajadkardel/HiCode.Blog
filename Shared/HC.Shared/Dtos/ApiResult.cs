using HC.Shared.Enums;
using HC.Shared.Extensions;

namespace HC.Shared.Dtos;

public class ApiResult
{
    public bool IsSuccess { get; set; }
    public ApiResultStatusCode StatusCode { get; set; }
    public string Message { get; set; }

    public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, string? message = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message ?? statusCode.ToDisplay();
    }
}

public class ApiResult<TData> : ApiResult
    where TData : class
{
    public TData? Data { get; set; }

    public ApiResult(bool isSuccess, ApiResultStatusCode statusCode, TData? data = null, string? message = null)
        : base(isSuccess, statusCode, message)
    {
        Data = data;
    }
}

using HC.Shared.Enums;

namespace HC.Shared.Dtos;

public class ApiResult
{
    public bool IsSuccess { get; set; }
    public ApiResultStatusCode StatusCode { get; set; }
    public string? Message { get; set; }
}

public class ApiResult<TData> : ApiResult
    where TData : class
{
    public TData? Data { get; set; }
}

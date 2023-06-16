using HC.Shared.Enums;

namespace HC.Shared.Dtos;

public abstract class ApiResult
{
    public bool IsSuccess { get; set; }
    public ApiResultStatusCode StatusCode { get; set; }
    public string? Message { get; set; }
}

public abstract class ApiResult<TData> : ApiResult
    where TData : class
{
    public TData? Data { get; set; } = default!;
}

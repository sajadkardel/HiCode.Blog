using HC.Shared.Dtos;
using HC.Shared.Enums;

namespace HC.Web.Models;

public class ClientSideApiResult : ApiResult
{
    public ClientSideApiResult(bool isSuccess, ApiResultStatusCode statusCode, string message = null) : base(isSuccess, statusCode, message)
    {
    }
}

public class ClientSideApiResult<TData> : ApiResult<TData> where TData : class
{
    public ClientSideApiResult(bool isSuccess, ApiResultStatusCode statusCode, TData data, string message = null) : base(isSuccess, statusCode, data, message)
    {
    }
}
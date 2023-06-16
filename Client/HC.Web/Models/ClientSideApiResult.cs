using HC.Shared.Dtos;
using HC.Shared.Enums;

namespace HC.Web.Models;

public class ClientSideApiResult : ApiResult
{
}

public class ClientSideApiResult<TData> : ApiResult<TData> where TData : class
{
}
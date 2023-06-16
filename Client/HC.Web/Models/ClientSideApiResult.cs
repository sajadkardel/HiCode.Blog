using HC.Shared.Dtos;

namespace HC.Web.Models;

public class ClientSideApiResult : ApiResult
{
}

public class ClientSideApiResult<TData> : ApiResult<TData> where TData : class
{
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HC.Entity.Identity;
using HC.Shared.Dtos.Identity;
using HC.Common.Models;

namespace HC.Api.Controllers.v1;

[ApiVersion("1")]
public class RoleController : BaseController
{

    [HttpGet]
    public virtual async Task<ApiResult<List<RoleResponseDto>>> Get(CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpGet("{id:int}")]
    public virtual async Task<ApiResult<RoleResponseDto>> Get(int id, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpPost]
    public virtual async Task<ApiResult<RoleResponseDto>> Create(RoleRequestDto dto, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpPut]
    public virtual async Task<ApiResult<RoleResponseDto>> Update(int id, RoleRequestDto dto, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public virtual async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        return Ok();
    }
}
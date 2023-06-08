using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HC.Entity.Identity;
using HC.Shared.Dtos.Identity;
using HC.Common.Models;
using HC.Service.Contracts;

namespace HC.Api.Controllers.v1
{
    [ApiVersion("1")]
    public class RoleController : BaseController
    {
        private readonly IRepository<Role> _repository;

        public RoleController(IRepository<Role> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public virtual async Task<ApiResult<List<RoleResponseDto>>> Get(CancellationToken cancellationToken)
        {
            List<RoleResponseDto> dtos = new();

            var roles = await _repository.TableNoTracking.ToListAsync(cancellationToken);
            roles.ForEach(role => dtos.Add(new()
            {
                Name = role.Name,
            }));

            return dtos;
        }

        [HttpGet("{id:int}")]
        public virtual async Task<ApiResult<RoleResponseDto>> Get(int id, CancellationToken cancellationToken)
        {
            var role = await _repository.TableNoTracking.SingleOrDefaultAsync(roleSelectDto => roleSelectDto.Id == id, cancellationToken);

            if (role is null) return NotFound();

            return new RoleResponseDto
            {
                Name = role.Name
            };
        }

        [HttpPost]
        public virtual async Task<ApiResult<RoleResponseDto>> Create(RoleRequestDto dto, CancellationToken cancellationToken)
        {
            Role model = new()
            {
                Name = dto.Name
            };

            await _repository.Entities.AddAsync(model, cancellationToken);

            RoleResponseDto selectDto = new()
            {
                Name = model.Name
            };

            return selectDto;
        }

        [HttpPut]
        public virtual async Task<ApiResult<RoleResponseDto>> Update(int id, RoleRequestDto dto, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);

            model = new()
            {
                Name= dto.Name
            };

            await _repository.UpdateAsync(model, cancellationToken);

            RoleResponseDto selectDto = new()
            {
                Name = model.Name
            };

            return selectDto;
        }

        [HttpDelete("{id:int}")]
        public virtual async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            await _repository.DeleteAsync(model, cancellationToken);
            return Ok();
        }
    }
}
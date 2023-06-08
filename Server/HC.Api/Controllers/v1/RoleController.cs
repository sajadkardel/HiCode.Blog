using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HC.Infrastructure.Api;
using HC.DataAccess.Contracts;
using HC.Entity.Identity;
using HC.Shared.Dtos.Identity;

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
        public virtual async Task<ApiResult<List<RoleSelectDto>>> Get(CancellationToken cancellationToken)
        {
            List<RoleSelectDto> dtos = new();

            var roles = await _repository.TableNoTracking.ToListAsync(cancellationToken);
            roles.ForEach(role => dtos.Add(new()
            {
                Name = role.Name,
            }));

            return dtos;
        }

        [HttpGet("{id:int}")]
        public virtual async Task<ApiResult<RoleSelectDto>> Get(int id, CancellationToken cancellationToken)
        {
            var role = await _repository.TableNoTracking.SingleOrDefaultAsync(roleSelectDto => roleSelectDto.Id == id, cancellationToken);

            if (role is null) return NotFound();

            return new RoleSelectDto
            {
                Name = role.Name
            };
        }

        [HttpPost]
        public virtual async Task<ApiResult<RoleSelectDto>> Create(RoleDto dto, CancellationToken cancellationToken)
        {
            Role model = new()
            {
                Name = dto.Name
            };

            await _repository.Entities.AddAsync(model, cancellationToken);

            RoleSelectDto selectDto = new()
            {
                Name = model.Name
            };

            return selectDto;
        }

        [HttpPut]
        public virtual async Task<ApiResult<RoleSelectDto>> Update(int id, RoleDto dto, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);

            model = new()
            {
                Name= dto.Name
            };

            await _repository.UpdateAsync(model, cancellationToken);

            RoleSelectDto selectDto = new()
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
using FluentValidation;
using HC.Common.Markers;
using HC.Entity.Identity;
using HC.Infrastructure.Api;

namespace HC.Api.Dto.Identity;

public class RoleDto : BaseDto<RoleDto, Role, int>
{
    public string Name { get; set; }
}

public class RoleDtoValidator : AbstractValidator<RoleDto>, IDtoValidator
{
    public RoleDtoValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("نام مقام را وارد نمایید");
    }
}

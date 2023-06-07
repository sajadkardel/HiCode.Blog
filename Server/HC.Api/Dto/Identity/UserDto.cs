using FluentValidation;
using HC.Common.Enums;
using HC.Common.Markers;
using HC.Entity.Identity;
using HC.Infrastructure.Api;

namespace HC.Api.Dto.Identity;

public class UserDto : BaseDto<UserDto, User>
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string FullName { get; set; }

    public int Age { get; set; }

    public GenderType Gender { get; set; }
}

public class UserDtoValidator : AbstractValidator<UserDto>, IDtoValidator
{
    public UserDtoValidator()
    {

        RuleFor(p => p.UserName).NotEmpty().WithMessage("نام کاربری را وارد نمایید");
        RuleFor(p => p.Email).NotEmpty().WithMessage("ایمیل را وارد نمایید");
        RuleFor(p => p.Password).NotEmpty().WithMessage("کلمه عبور را وارد نمایید");
    }
}

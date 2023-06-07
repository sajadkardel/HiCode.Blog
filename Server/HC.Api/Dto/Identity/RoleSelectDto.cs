using HC.Entity.Identity;
using HC.Infrastructure.Api;

namespace HC.Api.Dto.Identity;

public class RoleSelectDto : BaseDto<RoleSelectDto, Role, int>
{
    public string Name { get; set; }
}

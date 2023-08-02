using HC.Data.Entities.Identity;
using HC.Data.Services.Contracts;
using HC.Shared.Markers;
using Microsoft.AspNetCore.Identity;

namespace HC.Data.Services.Implementations;

public class UserDataInitializer : IDataInitializer, IScopedDependency
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    public UserDataInitializer(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void InitializeData()
    {
        if (_roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult() is false)
        {
            _roleManager.CreateAsync(new Role { Name = "Admin", Description = "Admin role" }).GetAwaiter().GetResult();
        }

        if (_userManager.Users.Any(p => p.UserName == "Admin") is false)
        {
            var user = new User { UserName = "Admin" };
            _userManager.CreateAsync(user, password: "@Admin@1122").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
        }
    }
}

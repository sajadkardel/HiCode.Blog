using HC.DataAccess.Entities.User;
using HC.DataAccess.Repositories.Contracts;
using HC.Shared.Markers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HC.DataAccess.Repositories.Implementations;

public class UserDataInitializer : IDataInitializer, IScopedDependency
{
    private readonly UserManager<User> userManager;
    private readonly RoleManager<Role> roleManager;

    public UserDataInitializer(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public void InitializeData()
    {
        if (!roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
        {
            roleManager.CreateAsync(new Role { Name = "Admin", Description = "Admin role" }).GetAwaiter().GetResult();
        }
        if (!userManager.Users.AsNoTracking().Any(p => p.UserName == "Admin"))
        {
            var user = new User
            {
                Age = 0,
                FullName = "مدیر",
                Gender = 0,
                UserName = "admin",
                Email = "admin@Gmail.com"
            };
            userManager.CreateAsync(user, "123456").GetAwaiter().GetResult();
            userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
        }
    }
}
using IdentityUserManagment.Domain.Models;
using IdentityUserManagment.Infrastructure.Contexts;
using IdentityUserManagment.Shared.Consts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityUserManagment.Shared.Extensions;

namespace IdentityUserManagment.Infrastructure.Seeds;

public interface IDatabaseSeeder
{
    Task SeedAsync();
}
public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    public DatabaseSeeder(ApplicationDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task SeedAsync()
    {
        await SeedRoles();
        await SeedUsers();
        await SeedSuperAdminClaims();
    }

    private async Task SeedRoles()
    {
        string[] roles = new[] { Roles.SuperAdmin, Roles.Admin, Roles.User }; 
        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                var result = await _roleManager.CreateAsync(new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = role,
                    NormalizedName = role.ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                });

                if (!result.Succeeded)
                    throw new Exception($"Error Seeding new role with name {role}, Errors: {string.Join("\n", result.Errors)}");
            }
        }
    }

    private async Task SeedUsers()
    {
        var superAdminUsers = new List<User>
        {
            new User { Id = Guid.NewGuid().ToString(), Email = "superadmin@gmail.com", UserName = "superadmin", EmailConfirmed = true, PasswordHash = "Super@123" },
        };

        var adminUsers = new List<User>
        {
            new User { Id = Guid.NewGuid().ToString(), Email = "admin@gmail.com", UserName = "admin", EmailConfirmed = true, PasswordHash = "Admin@123" },
        };

        var users = new List<User> 
        {
            new User { Id = Guid.NewGuid().ToString(), Email = "test@gmail.com", UserName = "test", EmailConfirmed = true, PasswordHash = "Test@123" },
        };

        if(! await _context.Users.AnyAsync())
        {
            foreach (var superAdmin in superAdminUsers)
            {
                await _userManager.CreateAsync(superAdmin, superAdmin.PasswordHash);
                await _userManager.AddToRoleAsync(superAdmin, Roles.SuperAdmin);
            }

            foreach (var admin in adminUsers)
            {
                await _userManager.CreateAsync(admin, admin.PasswordHash);
                await _userManager.AddToRoleAsync(admin, Roles.Admin);
            }

            foreach (var user in users)
            {
                await _userManager.CreateAsync(user, user.PasswordHash);
                await _userManager.AddToRoleAsync(user, Roles.User);
            }
        }
    }

    private async Task SeedSuperAdminClaims()
    {
        var adminRole = await _roleManager.FindByNameAsync(Roles.SuperAdmin);
        await _roleManager.AddPermissionClaimsToRole(adminRole, "Items");
    }
}
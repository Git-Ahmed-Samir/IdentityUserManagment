using IdentityUserManagment.Domain.Models;
using IdentityUserManagment.Infrastructure.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    private async Task SeedRoles()
    {
        string[] roles = new[] { "SuperAdmin", "User" }; 

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
        List<User> users = new List<User> 
        {
            new User { Id = Guid.NewGuid().ToString(), Email = "admin@admin.com", UserName = "admin", EmailConfirmed = true, PasswordHash = "P@ssw0rd" },
        };

        if(! await _context.Users.AnyAsync())
        {
            foreach (var user in users)
            {
                await _userManager.CreateAsync(user, user.PasswordHash);
            }
        }
    }
}
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
        if (!await _context.Modules.AnyAsync())
            await SeedModules();
        if (!await _context.ModuleSections.AnyAsync())
            await SeedModuleSections();
        if (!await _context.Pages.AnyAsync())
            await SeedPages();
        if (!await _context.PageActions.AnyAsync())
            await SeedPageActions();

        await SeedRoles();
        await SeedUsers();
        await SeedSuperAdminClaims();

    }

    #region Seed Modules
    private async Task SeedModules()
    {
        var data = new List<Module>
        {
            new Module { Id = 1, NameEn = "Manage Users", NameAr = "إدارة المستخدمين"},
            new Module { Id = 2, NameEn = "Manage Stock", NameAr = "إدارة المخزن"}
        };
        await _context.Modules.AddRangeAsync(data);
        await _context.SaveChangesAsync();
    }
    private async Task SeedModuleSections()
    {
        var data = new List<ModuleSection>
        {
            new ModuleSection { Id = 1, ModuleId = 1, NameEn = "Manage Users", NameAr = "إدارة المستخدمين"},
            new ModuleSection { Id = 2, ModuleId = 2, NameEn = "Manage Stock", NameAr = "إدارة المخزن"}
        };
        await _context.ModuleSections.AddRangeAsync(data);
        await _context.SaveChangesAsync();
    }
    private async Task SeedPages()
    {
        var data = new List<Page>
        {
            new Page { Id = 1, ModuleSectionId = 1, NameEn = "User", NameAr = "User"},
            new Page { Id = 2, ModuleSectionId = 1, NameEn = "Role", NameAr = "Role"},

            new Page { Id = 3, ModuleSectionId = 2, NameEn = "Category", NameAr = "Categories"},
            new Page { Id = 4, ModuleSectionId = 2, NameEn = "Item", NameAr = "Items"},
        };
        await _context.Pages.AddRangeAsync(data);
        await _context.SaveChangesAsync();
    }
    private async Task SeedPageActions()
    {
        var data = new List<PageAction>
        {
            new PageAction { Id = 1, PageId = 1, NameEn = PageActions.Create, NameAr = PageActions.Create },
            new PageAction { Id = 2, PageId = 1, NameEn = PageActions.Read, NameAr = PageActions.Read },
            new PageAction { Id = 3, PageId = 1, NameEn = PageActions.Update, NameAr = PageActions.Update },
            new PageAction { Id = 4, PageId = 1, NameEn = PageActions.Delete, NameAr = PageActions.Delete },

            new PageAction { Id = 5, PageId = 2, NameEn = PageActions.Create, NameAr = PageActions.Create },
            new PageAction { Id = 6, PageId = 2, NameEn = PageActions.Read, NameAr = PageActions.Read },
            new PageAction { Id = 7, PageId = 2, NameEn = PageActions.Update, NameAr = PageActions.Update },
            new PageAction { Id = 8, PageId = 2, NameEn = PageActions.Delete, NameAr = PageActions.Delete },

            new PageAction { Id = 9, PageId = 3, NameEn = PageActions.Create, NameAr = PageActions.Create },
            new PageAction { Id = 10, PageId = 3, NameEn = PageActions.Read, NameAr = PageActions.Read },
            new PageAction { Id = 11, PageId = 3, NameEn = PageActions.Update, NameAr = PageActions.Update },
            new PageAction { Id = 12, PageId = 3, NameEn = PageActions.Delete, NameAr = PageActions.Delete },

            new PageAction { Id = 13, PageId = 4, NameEn = PageActions.Create, NameAr = PageActions.Create },
            new PageAction { Id = 14, PageId = 4, NameEn = PageActions.Read, NameAr = PageActions.Read },
            new PageAction { Id = 15, PageId = 4, NameEn = PageActions.Update, NameAr = PageActions.Update },
            new PageAction { Id = 16, PageId = 4, NameEn = PageActions.Delete, NameAr = PageActions.Delete },

        };
        await _context.PageActions.AddRangeAsync(data);
        await _context.SaveChangesAsync();
    }
    #endregion

    #region Seed Users
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
        var allPermissions = _context.Pages.Include(x => x.PageActions).SelectMany(x => x.PageActions.Select(pa => Claims.Permission + '.' + x.NameEn + '.' + pa.NameEn)).ToList();
        await _roleManager.AddPermissionClaimsToRole(adminRole, allPermissions);
    }
    #endregion

}
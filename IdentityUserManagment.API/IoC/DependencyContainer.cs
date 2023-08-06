using IdentityUserManagment.Infrastructure.UnitOfWork;
using IdentityUserManagment.Application.Services;
using IdentityUserManagment.Domain.IUnitOfWork;
using IdentityUserManagment.Infrastructure.Seeds;
using IdentityUserManagment.Shared.Authorization.Permissions;

namespace IdentityUserManagment.API.IoC;

public class DependencyContainer
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IDatabaseSeeder, DatabaseSeeder>();

        //Registering services
        services.AddScoped<IAccountService,  AccountService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IPermissionService, PermissionService>();

        //Repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}

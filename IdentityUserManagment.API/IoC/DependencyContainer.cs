using IdentityUserManagment.Infrastructure.UnitOfWork;
using IdentityUserManagment.Application.Services;
using IdentityUserManagment.Domain.IUnitOfWork;
using IdentityUserManagment.Infrastructure.Seeds;

namespace IdentityUserManagment.API.IoC;

public class DependencyContainer
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IDatabaseSeeder, DatabaseSeeder>();

        //Registering services
        services.AddScoped<IAccountService,  AccountService>();

        //Repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}

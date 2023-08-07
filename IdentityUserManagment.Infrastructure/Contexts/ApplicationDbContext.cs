using IdentityUserManagment.Domain.ModelConfiguration;
using IdentityUserManagment.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PageAction = IdentityUserManagment.Domain.Models.PageAction;

namespace IdentityUserManagment.Infrastructure.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<User,Role, string,UserClaim,UserRole,IdentityUserLogin<string>,
                                RoleClaim,IdentityUserToken<string>>
    {
        #region database entities
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<ModuleSection> ModuleSections { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<PageAction> PageActions { get; set; }
        #endregion
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleClaimConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new UserClaimConfiguration());
            builder.Entity<IdentityUserLogin<string>>()
                .ToTable("UserLogins", "security");
            builder.Entity<IdentityUserToken<string>>()
                .ToTable("UserTokens", "security");


        }

    }
}
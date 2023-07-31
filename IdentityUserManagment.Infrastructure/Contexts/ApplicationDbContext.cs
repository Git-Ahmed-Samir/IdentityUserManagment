using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityUserManagment.Domain.ModelConfiguration;
using IdentityUserManagment.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IdentityUserManagment.Infrastructure.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<User,Role, string,UserClaim,UserRole,IdentityUserLogin<string>,
                                IdentityRoleClaim<string>,IdentityUserToken<string>>
    {

        //private readonly IConfiguration _configuration;
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

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new UserClaimConfiguration());
            builder.Entity<IdentityRoleClaim<string>>()
                .ToTable("IdentityRoleClaims", "security");
            builder.Entity<IdentityUserLogin<string>>()
                .ToTable("IdentityUserLogins", "security");
            builder.Entity<IdentityUserToken<string>>()
                .ToTable("IdentityUserTokens", "security");
        }

    }
}
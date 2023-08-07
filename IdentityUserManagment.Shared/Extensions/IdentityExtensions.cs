using IdentityUserManagment.Domain.Models;
using IdentityUserManagment.Shared.Authorization.Permissions;
using IdentityUserManagment.Shared.Consts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Shared.Extensions;
public static class IdentityExtensions
{
    public static async Task AddPermissionClaimsToRole(this RoleManager<Role> roleManager, Role role, List<string> permissions)
    {
        var allClaims = await roleManager.GetClaimsAsync(role);

        foreach (var permission in permissions)
        {
            if(! allClaims.Any(x => x.Type == Claims.Permission && x.Value == permission))
                await roleManager.AddClaimAsync(role, new Claim(Claims.Permission, permission));
        }
    }
}

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
    public static async Task AddPermissionClaimsToRole(this RoleManager<Role> roleManager, Role role, string module)
    {
        var allClaims = await roleManager.GetClaimsAsync(role);
        var allPermissions = Permissions.GeneratePermissionsForModule(module);
        foreach (var permission in allPermissions)
        {
            if(! allClaims.Any(x => x.Type == Claims.Permission && x.Type == permission))
                await roleManager.AddClaimAsync(role, claim: new Claim(Claims.Permission, permission));
        }
    }
}

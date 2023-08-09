using IdentityUserManagment.Shared.Consts;
using IdentityUserManagment.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Shared.Authorization.Permissions;
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IPermissionService _permissionService;
    public PermissionAuthorizationHandler(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    //Verify permission from database [Server Side]
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.User == null)
        {
            context.Fail();
            return;
        }

        var canAccess = await _permissionService.CanAccess(context.User.GetUserId(), requirement.Permission);

        if (canAccess)
            context.Succeed(requirement);
    }
}

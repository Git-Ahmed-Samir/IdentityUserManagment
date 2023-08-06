using IdentityUserManagment.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Shared.Authorization.Permissions;
public static class Permissions
{
    public static List<string> GeneratePermissionsForModule(string module)
    {
        return new List<string>
        {
            $"Permissions.{module}.Create",
            $"Permissions.{module}.View",
            $"Permissions.{module}.Edit",
            $"Permissions.{module}.Delete",
        };
    }

    public static List<string> GenerateAllPermissions()
    {
        var allPermissions = new List<string>();
        var modules = Enum.GetValues(typeof(ModulesEnum));

        foreach (var module in modules)
            allPermissions.AddRange(GeneratePermissionsForModule(module.ToString()));

        return allPermissions;
    }
}

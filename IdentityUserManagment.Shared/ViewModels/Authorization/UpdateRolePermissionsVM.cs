using System.ComponentModel.DataAnnotations;

namespace IdentityUserManagment.Shared.ViewModels;
public class UpdateRolePermissionsVM
{
    [Required]
    public string RoleId { get; set; }

    public List<PermissionVM> Permissions { get; set; }
}

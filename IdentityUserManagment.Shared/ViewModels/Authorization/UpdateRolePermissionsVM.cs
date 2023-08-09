using System.ComponentModel.DataAnnotations;

namespace IdentityUserManagment.Shared.ViewModels;
public class UpdateRolePermissionsVM
{
    [Required]
    public string roleName { get; set; }

    public List<string> Permissions { get; set; }
}

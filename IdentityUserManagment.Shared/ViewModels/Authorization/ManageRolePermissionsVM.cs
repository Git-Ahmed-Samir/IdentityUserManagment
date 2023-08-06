using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Shared.ViewModels;
public class ManageRolePermissionsVM
{
    [Required]
    public string RoleId { get; set; }

    public List<string> Permissions { get; set;}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Shared.ViewModels;
public class PermissionVM
{
    public string Module { get; set; }
    public string ModuleSection { get; set; }
    public string Page { get; set; }
    public string PageAction { get; set; }

    public bool IsSelected { get; set; } = false;
}

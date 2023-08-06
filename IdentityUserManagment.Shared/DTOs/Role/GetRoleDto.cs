using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Shared.DTOs;
public class GetRoleDto
{
    public string Id { get; set; }
    public string Name { get; set; }

    public List<string> RoleClaims { get; set; }
}

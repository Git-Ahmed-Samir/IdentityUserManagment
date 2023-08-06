using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Domain.Models;
public class RoleClaim : IdentityRoleClaim<string>
{
    public virtual Role Role { get; set; }
}

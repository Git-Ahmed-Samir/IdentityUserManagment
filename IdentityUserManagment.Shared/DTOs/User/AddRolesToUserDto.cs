using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Shared.DTOs;
public class AddRolesToUserDto
{
    [Required]
    public string UserId { get; set; }
    [Required]
    public List<string> Roles { get; set; }
}

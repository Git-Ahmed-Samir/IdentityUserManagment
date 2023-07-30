using Microsoft.AspNetCore.Identity;

namespace IdentityUserManagment.Domain.Models
{
    public class Role : IdentityRole
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
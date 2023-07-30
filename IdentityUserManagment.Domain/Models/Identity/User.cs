using IdentityUserManagment.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityUserManagment.Domain.Models
{
    public class User : IdentityUser
    {
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate {get;set;}
        public short LoggingAttempts {get;set;}

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
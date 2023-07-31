using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Shared.ViewModels;
public class AuthModel
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }

    public string Token { get; set; }
    public List<string> Roles { get; set; }

    public DateTime ExpiresOn { get; set; }
}

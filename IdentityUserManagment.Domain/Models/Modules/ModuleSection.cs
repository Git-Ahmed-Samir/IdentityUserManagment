using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Domain.Models;
[Table("ModuleSections", Schema = "modules")]
public class ModuleSection : ModuleBaseEntity
{
    public int ModuleId { get; set; }

    public virtual Module Module { get; set; }

    public virtual ICollection<Page> Pages { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Domain.Models;
[Table("Modules", Schema = "modules")]
public class Module : ModuleBaseEntity
{
    public virtual ICollection<ModuleSection> ModuleSections { get; set; }
}

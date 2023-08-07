using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Domain.Models;
[Table("Pages", Schema = "modules")]
public class Page : ModuleBaseEntity
{
    public int ModuleSectionId { get; set; }
    
    public virtual ModuleSection ModuleSection { get; set; }
    public virtual ICollection<PageAction> PageActions { get; set; }
}

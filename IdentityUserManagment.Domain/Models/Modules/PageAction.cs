using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityUserManagment.Domain.Models;
[Table("PageActions", Schema = "modules")]
public class PageAction : ModuleBaseEntity
{
    public int PageId { get; set; }

    public virtual Page Page { get; set; }
}

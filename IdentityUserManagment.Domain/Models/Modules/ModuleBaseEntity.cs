using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Domain.Models;
public class ModuleBaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string NameEn { get; set; }
    public string NameAr { get; set; }

    public bool IsActive { get; set; } = true;

}

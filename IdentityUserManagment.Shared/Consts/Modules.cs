using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Shared.Consts;
public static class Modules
{
    public const string Users = "Users";
    public const string Products = "Products";


    public static List<string> GetAllModules()
    {
        Type type = MethodBase.GetCurrentMethod().DeclaringType;
        return type.GetFields().Select(x => x.GetValue(null).ToString()).ToList();
    }
}

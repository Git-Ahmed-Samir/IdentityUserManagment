using IdentityUserManagment.Shared.DTOs;
using IdentityUserManagment.Shared.Helpers;
using IdentityUserManagment.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Application.Services;
public interface IRoleService
{
    Task<ResponseModel<List<GetRoleDto>>> GetAll();
    Task<ResponseModel<GetRoleDto>> Add(string roleName);
    Task<ResponseModel<bool>> ManagePermissions(ManageRolePermissionsVM model);
}

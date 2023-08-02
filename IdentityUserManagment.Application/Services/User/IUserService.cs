using IdentityUserManagment.Shared.DTOs;
using IdentityUserManagment.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Application.Services;
public interface IUserService
{
    Task<ResponseModel<GetUserDto>> GetAllUsers();
}

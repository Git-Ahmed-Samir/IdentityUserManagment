using IdentityUserManagment.Shared.DTOs;
using IdentityUserManagment.Shared.Helpers;
using IdentityUserManagment.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityUserManagment.Application.Services
{
    public interface IAccountService
    {
        Task<ResponseModel<AuthModel>> RegisterAsync(RegisterDto model);
        Task<ResponseModel<AuthModel>> LoginAsync(LoginDto model);
    }
}
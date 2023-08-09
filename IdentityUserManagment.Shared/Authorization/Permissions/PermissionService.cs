using IdentityUserManagment.Domain.IRepositories;
using IdentityUserManagment.Domain.IUnitOfWork;
using IdentityUserManagment.Domain.Models;
using IdentityUserManagment.Shared.Consts;
using IdentityUserManagment.Shared.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IdentityUserManagment.Shared.Authorization.Permissions;
public interface IPermissionService
{
    Task<bool> CanAccess(string userId, string permission);

}
public class PermissionService : IPermissionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<User> _userRepo;
    private readonly IRepository<RoleClaim> _roleClaimRepo;

    public PermissionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userRepo = _unitOfWork.Repository<User>();
        _roleClaimRepo = _unitOfWork.Repository<RoleClaim>();
    }

    public async Task<bool> CanAccess(string userId, string permission)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(permission))
            return false;

        var userRoleIds = await _userRepo.Query(x => x.Id == userId)
                .SelectMany(x => x.UserRoles).Select(x => x.RoleId).ToListAsync();

        return await _roleClaimRepo.IsExistAsync(x => userRoleIds.Any(rId => rId == x.RoleId)
                && x.ClaimType == Claims.Permission && x.ClaimValue == permission);
    }
}

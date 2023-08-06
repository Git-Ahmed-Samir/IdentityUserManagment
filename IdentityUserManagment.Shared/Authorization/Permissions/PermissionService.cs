using IdentityUserManagment.Domain.IRepositories;
using IdentityUserManagment.Domain.IUnitOfWork;
using IdentityUserManagment.Domain.Models;
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
    Task<HashSet<string>> GetUserPermissions(string userId);

}
public class PermissionService : IPermissionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<User> _userRepository;

    public PermissionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _userRepository = _unitOfWork.Repository<User>();
    }

    public async Task<HashSet<string>> GetUserPermissions(string userId)
    {
        var roles = await _userRepository.Query(x => x.Id == userId)
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .ThenInclude(x => x.RoleClaims)
                .SelectMany(x => x.UserRoles)
                .Select(x => x.Role)
                .SelectMany(x => x.RoleClaims)
                .Select(x => x.ClaimValue).ToListAsync();

        return roles.ToHashSet<string>();
    }
}

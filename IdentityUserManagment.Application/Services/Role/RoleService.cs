using AutoMapper;
using AutoMapper.QueryableExtensions;
using IdentityUserManagment.Domain.IRepositories;
using IdentityUserManagment.Domain.IUnitOfWork;
using IdentityUserManagment.Domain.Models;
using IdentityUserManagment.Shared.Consts;
using IdentityUserManagment.Shared.DTOs;
using IdentityUserManagment.Shared.Extensions;
using IdentityUserManagment.Shared.Helpers;
using IdentityUserManagment.Shared.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace IdentityUserManagment.Application.Services;
public class RoleService : IRoleService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Role> _roleRepository;
    private readonly RoleManager<Role> _roleManager;
    private readonly IRepository<PageAction> _pageActionRepository;
    public RoleService(IMapper mapper, IUnitOfWork unitOfWork, RoleManager<Role> roleManager)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _roleRepository = _unitOfWork.Repository<Role>();
        _roleManager = roleManager;
        _pageActionRepository = _unitOfWork.Repository<PageAction>();
    }

    public async Task<ResponseModel<GetRoleDto>> Add(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            return Response<GetRoleDto>.Failed("Invalid data", (int)HttpStatusCode.BadRequest);

        var isExist = await _roleManager.RoleExistsAsync(roleName);
        if (isExist)
            return Response<GetRoleDto>.Failed("Role already exists", (int)HttpStatusCode.BadRequest);

        var role = new Role()
        {
            Id = Guid.NewGuid().ToString(),
            Name = roleName,
            NormalizedName = roleName.ToUpper(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
        };
        var result = await _roleManager.CreateAsync(role);

        if (!result.Succeeded)
            return Response<GetRoleDto>.Failed("Error adding new Role", (int)HttpStatusCode.InternalServerError);

        return Response<GetRoleDto>.Success(_mapper.Map<GetRoleDto>(role));
    }

    public async Task<ResponseModel<List<GetRoleDto>>> GetAll()
    {
        var result = await _roleRepository.Query()
            .ProjectTo<GetRoleDto>(_mapper.ConfigurationProvider).ToListAsync();

        return Response<List<GetRoleDto>>.Success(result);
    }

    public async Task<ResponseModel<List<PermissionVM>>> GetPermissions(string roleId)
    {
        if (string.IsNullOrWhiteSpace(roleId))
            return Response<List<PermissionVM>>.Failed("Invalid roleId", (int)HttpStatusCode.BadRequest);
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role is null)
            return Response<List<PermissionVM>>.Failed("Role not found", (int)HttpStatusCode.NotFound);
        var permissions = await _pageActionRepository.Query().Include(x => x.Page)
            .ThenInclude(x => x.ModuleSection).ThenInclude(x => x.Module)
            .Select(x => new PermissionVM
            {
                Module = x.Page.ModuleSection.Module.NameEn,
                ModuleSection = x.Page.ModuleSection.NameEn,
                Page = x.Page.NameEn,
                PageAction = x.NameEn
            }).ToListAsync();

        var rolePermissions = await _roleManager.GetClaimsAsync(role);
        foreach (var permission in permissions)
        {
            permission.IsSelected = rolePermissions.Any(x => x.Type == Claims.Permission
                    && x.Value == $"{Claims.Permission}.{permission.Page}.{permission.PageAction}");
        }

        return Response<List<PermissionVM>>.Success(permissions);
    }

    public async Task<ResponseModel<bool>> UpdatePermissions(UpdateRolePermissionsVM model)
    {
        if (model == null)
            return Response<bool>.Failed("Invalid data", (int)HttpStatusCode.BadRequest);

        var role = await _roleManager.FindByIdAsync(model.RoleId);
        if (role is null)
            return Response<bool>.Failed("Role Not Found", (int)HttpStatusCode.NotFound);

        var oldClaims = await _roleManager.GetClaimsAsync(role);
        //Remove old claims
        foreach (var claim in oldClaims)
            await _roleManager.RemoveClaimAsync(role, claim);

        var newPermissions = model.Permissions.Where(x => x.IsSelected)
            .Select(x => $"{Claims.Permission}.{x.Page}.{x.PageAction}").ToList();
        await _roleManager.AddPermissionClaimsToRole(role, newPermissions);

        return Response<bool>.Success(true);
    }
}

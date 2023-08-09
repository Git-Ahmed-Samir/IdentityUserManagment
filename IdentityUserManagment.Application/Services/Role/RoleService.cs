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
    private readonly IRepository<Role> _roleRepo;
    private readonly RoleManager<Role> _roleManager;
    private readonly IRepository<Module> _moduleRepo;
    public RoleService(IMapper mapper, IUnitOfWork unitOfWork, RoleManager<Role> roleManager)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _roleRepo = _unitOfWork.Repository<Role>();
        _roleManager = roleManager;
        _moduleRepo = _unitOfWork.Repository<Module>();
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
        var result = await _roleRepo.Query()
            .ProjectTo<GetRoleDto>(_mapper.ConfigurationProvider).ToListAsync();

        return Response<List<GetRoleDto>>.Success(result);
    }

    public async Task<ResponseModel<List<ModuleDto>>> GetPermissions(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
            return Response<List<ModuleDto>>.Failed("Invalid roleName", (int)HttpStatusCode.BadRequest);
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role is null)
            return Response<List<ModuleDto>>.Failed("Role not found", (int)HttpStatusCode.NotFound);
        var permissions = await _moduleRepo.Query()
            .ProjectTo<ModuleDto>(_mapper.ConfigurationProvider).ToListAsync();

        var rolePermissions = await _roleManager.GetClaimsAsync(role);
        foreach (var page in permissions.SelectMany(x => x.ModuleSections).SelectMany(x => x.Pages))
        {
            page.PageActions.ForEach(pa => {
                pa.IsSelected = rolePermissions.Any(x => x.Type == Claims.Permission
                    && x.Value == $"{Claims.Permission}.{page.Name}.{pa.Name}");
            });
        }

        return Response<List<ModuleDto>>.Success(permissions);
    }

    public async Task<ResponseModel<bool>> UpdatePermissions(UpdateRolePermissionsVM model)
    {
        if (model == null)
            return Response<bool>.Failed("Invalid data", (int)HttpStatusCode.BadRequest);

        var role = await _roleManager.FindByNameAsync(model.roleName);
        if (role is null)
            return Response<bool>.Failed("Role Not Found", (int)HttpStatusCode.NotFound);

        var oldClaims = await _roleManager.GetClaimsAsync(role);
        //Remove old claims
        foreach (var claim in oldClaims)
            await _roleManager.RemoveClaimAsync(role, claim);

        await _roleManager.AddPermissionClaimsToRole(role, model.Permissions);

        return Response<bool>.Success(true);
    }
}

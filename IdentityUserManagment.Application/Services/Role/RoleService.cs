using AutoMapper;
using AutoMapper.QueryableExtensions;
using IdentityUserManagment.Domain.IRepositories;
using IdentityUserManagment.Domain.IUnitOfWork;
using IdentityUserManagment.Domain.Models;
using IdentityUserManagment.Shared.DTOs;
using IdentityUserManagment.Shared.Enums;
using IdentityUserManagment.Shared.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IdentityUserManagment.Shared.Extensions;
using IdentityUserManagment.Shared.ViewModels;
using System.Security.Claims;
using IdentityUserManagment.Shared.Consts;

namespace IdentityUserManagment.Application.Services;
public class RoleService : IRoleService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Role> _roleRepository;
    private readonly RoleManager<Role> _roleManager;
    public RoleService(IMapper mapper, IUnitOfWork unitOfWork, RoleManager<Role> roleManager)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _roleRepository = _unitOfWork.Repository<Role>();
        _roleManager = roleManager;
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

    public async Task<ResponseModel<bool>> ManagePermissions(ManageRolePermissionsVM model)
    {
        if (model == null)
            return Response<bool>.Failed("Invalid data", (int)HttpStatusCode.BadRequest);

        var role = await _roleManager.FindByIdAsync(model.RoleId);
        if (role is null)
            return Response<bool>.Failed("Role Not Found", (int)HttpStatusCode.NotFound);

        var roleClaims = await _roleManager.GetClaimsAsync(role);
        var claimsToRemove = roleClaims.Where(x => model.Permissions.Any(p => p != x.Value));

        var currentClaims = roleClaims.Select(x => x.Value);
        var claimsToAdd = model.Permissions.Except(currentClaims);

        //Remove claims that not exists in model permissions
        foreach (var claim in claimsToRemove)
            await _roleManager.RemoveClaimAsync(role, claim);

        foreach (var claim in claimsToAdd)
            await _roleManager.AddClaimAsync(role, new Claim(Claims.Permission, claim));

        return Response<bool>.Success(true);
    }
}

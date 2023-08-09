using AutoMapper;
using AutoMapper.QueryableExtensions;
using IdentityUserManagment.Domain.IRepositories;
using IdentityUserManagment.Domain.IUnitOfWork;
using IdentityUserManagment.Domain.Models;
using IdentityUserManagment.Shared.DTOs;
using IdentityUserManagment.Shared.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace IdentityUserManagment.Application.Services;
public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<User> _userRepo;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userRepo = _unitOfWork.Repository<User>();
        _userManager = userManager;
    }

    public async Task<ResponseModel<bool>> AddRolesToUser(AddRolesToUserDto model)
    {
        if (model == null)
            return Response<bool>.Failed(false, "Invalid data", (int)HttpStatusCode.BadRequest);
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
            return Response<bool>.Failed(false, "User not found", (int)HttpStatusCode.NotFound);
        //get current roles for user
        var userRoles = await _userManager.GetRolesAsync(user);

        var rolesToRemove = userRoles.Except(model.Roles);
        var rolesToAdd = model.Roles.Except(userRoles);

        var result = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
        result = await _userManager.AddToRolesAsync(user, rolesToAdd);

        if (!result.Succeeded)
            return Response<bool>.Failed(false, "Error Updating roles", (int)HttpStatusCode.BadRequest);

        return Response<bool>.Success(true);
    }

    public async Task<ResponseModel<List<GetUserDto>>> GetAllUsers()
    {
        var result = await _userRepo.Query()
            .ProjectTo<GetUserDto>(_mapper.ConfigurationProvider).ToListAsync();

        return Response<List<GetUserDto>>.Success(result);
    }

}

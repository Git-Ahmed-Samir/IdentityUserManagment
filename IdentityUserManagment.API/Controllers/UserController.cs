using IdentityUserManagment.Application.Services;
using IdentityUserManagment.Shared.Consts;
using IdentityUserManagment.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityUserManagment.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet("GetAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _userService.GetAllUsers();
        if (!result.IsSuccess)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("AddRolesToUser")]
    public async Task<IActionResult> AddRolesToUser(AddRolesToUserDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await _userService.AddRolesToUser(model);
        if (!result.IsSuccess)
            return BadRequest(result);
        return Ok(result);
    }
}

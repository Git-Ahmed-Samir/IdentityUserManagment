using IdentityUserManagment.Application.Services;
using IdentityUserManagment.Shared.Consts;
using IdentityUserManagment.Shared.DTOs;
using IdentityUserManagment.Shared.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityUserManagment.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }
    [Authorize(policy: $"{Claims.Permission}.{Pages.Role}.{PageActions.Read}")]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _roleService.GetAll();
        if (!result.IsSuccess)
            return BadRequest(result);
        return Ok(result);
    }
    [Authorize(policy: $"{Claims.Permission}.{Pages.Role}.{PageActions.Create}")]
    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] RoleDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await _roleService.Add(model.Name);
        if (!result.IsSuccess)
            return BadRequest(result);
        return Ok(result);
    }

    [Authorize(policy: $"{Claims.Permission}.{Pages.Role}.{PageActions.Read}")]
    [HttpGet("GetPermissions")]
    public async Task<IActionResult> GetPermissions(string roleId)
    {
        var result = await _roleService.GetPermissions(roleId);
        if (!result.IsSuccess)
            return BadRequest(result);
        return Ok(result);
    }

    [Authorize(policy: $"{Claims.Permission}.{Pages.Role}.{PageActions.Update}")]
    [HttpPost("UpdatePermissions")]
    public async Task<IActionResult> UpdatePermissions(UpdateRolePermissionsVM model)
    {
        var result = await _roleService.UpdatePermissions(model);
        if (!result.IsSuccess)
            return BadRequest(result);
        return Ok(result);
    }
}

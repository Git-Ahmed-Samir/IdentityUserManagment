using IdentityUserManagment.Application.Services;
using IdentityUserManagment.Shared.DTOs;
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

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _roleService.GetAll();
        if (!result.IsSuccess)
            return BadRequest(result);
        return Ok(result);
    }

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
}

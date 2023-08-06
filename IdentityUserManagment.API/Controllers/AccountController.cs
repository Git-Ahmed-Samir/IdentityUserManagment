using IdentityUserManagment.Application.Services;
using IdentityUserManagment.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityUserManagment.API.Controllers;
[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await _accountService.RegisterAsync(model);
        if (!result.IsSuccess)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await _accountService.LoginAsync(model);
        if (!result.IsSuccess)
            return BadRequest(result);
        return Ok(result);
    }
}

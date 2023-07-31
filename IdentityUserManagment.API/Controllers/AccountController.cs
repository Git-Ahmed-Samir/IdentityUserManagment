using IdentityUserManagment.Application.Services;
using IdentityUserManagment.Shared.DTOs.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityUserManagment.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        var result = await _accountService.RegisterAsync(model);
        if (!result.IsSuccess)
            return BadRequest(result);
        return Ok(result);
    }
}

using IdentityUserManagment.Shared.Consts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityUserManagment.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    public UserController()
    {
        
    }
    [HttpGet]
    public async Task<IActionResult> GetModules()
    {
        return Ok(Modules.GetAllModules());
    }
}

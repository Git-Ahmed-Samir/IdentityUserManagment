using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityUserManagment.API.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    [HttpGet("Get")]
    [Authorize(Policy = "Permissions.Items.View")]
    public async Task<IActionResult> Get()
    {
        return Ok("kjsglk");
    }
}

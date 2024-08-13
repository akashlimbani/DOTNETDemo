using DOTNETDemo.Models.Request;
using DOTNETDemo.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DOTNETDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("GetUser")]
    public async Task<ActionResult> GetUserAsync([Required] int id)
    {
        var user = await _userService.GetUserAsyncById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost("PostUser")]
    public async Task<ActionResult> PostUserDataAsync(UserCardRequest request)
    {
        var result = await _userService.PostUserDataAsync(request);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpDelete("DeleteUserCard")]
    public async Task<ActionResult> DeleteUserAsync([Required] int id)
    {
        var success = await _userService.DeleteUserAsync(id);
        if (!success)
        {
            return NotFound();
        }

        return Ok();
    }
}

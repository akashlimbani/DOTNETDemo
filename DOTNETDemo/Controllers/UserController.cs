using DOTNETDemo.Models.Request;
using DOTNETDemo.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace DOTNETDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    public async Task<ActionResult> GetUserAsync(int id)
    {
        var user = await _userService.GetUserAsyncById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult> PostUserDataAsync(UserCardRequest request)
    {
        var result = await _userService.PostUserDataAsync(request);
        if (result)
        {
            return Ok();
        }
        return BadRequest();
    }
}

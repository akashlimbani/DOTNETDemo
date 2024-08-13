using DOTNETDemo.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Stanza.AzureFunctions.Services.UserService;

namespace DOTNETDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    public async Task<ActionResult> GetUserAsync(int id)
    {
        return Ok(await _userService.GetUserAsyncById(id));
    }

    [HttpPost]
    public async Task<ActionResult> PostUserDataAsync(UserCardRequest request)
    {
        return Ok(await _userService.PostUserDataAsync(request));
    }
}

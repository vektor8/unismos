using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using unismos.Common.Extensions;
using unismos.Common.ViewModels;
using unismos.Interfaces.IUser;

namespace unismos.API.Controllers;
[Authorize]
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [AllowAnonymous]
    [HttpPost]
    [Route("authenticate")]
    public async Task<IActionResult> Authenticate(LoginViewModel model)
    {
        Log.Information("user with username {username} is trying to login", model.Username);
        var user = (await _userService.Authenticate(model.ToDto())).ToViewModel();
        return user is NullLoggedInUserViewModel ? BadRequest("Incorrect credentials") : Ok(user);
    }
}
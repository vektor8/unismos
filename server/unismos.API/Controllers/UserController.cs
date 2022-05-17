using Microsoft.AspNetCore.Mvc;
using unismos.Common.Extensions;
using unismos.Common.ViewModels;
using unismos.Interfaces.IUser;

namespace unismos.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("authenticate")]
    public async Task<IActionResult> Authenticate(LoginViewModel model)
    {
        var user = (await _userService.Authenticate(model.ToDto())).ToViewModel();
        return user is NullLoggedInUserViewModel ? BadRequest("Incorrect credentials") : Ok(user);
    }
}
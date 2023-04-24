using Microsoft.AspNetCore.Mvc;
using QuickLoanAPI.Application;
using QuickLoanAPI.Application.Objects;
using QuickLoanAPI.Application.Services.Interfaces;

namespace QuickLoanAPI.Controllers
{
  public class UserController : BaseController
  {
    private IUserService _userService;
    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<(string, UserType)>> UserLogin(LoginModel loginform)
    {
      var user = await _userService.Login(loginform);
      return Ok(new { token = user.Item1, userType = user.Item2 });
    }

    [HttpPost]
    public async Task<ActionResult<string>> UserRegistration(UserRegistration user)
    {
      return Ok(await _userService.Registration(user));
    }
  }
}

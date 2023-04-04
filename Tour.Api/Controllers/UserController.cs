using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tour.Application.Dto;
using Tour.Application.Interfaces;

namespace Tour.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpDto su)
        {
            try
            {
                var result = await _userService.SignUpAsync(su);

                return Ok(result.Succeeded);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInDto si)
        {
            try
            {
                var result = await _userService.SignInAsync(si);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Tour.Application.Services.Authentication;
using Tour.Contracts.Authentication;
namespace Tour.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication _authentication;

        public AuthenticationController(IAuthentication authentication)
        {
            _authentication = authentication;
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(RegisterRequest req)
        {
            try
            {
                var result = _authentication.Register(req.FirstName, req.LastName, req.Email, req.Password);
                var response = new AuthenticationReponse(result.Id, result.FirstName, result.LastName, result.Email, result.Token);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }

        //[HttpPost("SignIn")]
        //public async Task<IActionResult> SignIn(LoginRequest req)
        //{
        //    try
        //    {
        //        var result = _authentication.Login(req.Email, req.Password);
        //        var response = new AuthenticationReponse(result.Id, result.FirstName, result.LastName, result.Email, result.Token);

        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Unauthorized(ex.Message);
        //    }

        //}
    }
}

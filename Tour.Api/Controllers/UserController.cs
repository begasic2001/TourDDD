using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Tour.Api.Models;
using Tour.Application.Dto.User;
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

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                Uri u = new Uri("https://localhost:9001/api/User/GetAllUsers");
                var t = Task.Run(() => _userService.GetUri(u));
                t.Wait();
                await Console.Out.WriteLineAsync(t.Result);
                var result = JsonConvert.DeserializeObject(t.Result,typeof(List<AllUserModel>));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpDto model)
        {
            try
            {
                Uri u = new Uri("https://localhost:9001/api/User/SignUp");
                var payload = JsonConvert.SerializeObject(model);
                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                var t = Task.Run(() => _userService.PostUri(u, c));
                t.Wait();

                var result = JsonConvert.DeserializeObject(t.Result, typeof(ResponseRegister));
            
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            try
            {
                Uri u = new Uri("https://localhost:9001/api/User/SignIn");
                var payload = JsonConvert.SerializeObject(model);
                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                var t = Task.Run(() => _userService.PostUri(u, c));
                t.Wait();

                
                var result = JsonConvert.DeserializeObject(t.Result,typeof(AuthToken));
                return Ok(result);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

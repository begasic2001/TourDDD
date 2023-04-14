using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Tour.Api.Models;
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

        //[HttpPost("SignUp")]
        //public async Task<IActionResult> SignUp(SignUpDto su)
        //{
        //    try
        //    {
        //        var result = await _userService.SignUpAsync(su);

        //        return Ok(result.Succeeded);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Unauthorized(ex.Message);
        //    }

        //}

        //[HttpPost("SignIn")]
        //public async Task<IActionResult> SignIn(SignInDto si)
        //{
        //    try
        //    {
        //        var result = await _userService.SignInAsync(si);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Unauthorized(ex.Message);
        //    }

        //}

        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var result = await _userService.GetAllUser();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

                //await Console.Out.WriteLineAsync(t.Result);
                var result = JsonConvert.DeserializeObject(t.Result,typeof(AuthToken));
                return Ok(result);
                //PostUri
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Web;
using Tour.Api.Models;
using Tour.Api.Models.Logout;
using Tour.Application.Dto.User;
using Tour.Application.Interfaces;
using Tour.Domain.Entities;
using Tour.Infrastructure.Data;

namespace Tour.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly TourDatabaseContext _context;

        public UserController(IUserService userService, TourDatabaseContext context)
        {
            _userService = userService;
            _context = context;
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

        [HttpGet("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                Uri u = new Uri($"https://localhost:9001/api/User/GetUserByEmail?email={email}");
                var t = Task.Run(() => _userService.GetUri(u));
                t.Wait();
               
                var result = JsonConvert.DeserializeObject(t.Result, typeof(AllUserModel));

                
                // get Id 
                //Type t3 = result.GetType();
                //PropertyInfo[] props = t3.GetProperties();
                //var id = props.First().GetValue(result).GetType();
                
                //Console.WriteLine(id);
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
                
                // get user register 
  
                Uri u2 = new Uri($"https://localhost:9001/api/User/GetUserByEmail?email={model.Email}");
                var t2 = Task.Run(() => _userService.GetUri(u2));
                t2.Wait();
                Console.WriteLine("Email::::::"+result);
                var result2 = JsonConvert.DeserializeObject(t2.Result, typeof(AllUserModel));

     
                PropertyInfo[] props = result2.GetType().GetProperties();
                var id = props.First().GetValue(result2).ToString();
                var passwordHash = props.Last().GetValue(result2).ToString();
                if (id != null)
                {
                    var user = new Users()
                    {
                        Id = id,
                        Email = model.Email,
                        Password = passwordHash,
                    };
                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    return Ok(result);
                }
                else
                {
                    return NotFound($"{id} not found");
                }
                   
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout(ModelToken model)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Split()[1];
                Uri u = new Uri("https://localhost:9001/api/User/Logout");
                var payload = JsonConvert.SerializeObject(model);
                HttpContent c = new StringContent(payload, Encoding.UTF8, "application/json");
                var t = Task.Run(() => _userService.PostUri(u, c));
                t.Wait();
                await Console.Out.WriteLineAsync(t.Result);
                var result = JsonConvert.DeserializeObject(t.Result, typeof(LogoutResult));
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

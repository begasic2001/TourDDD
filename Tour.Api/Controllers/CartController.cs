using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using Tour.Api.Models;
using Tour.Infrastructure.Data;

namespace Tour.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly TourDatabaseContext context;
        public CartController(TourDatabaseContext context)
        {
            this.context = context;   
        }
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddToCart()
        {
            try
            {
                //AddToCartModel model
                //Console.WriteLine(model.TourId);
                //Console.WriteLine(model.UserId);
                //Console.WriteLine(model.Amount);
                //string StoredProc = "exec sp_AddToCart " +
                //        "@CUS = " + model.UserId + "," +
                //        "@ITEM = '" + model.TourId + "'," +
                //        "@Amount= '" + model.Amount + "'";
                //var result = await context.CartOrders.FromSqlRaw(StoredProc).ToListAsync();
                var result = HttpContext.Request.Headers["Authorization"].ToString().Split()[1];
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(result);
                var tokenS = jsonToken as JwtSecurityToken;
                var id = tokenS.Claims.First(claim => claim.Type == "nameid").Value;
                return Ok(id);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
          
        }
    }
}

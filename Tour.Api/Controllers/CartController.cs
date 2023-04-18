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
using Tour.Domain.Entities;
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
        public async Task<IActionResult> AddToCart(AddToCartModel model)
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Split()[1];
                
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);
                var tokenS = jsonToken as JwtSecurityToken;
                var UsersId = tokenS.Claims.First(claim => claim.Type == "nameid").Value;
                Console.WriteLine("TourId:::::"+model.TourId);
                //var hasCart = context.CartOrders.SingleOrDefault(c => c.UsersId == UsersId && c.TourId == model.TourId);
                //if(hasCart == null)
                //{
                //    double price = context.Tour.Where(t => t.Id == model.TourId).Select(t => t.Price).Cast<double>();
                //    Console.WriteLine("Price::::"+price);
                //    var cart = new CartOrder()
                //    {
                //        UsersId = UsersId,
                //        TourId = model.TourId,
                //        Amount = 1,
                //        SingleProduct = Convert.ToDouble(price),
                //    };
                //    await context.CartOrders.AddAsync(cart);
                //}
                //else
                //{
                //    hasCart.Amount++;
                //}

                //context.SaveChanges();
                //return Ok(hasCart);
                // or call store procedure
                string StoredProc = $"exec sp_AddToCart @CUS ='{UsersId}', @ITEM ='{model.TourId}', @Amount ='1'";
                await context.CartOrders.FromSqlRaw(StoredProc).ToListAsync();
                return StatusCode(StatusCodes.Status200OK,
                       new
                       {
                           Status = "Success",
                           Message = $"Add  for {UsersId} succesffully"
                       });
                
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
          
        }

        [HttpGet("GetCart")]
        public async Task<IActionResult> GetCart()
        {
            try
            {

                var token = HttpContext.Request.Headers["Authorization"].ToString().Split()[1];

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);
                var tokenS = jsonToken as JwtSecurityToken;
                var UsersId = tokenS.Claims.First(claim => claim.Type == "nameid").Value;
                
                 
                var result = await context.CartOrders.FromSqlRaw($"exec sp_getCart @CUS = '{UsersId}' ").ToListAsync();    
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("RemoveElementCart")]
        public async Task<IActionResult> RemoveElementCart(RemoveElementCart removeElementCart)
        {
            try
            {

                var token = HttpContext.Request.Headers["Authorization"].ToString().Split()[1];

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);
                var tokenS = jsonToken as JwtSecurityToken;
                var UsersId = tokenS.Claims.First(claim => claim.Type == "nameid").Value;

                var result = await context.CartOrders.FromSqlRaw($"exec sp_removeElementCart @CUS = '{UsersId}',@ITEM ='{removeElementCart.TourId}' ").ToListAsync();
                return Ok(result);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DestroyCart")]
        public async Task<IActionResult> DestroyCart()
        {
            try
            {

                var token = HttpContext.Request.Headers["Authorization"].ToString().Split()[1];

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);
                var tokenS = jsonToken as JwtSecurityToken;
                var UsersId = tokenS.Claims.First(claim => claim.Type == "nameid").Value;

                var result = await context.CartOrders.FromSqlRaw($"exec sp_destroyCart @CUS = '{UsersId}'").ToListAsync();
                return Ok(result);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateCart")]
        public async Task<IActionResult> UpdateCart(AddToCartModel model)
        {
            try
            {
                //var token = HttpContext.Request.Headers["Authorization"].ToString().Split()[1];

                //var handler = new JwtSecurityTokenHandler();
                //var jsonToken = handler.ReadToken(token);
                //var tokenS = jsonToken as JwtSecurityToken;
                //var UsersId = tokenS.Claims.First(claim => claim.Type == "nameid").Value;

                //var result = await context.CartOrders.FromSqlRaw($"exec sp_updateCart @CUS ='{UsersId}', @ITEM ='{model.TourId}', @Amount ='{model.Amount}'").ToListAsync();
                //return Ok(result);
                return Ok();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Purchase")]
        public async Task<IActionResult> Purchase()
        {
            try
            {
                var token = HttpContext.Request.Headers["Authorization"].ToString().Split()[1];

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);
                var tokenS = jsonToken as JwtSecurityToken;
                var UsersId = tokenS.Claims.First(claim => claim.Type == "nameid").Value;

                var result = await context.Carts.FromSqlRaw($"exec sp_PURCHASE_CART @CUS ='{UsersId}'").ToListAsync();
                return Ok(result);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}

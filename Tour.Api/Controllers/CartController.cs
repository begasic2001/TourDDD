using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tour.Api.Models;
using Tour.Infrastructure.Data;

namespace Tour.Api.Controllers
{
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
            Console.WriteLine(model.TourId);
            Console.WriteLine(model.UserId);
            Console.WriteLine(model.Amount);
            string StoredProc = "exec sp_AddToCart " +
                    "@CUS = " + model.UserId + "," +
                    "@ITEM = '" + model.TourId + "'," +
                    "@Amount= '" + model.Amount + "'";
            var result = await context.CartOrders.FromSqlRaw(StoredProc).ToListAsync();
            return Ok(result);
        }
    }
}

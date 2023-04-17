using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Tour.Domain.Entities
{
    public class Users 
    {
        [Key]
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Avatar { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Cart>? Carts { get; set; }
        public ICollection<CartOrder>? CartOrders { get; set; }
    }
}

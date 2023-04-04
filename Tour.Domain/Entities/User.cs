using Microsoft.AspNetCore.Identity;
namespace Tour.Domain.Entities
{
    public class User : IdentityUser
    {
        public string? Avatar { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}

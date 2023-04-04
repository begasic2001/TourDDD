using System.ComponentModel.DataAnnotations;

namespace Tour.Domain.Entities
{
    public class Order
    {
        [Key]
        public string Id { get; set; }
        public double Total { get; set; }
        public DateTime DateOrder { get; set; }

        public string? UserId { get; set; }
        public User? User { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Tour.Domain.Entities
{
    public class Order
    {
        [Key]
        public string Id { get; set; }
        public double Total { get; set; }
        public DateTime DateOrder { get; set; }
       
        public string? UsersId { get; set; }
        public Users? Users { get; set; }
        public string? ShippingId { get; set; }
        public Shipping? Shipping { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}

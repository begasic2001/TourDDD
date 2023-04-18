using System.ComponentModel.DataAnnotations;

namespace Tour.Domain.Entities
{
    public class OrderDetail
    {
        [Key]
        public string id { get; set; }
        public int? quantity { get; set; }
        public double? SinglePrice { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }
        public string TourId { get; set; }
        public Tours Tour { get; set; }
    }
}

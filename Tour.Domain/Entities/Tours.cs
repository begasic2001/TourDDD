using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Tour.Domain.Entities
{
    public class Tours
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int MaxTourists { get; set; }
        public string? TransportId { get; set; }
        public Transport? Transport { get; set; }
        public string? CityId { get; set; }
        public City? City { get; set; }
        public string? SightId { get; set; }
        public Sight? Sight { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<CartOrder>? CartOrders { get; set; }
    }
}
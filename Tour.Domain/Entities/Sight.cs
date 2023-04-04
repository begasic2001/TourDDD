using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Tour.Domain.Entities
{
    public class Sight
    {
        [Key]
        public string Id { get; set; }
        public string SightName { get; set; }
        public double SightForMoney { get; set; }
        public string? Picture { get; set; }
        public string? CityId { get; set; }
        public City? City { get; set; }
        public ICollection<Tours>? Tours { get; set; }
    }
}

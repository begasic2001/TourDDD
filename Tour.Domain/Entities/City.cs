using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Tour.Domain.Entities
{
    public class City
    {
        [Key]
        public string Id { get; set; }
        public string CityName { get; set; }
        public ICollection<Sight>? Sights { get; set; }
        public string? CountryId { get; set; }
        public Country? Country { get; set; }
        public ICollection<Tours>? Tours { get; set; }
    }
}

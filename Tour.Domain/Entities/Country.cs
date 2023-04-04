using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Tour.Domain.Entities
{
    public class Country
    {
        [Key]
        public string Id { get; set; }
        public string CountryName { get; set; }
        public ICollection<City>? Cities { get; set; }
    }
}

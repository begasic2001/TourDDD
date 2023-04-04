using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Tour.Domain.Entities
{
    public class Transport
    {
        [Key]
        public string Id { get; set; }
        public string TransportName { get; set; }
        public ICollection<Tours>? Tours { get; set; }
    }
}

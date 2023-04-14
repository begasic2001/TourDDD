using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour.Domain.Entities
{
    public class Shipping
    {
        [Key]
        public string Id { get; set; }
        public string ship_name { get; set; }
        public string ship_address { get; set; }
        public string ship_phone { get; set;}
        public string ship_email { get; set;}
        public string ship_city { get; set;}
        public ICollection<Order> Orders { get; set; }
    }
    
}

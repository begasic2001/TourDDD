using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour.Domain.Entities
{
    public class CartOrder
    {
        
        public string? UsersId { get; set; }
        public Users? Users { get; set; }
        
        public string? TourId { get; set; }
        public Tours Tour { get; set; }
        public int Amount { get; set; }
        public double SingleProduct { get; set; }
    }
}

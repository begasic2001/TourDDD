using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string UsersId { get; set; }
        public Users Users { get; set; }  
        public double Total { get; set; }
    }
}

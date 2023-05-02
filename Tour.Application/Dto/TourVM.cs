using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour.Application.Dto
{
    public class TourVM
    {
        public string name { get; set; }
        public string CityName { get; set; }
        public string SightName { get; set; }
        public string TransportName { get; set; }
        public double Price { get; set; }
    }
}

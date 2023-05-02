using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour.Application.Dto
{
    public class SearchVM
    {
        public IEnumerable<TourVM> Results { get; set; }
        public int TotalPage { get; set; }
    }
}

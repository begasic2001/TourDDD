using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour.Domain.Entities;

namespace Tour.Infrastructure.Data
{
    public class SeedData
    {
        private readonly TourDatabaseContext tourDatabaseContext;
        public SeedData(TourDatabaseContext tourDatabaseContext)
        {
            this.tourDatabaseContext = tourDatabaseContext;
        }

        public void Seed()
        {
            if(!tourDatabaseContext.Country.Any())
            {
                var countries = new List<Country>()
                {
                    new Country(){Id = "1", CountryName="việt nam"},
                    new Country(){Id = "2", CountryName="thái lan"},
                    new Country(){Id = "3", CountryName="hàn quốc"},
                    new Country(){Id = "4", CountryName="nhật bản"},
                    new Country(){Id = "5", CountryName="singapor"},
                };

                tourDatabaseContext.Country.AddRange(countries);
            }
        }
    }
}

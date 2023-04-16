using Microsoft.EntityFrameworkCore;
using Tour.Domain.Entities;

namespace Tour.Infrastructure.Data
{
    public class TourDatabaseContext : DbContext
    {
        //IdentityDbContext<User>
        public TourDatabaseContext(DbContextOptions<TourDatabaseContext> options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Shipping> Shipping { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Sight> Sight { get; set; }
        public DbSet<Transport> Transport { get; set; }
        public DbSet<Tours> Tour { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartOrder> CartOrders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CartOrder>()
                .HasKey(c => new { c.UsersId, c.TourId });


            // data country
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = "1",
                    CountryName = "việt nam"
                },
                new Country { Id = "2", CountryName = "hàn quốc" },
                new Country
                  {
                      Id = "3",
                      CountryName = "nhật bản"
                  },
                new Country
                   {
                       Id = "4",
                       CountryName = "thái lan"
                   },
                 new Country
                    {
                        Id = "5",
                        CountryName = "singapor"
                    }
            );
            // data city
            modelBuilder.Entity<City>().HasData(
                new City { Id = "1", CityName = "tphcm" , CountryId="1"},
                new City { Id = "2", CityName = "vũng tàu", CountryId = "1" },
                new City { Id = "3", CityName = "tokyo", CountryId = "3" },
                new City { Id = "4", CityName = "bangkok", CountryId = "4" },
                new City { Id = "5", CityName = "seoul", CountryId = "2" },
                new City { Id = "6", CityName = "busan", CountryId = "2" }
            );
            // data transport
            modelBuilder.Entity<Transport>().HasData(
                new Transport { Id = "1", TransportName = "Xe Khách"}
            );
            // data sight
            modelBuilder.Entity<Sight>().HasData(
                new Sight { Id = "1", CityId = "1" , SightName = "Đầm Sen", SightForMoney = 140000}
            );
            // data tour
            modelBuilder.Entity<Tours>().HasData(
                new Tours { Id = "1" , SightId = "1",TransportId = "1" , CityId = "1", Name = "Du Lịch TPHCM"
                    , Price = 1000000 , MaxTourists = 50 , StartDate = DateTime.UtcNow , EndDate = DateTime.UtcNow
                }   
            );
        }
    }
}

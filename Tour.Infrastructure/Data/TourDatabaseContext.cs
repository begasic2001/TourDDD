using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tour.Domain.Entities;

namespace Tour.Infrastructure.Data
{
    public class TourDatabaseContext : IdentityDbContext<User>
    {
        public TourDatabaseContext(DbContextOptions<TourDatabaseContext> options) : base(options)
        {

        }
        public DbSet<Country> Country { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Sight> Sight { get; set; }
        public DbSet<Transport> Transport { get; set; }
        public DbSet<Tours> Tour { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

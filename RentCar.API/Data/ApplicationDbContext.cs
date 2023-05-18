using Microsoft.EntityFrameworkCore;
using RentCar.API.Models;

namespace RentCar.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }
    }
}

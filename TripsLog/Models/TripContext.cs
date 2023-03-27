using Microsoft.EntityFrameworkCore;

namespace TripLog.Models
{
    public class TripContext : DbContext
    {
        public TripContext(DbContextOptions<TripContext> options) : base(options)
        {
        }

        public DbSet<Trip> Trip { get; set; }
    }
}

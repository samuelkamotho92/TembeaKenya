using Microsoft.EntityFrameworkCore;
using TourService.Model;

namespace TourService.Data
{
    public class TourDbContext : DbContext
    {
        public TourDbContext(DbContextOptions<TourDbContext>options):base(options) { }
       public DbSet<Tour> tours { get; set; }
    }
}

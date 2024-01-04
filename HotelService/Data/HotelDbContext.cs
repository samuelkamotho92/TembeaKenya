using HotelService.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Data
{
    public class HotelDbContext :DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext>options):base(options) { }
       public DbSet<Hotel> hotels { get; set; }
    }
}

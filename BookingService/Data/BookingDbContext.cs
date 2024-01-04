using BookingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Data
{
    public class BookingDbContext :DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext>options):base(options) { }
        public DbSet<Booking> bookings { get; set; }
    }
}

using BookingService.Data;
using BookingService.Models;
using BookingService.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Services
{
    public class BookingServices : IBookingService
    {
        private readonly BookingDbContext _context;
        public BookingServices(BookingDbContext context)
        {
            _context = context;

        }
        public async Task<string> AddBooking(Booking booking)
        {
            try
            {
                await  _context.bookings.AddAsync(booking);
                await _context.SaveChangesAsync();
                return "Booking added successfully";
            }catch (Exception ex)
            {
                return $"{ex.InnerException}";
            }
        }

        public async Task<string> DeleteBooking(Booking booking)
        {
            try
            {
                _context.bookings.Remove(booking);
                await _context.SaveChangesAsync();
                return "Booking removed successfully";
            }catch(Exception ex)
            {
                return $"{ex.InnerException}";
            }
        }

        public async Task<Booking> GetABooking(Guid Id)
        {
            try
            {
              var booking = _context.bookings.Where(x=>x.Id == Id).FirstOrDefault();
               return booking;
            }catch(Exception ex)
            {
                return new Booking();
            }
        }

        public async Task<List<Booking>> GetAllBooking(Guid UserId)
        {
          var bookings = await  _context.bookings.Where(x=>x.UserId == UserId).ToListAsync();
            return bookings;
        }

        public async Task saveChanges()
        {
            await _context.SaveChangesAsync();
        }

            public Task<string> UpdateBooking(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}

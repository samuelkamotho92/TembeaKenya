using BookingService.Models;

namespace BookingService.Services.IService
{
    public interface IBookingService
    {
        Task<string> AddBooking(Booking booking);

        Task saveChanges();
        Task<List<Booking>> GetAllBooking(Guid UserId);
        Task<Booking> GetABooking(Guid Id);

        Task<string>UpdateBooking (Booking booking);

        Task<string> DeleteBooking(Booking booking);
    }
}

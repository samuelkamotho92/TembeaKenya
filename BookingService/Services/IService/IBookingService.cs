using BookingService.Dto;
using BookingService.Models;

namespace BookingService.Services.IService
{
    public interface IBookingService
    {
        Task<string> AddBooking(Booking booking);

        Task saveChanges();
        Task<List<Booking>> GetAllBooking(Guid UserId);
        Task<List<Booking>> GetAllBookings();
        Task<Booking> GetABooking(Guid Id);

        Task<string>UpdateBooking (Booking booking);

        Task<string> DeleteBooking(Booking booking);

        Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto);

        Task<bool> validatePayments(Guid BookingId);
    }
}

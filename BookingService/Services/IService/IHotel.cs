using BookingService.Dto;

namespace BookingService.Services.IService
{
    public interface IHotel
    {
        Task<HotelDto> GetHotelById(Guid id);
    }
}

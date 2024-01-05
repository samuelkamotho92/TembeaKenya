using BookingService.Dto;

namespace BookingService.Services.IService
{
    public interface ITour
    {
        Task<TourDto> GetATour(Guid id);
    }
}

using HotelService.Dto;

namespace HotelService.Services.IServices
{
    public interface ITour
    {
        Task<TourDto> GetTourById(Guid Id);
    }
}

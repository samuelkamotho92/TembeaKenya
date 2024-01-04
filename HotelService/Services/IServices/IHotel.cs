using HotelService.Dto;
using HotelService.Models;

namespace HotelService.Services.IServices
{
    public interface IHotel
    {
        Task<string> AddHotel(Hotel hotel);
        //Get list of  all hotels in one tour; 
        Task<List<Hotel>> GetAllHotel(Guid TourId);
        Task<Hotel> GetHotelById(Guid Id);
        Task<string> DeleteHotel(Hotel hotel);
        Task <string> UpdatedHotel(Hotel hotel);
    }
}

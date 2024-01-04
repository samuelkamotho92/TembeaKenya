using HotelService.Data;
using HotelService.Models;
using HotelService.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Services
{
    public class HotelServices : IHotel
    {
        private readonly HotelDbContext _context;
       
        public HotelServices(HotelDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddHotel(Hotel hotel)
        {
            try
            {
                await _context.hotels.AddAsync(hotel);
              await   _context.SaveChangesAsync();
                return "Hotel Added successfully";
            }
            catch (Exception ex)
            {
                return $"{ex.InnerException}";
            }
        }

        public async Task<string> DeleteHotel(Hotel hotel)
        {
            try
            {
                _context.hotels.Remove(hotel);
                _context.SaveChanges();
                return "Hotel removed successfully";
            }catch(Exception ex)
            {
                return $"{ex.InnerException}";
            }
        }

        public async Task<List<Hotel>> GetAllHotel(Guid TourId)
        {
            return await _context.hotels.Where(x => x.TourId == TourId).ToListAsync();
        }

        public async  Task<Hotel> GetHotelById(Guid Id)
        {
           var hotel = _context.hotels.Where(x => x.Id == Id).FirstOrDefault();
            return hotel;
        }

        public async Task<string> UpdatedHotel(Hotel hotel)
        {
            try
            {
                _context.hotels.Update(hotel);
                await _context.SaveChangesAsync();
                return "Hotel updated successfully";

            }catch(Exception ex)
            {
                return $"{ex.Message}";
            }
        }
    }
}

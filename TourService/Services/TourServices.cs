using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TourService.Data;
using TourService.Dto;
using TourService.Migrations;
using TourService.Model;

namespace TourService.Services
{
    public class TourServices : ITour
    {
        private readonly TourDbContext _context;
      

        public TourServices(IMapper mapper , TourDbContext context)
        {
            _context = context;
        }
        public async Task<string> createAtour(Tour tour)
        {
            try
            {
                _context.Add(tour);
               await  _context.SaveChangesAsync();
                return "Succesfully added tour";
            }catch (Exception ex)
            {
                return $"{ex.InnerException}";
            }
        }

        public async Task<string> deleteTour(Tour tour)
        {
            try
            {
                _context.tours.Remove(tour);
                await _context.SaveChangesAsync();
                return "Deleted successfully";
            }catch(Exception ex)
            {
                return $"{ex.InnerException}";
            }
        }

        public async  Task<Tour> getTour(Guid id)
        {
            try
            {
               var tour = await  _context.tours.FindAsync(id);
                return tour;
            }catch(Exception ex)
            {
                return new Tour();
            }
        }

        public async  Task<List<TourImagesRespDto>> getTours()
        {
            var tours = await _context.tours.Select(t => new TourImagesRespDto()
            {
                Guid = t.Id,
                TourName = t.TourName,
                TourDescription = t.TourDescription,
                price = t.Price,
                endDate = t.EndDate,
                startDate = t.StartDate,
                TourImages = t.TourImages.Select(x => new AddTourImageDto()
                {
                    Image = x.image
                }).ToList()
            }).ToListAsync();

            return tours;
        }

        public async Task<string> updateTour(Tour tour)
        {
            try
            {
                _context.tours.Update(tour);
                _context.SaveChanges();
                return "update made successfully";
            }catch(Exception ex)
            {
                return $"{ex.InnerException}";
            }
        }
    }
}

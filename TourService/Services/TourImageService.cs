using TourService.Data;
using TourService.Model;

namespace TourService.Services
{
    public class TourImageService : ITourImage
    {
        private readonly TourDbContext _context;
        
        public TourImageService(TourDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddImage(Guid id, TourImages image)

        {//get specific tour based on Id
            try
            {
                var tour = _context.tours.Where(x => x.Id == id).FirstOrDefault();
                if (tour != null)
                {
                    tour.TourImages.Add(image);
                    await _context.SaveChangesAsync();
                    return "Image added successfully";
                }
                return "Tour does not exist";
            }
            catch(Exception ex)
            {
                return $"{ex.InnerException}";
            }
        }
    }
}

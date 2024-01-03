using TourService.Model;

namespace TourService.Services
{
    public interface ITourImage
    {
        Task<string> AddImage(Guid id, TourImages tour);
    }
}

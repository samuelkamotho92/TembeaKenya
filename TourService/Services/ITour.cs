using TourService.Model;

namespace TourService.Services
{
    public interface ITour
    {
        //create
        Task<string> createAtour(Tour tour);
        //Get list tour
        Task<List<Tour>> getTours();
        //Get one
        Task<Tour> getTour(Guid id);

        //Update tour
        Task<string> updateTour(Tour tour);

        //delete Tour
        Task<string> deleteTour(Tour tour);
    }
}

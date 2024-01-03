using System.ComponentModel.DataAnnotations.Schema;

namespace TourService.Model
{
    public class TourImages
    {
        public Guid id { get; set; }

        public string image { get; set; }

        [ForeignKey("tourId")]
        public Tour tour { get; set; } = default!;

        public Guid tourId { get; set; }
    }
}

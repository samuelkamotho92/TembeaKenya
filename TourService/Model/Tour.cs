namespace TourService.Model
{
    public class Tour
    {
        public Guid Id { get; set; }

        public string TourName { get; set; } = string.Empty;

        public string TourDescription { get; set; }

        public List<TourImages> TourImages { get; set; } = new List<TourImages>();

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Price { get; set; }
    }
}

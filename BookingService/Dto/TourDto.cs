namespace BookingService.Dto
{
    public class TourDto
    {
        public string TourName { get; set; } = string.Empty;

        public string TourDescription { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public int price { get; set; }
    }
}

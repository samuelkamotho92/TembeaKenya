namespace BookingService.Dto
{
    public class BookingDto
    {
        public Guid UserId { get; set; }

        public double BookingTotal { get; set; }

        public int Adults { get; set; }

        public int Kids { get; set; }

        public Guid TourId { get; set; }

        public Guid HotelId { get; set; }
    }
}

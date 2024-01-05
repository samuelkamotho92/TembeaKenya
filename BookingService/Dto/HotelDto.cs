namespace BookingService.Dto
{
    public class HotelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        //Pass an Id of tour while creating a  Hotel;
        public Guid TourId { get; set; }

        public int AdultPrice { get; set; }

        public int KidsPrice { get; set; }
    }
}

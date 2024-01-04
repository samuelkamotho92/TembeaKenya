namespace HotelService.Models
{
    public class Hotel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        //Id of associated tour
        public Guid TourId { get; set; }

        public int AdultPrice { get; set; }

        public int KidsPrice { get; set; }
    }
}

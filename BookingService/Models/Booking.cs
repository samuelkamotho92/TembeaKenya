namespace BookingService.Models
{
    public class Booking
    {
        public Guid Guid { get; set; }

        public Guid UserId { get; set; }

        //apply coupon to the booking
        public string CouponCode { get; set; } = string.Empty;

        public double Discount { get; set; }

        public double BookingTotal { get; set; }

        public int Adults { get; set; }

        public int Kids { get; set; }

        public Guid TourId { get; set; }    

        public Guid HotelId { get; set; }
    }
}

namespace BookingService.Dto
{
    public class CouponDto
    {

        public Guid Id { get; set; }
        //coupon code
        public string CouponCode { get; set; } = string.Empty;

        //coupon amount
        public int CouponAmount { get; set; }

        //min amount to apply coupon
        public int CouponMinAmount { get; set; }

    }
}

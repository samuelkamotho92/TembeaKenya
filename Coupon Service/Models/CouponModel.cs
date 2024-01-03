namespace Coupon_Service.Models
{
    public class CouponModel
    {
        public Guid id { get; set; }

        public string couponCode { get; set; }

        public int couponAmount { get; set; }

        public int couponMinAmount { get; set; }
    }
}

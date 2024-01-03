using System.ComponentModel.DataAnnotations;

namespace Coupon_Service.Dtos
{
    public class CouponDto
    {
        [Required]
        public string couponCode { get; set; }

        [Required]
        public int couponAmount { get; set; }

        [Required]
        public int couponMinAmount { get; set; }
    }
}

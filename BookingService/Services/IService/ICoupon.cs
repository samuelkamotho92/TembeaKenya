using BookingService.Dto;

namespace BookingService.Services.IService
{
    public interface ICoupon
    {
        Task<CouponDto> GetCouponByCode(string couponCode);
    }
}

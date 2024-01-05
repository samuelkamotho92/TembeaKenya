using Coupon_Service.Dtos;
using Coupon_Service.Models;

namespace Coupon_Service.Service.IService
{
    public interface ICouponService
    {
        Task<List<CouponModel>> GetCoupons();

        Task<CouponModel> GetCoupon(Guid id);

        //Get a coupon by code
        Task<CouponModel> GetCouponByCode(string code);

        Task<string> AddCoupon(CouponModel coupon);
        
        Task<string> updateCoupon(CouponModel coupon);  

        Task<string> deleteCoupon(CouponModel coupon);
    }
}

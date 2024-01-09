using Coupon_Service.Data;
using Coupon_Service.Models;
using Coupon_Service.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace Coupon_Service.Service
{
    public class CouponService : ICouponService
    {
        private readonly CouponDBContext _couponDBContext;
        public CouponService(CouponDBContext couponDBContext)
        {
            _couponDBContext = couponDBContext;
        }
        public async Task<string> AddCoupon(CouponModel coupon)
        {
            try
            {
               await  _couponDBContext.coupons.AddAsync(coupon);
             
               await _couponDBContext.SaveChangesAsync();
                return "Coupon added successfully";
            }
            catch(Exception ex)
            {
                return $"{ex.InnerException}";
            }
   
        }

        public async Task<string> deleteCoupon(CouponModel coupon)
        {
            try
            {
                _couponDBContext?.coupons.Remove(coupon);
                await _couponDBContext.SaveChangesAsync();
                return "Coupon deleted successfully";
            }
            catch(Exception ex)
            {
                return $"{ex.InnerException}";
            }
         
        }

        public async Task<CouponModel> GetCoupon(Guid id)
        {
            try
            {
                var coupon = await _couponDBContext.coupons.Where(x => x.id == id).FirstOrDefaultAsync();
                Console.WriteLine(coupon);
                return coupon;
            }
            catch(Exception ex)
            {
                return new CouponModel();
            }
        
        }

        public async Task<CouponModel> GetCouponByCode(string code)
        {
            return await _couponDBContext.coupons.Where(x=>x.couponCode == code).FirstOrDefaultAsync();
        }

        public async Task<List<CouponModel>> GetCoupons()
        {
           List<CouponModel> coupons =  _couponDBContext.coupons.ToList();
            return coupons;
        }

        public async Task<string> updateCoupon(CouponModel coupon)
        {
            try
            {
               _couponDBContext.coupons.Update(coupon);
               await  _couponDBContext.SaveChangesAsync();
                return "Coupon updated successfully";
            }
            catch(Exception ex)
            {
                return $"{ex.InnerException}";
            }   
        }
    }
}

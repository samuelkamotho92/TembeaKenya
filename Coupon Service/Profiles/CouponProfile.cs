using AutoMapper;
using Coupon_Service.Dtos;
using Coupon_Service.Models;

namespace Coupon_Service.Profiles
{
    public class CouponProfile:Profile
    {
        public CouponProfile()
        {        
        CreateMap<CouponDto,CouponModel>().ReverseMap();
        }
    }
}

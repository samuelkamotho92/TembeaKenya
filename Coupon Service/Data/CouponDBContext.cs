using Coupon_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Coupon_Service.Data
{
    public class CouponDBContext:DbContext
    {
        public CouponDBContext(DbContextOptions<CouponDBContext>options):base(options) { }
        public DbSet<CouponModel> coupons { get; set; }
    }
}

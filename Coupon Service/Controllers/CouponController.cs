using AutoMapper;
using Coupon_Service.Dtos;
using Coupon_Service.Models;
using Coupon_Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Net;

namespace Coupon_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CouponController : ControllerBase
    {
        private readonly ICouponService _couponService;
        private readonly ResponseDto _responseDto;
        private readonly IMapper _mapper;

        public CouponController(IMapper mapper, ICouponService couponService)
        {
            _mapper = mapper;
            _couponService = couponService;
            _responseDto = new ResponseDto();
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> createCoupon(CouponDto coupon)
        {
            try
            {
                var createdCoupon = _mapper.Map<CouponModel>(coupon);
                string resp = await _couponService.AddCoupon(createdCoupon);
                _responseDto.result = coupon;
                _responseDto.message = resp;
                _responseDto.statusCode = HttpStatusCode.OK;
                //add in db and stripe
                var options = new CouponCreateOptions() {   
                AmountOff=(long) createdCoupon.couponAmount * 100,
                Currency = "kes",
                Id=createdCoupon.couponCode,
                Name=createdCoupon.couponCode                
                };
                var service = new CouponService();
                service.Create(options);

                return Created("", _responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.errorMessage = ex.Message;
                return BadRequest(_responseDto);
            }

        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> getAllCoupons()
        {
            try
            {
                var coupons = await _couponService.GetCoupons();
                _responseDto.result = coupons;
                _responseDto.message = "success";
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.errorMessage = ex.Message;
                return BadRequest(_responseDto);
            }
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> updateCoupon(Guid id, CouponDto couponDto)
        {
            try
            {
                var coupon = await _couponService.GetCoupon(id);
                if (coupon != null)
                {
                    var newcoupon = _mapper.Map(couponDto, coupon);
                    string resp = await _couponService.updateCoupon(newcoupon);
                    _responseDto.message = resp;
                    _responseDto.result = newcoupon;
                    _responseDto.statusCode = HttpStatusCode.OK;
                    var service = new  CouponService();
                    service.Delete(coupon.couponCode);

                    var options = new CouponCreateOptions() {
                        AmountOff = (long)newcoupon.couponAmount * 100,
                        Currency = "kes",
                        Id = newcoupon.couponCode,
                        Name = newcoupon.couponCode                   
                    };
                    service.Create(options);

                    return Ok(_responseDto);
                }
                _responseDto.errorMessage = "Coupon not found";
                return BadRequest(_responseDto);

            }
            catch (Exception ex)
            {
                _responseDto.errorMessage = ex.Message;
                return BadRequest(_responseDto);
            }
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> getOneCoupon(Guid id)
        {
            try
            {
                var coupon = await _couponService.GetCoupon(id);
                _responseDto.message = "success";
                _responseDto.result = coupon;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.errorMessage = ex.Message;
                return BadRequest(_responseDto);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> deleteCoupon(Guid id)
        {
            try
            {
                var coupon = await _couponService.GetCoupon(id);
                if (coupon != null)
                {
                    string resp = await _couponService.deleteCoupon(coupon);
                    _responseDto.message = resp;
                    var service = new Stripe.CouponService();
                    service.Delete(coupon.couponCode);
                    return Ok(_responseDto);
                }
                _responseDto.errorMessage = "Coupon does not exist";
                return NotFound(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.errorMessage = ex.Message;
                return BadRequest(_responseDto);
            }
        }

        [HttpGet("getbycode/{code}")]
        public async Task<ActionResult<ResponseDto>> getCouponByCode(string code)
        {
            try
            {
                var coupon = await _couponService.GetCouponByCode(code);
                if(coupon != null)
                {
                    _responseDto.message = "success";
                    _responseDto.result = coupon;
                    return Ok(_responseDto);
                }
                _responseDto.errorMessage = "coupon does not exist";
                return BadRequest(_responseDto);

            }
            catch(Exception ex)
            {
            _responseDto.errorMessage=ex.Message;
           return BadRequest(_responseDto);
            }
        }
    }
}

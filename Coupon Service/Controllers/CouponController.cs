﻿using AutoMapper;
using Coupon_Service.Dtos;
using Coupon_Service.Models;
using Coupon_Service.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                return Created("", _responseDto);
            }
            catch(Exception ex)
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
        public async Task<ActionResult<ResponseDto>> updateCoupon(Guid id)
        {
            try
            {
                var coupon = _couponService.GetCoupon(id);
                var newcoupon = _mapper.Map<CouponModel>(coupon);
                string resp = await _couponService.updateCoupon(newcoupon);
                _responseDto.message = resp;
                _responseDto.statusCode = System.Net.HttpStatusCode.OK;
                return Ok(_responseDto);
            }
            catch(Exception ex)
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
                var coupon = await  _couponService.GetCoupon(id);
                _responseDto.message= "success";
                _responseDto.result = coupon;
                return Ok(_responseDto);
            }
            catch(Exception ex)
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
                string resp = await _couponService.deleteCoupon(coupon);
                _responseDto.message = resp;
                return Ok(_responseDto);
            }
            catch(Exception ex)
            {
                _responseDto.errorMessage = ex.Message;
                return BadRequest(_responseDto);
            }        
        }
    }
}
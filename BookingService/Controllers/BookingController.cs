﻿using AutoMapper;
using BookingService.Dto;
using BookingService.Models;
using BookingService.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICoupon _couponService;
        private readonly IBookingService _bookingService;
        private readonly ITour _tourService;
        private readonly IHotel _hotelService;
        private readonly ResponseDto _responseDto;
        public BookingController(IMapper mapper, IBookingService bookingService, ITour tourService, IHotel hotelService, ICoupon couponService)
        {
            _bookingService = bookingService;
            _tourService = tourService;
            _couponService = couponService;
            _mapper = mapper;
            _hotelService = hotelService;
            _responseDto = new ResponseDto();
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> AddBooking(BookingDto dto)
        {

            var booking = _mapper.Map<Booking>(dto);
            //get tour and  hotel based on the id
            var tour = await _tourService.GetATour(booking.TourId);
            var hotel = await _hotelService.GetHotelById(booking.HotelId);
            if (hotel == null || tour == null)
            {
                _responseDto.errorMessage = "Invalid Values";
                return NotFound(_responseDto);
            }
            Double days = 1;
            if (tour.endDate == tour.startDate)
            {
                days = 1;
            }
            else
            {
                days = (tour.endDate - tour.startDate).TotalDays;
                Console.WriteLine(days);
            }

            var total = (tour.price) + (hotel.AdultPrice * dto.Adults * days) + (hotel.KidsPrice * dto.Kids * days);
            booking.BookingTotal = total;
            var res = await _bookingService.AddBooking(booking);
            _responseDto.result = res;

            return Ok(_responseDto);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<ResponseDto>> ApplyCoupon(Guid Id,string code){
            //get a booking based on the Id
            var booking = await _bookingService.GetABooking(Id);
            if(booking == null)
            {
                _responseDto.errorMessage = "Booking not found";
                return NotFound(_responseDto);
            }
            //get coupon based on the code , where we can a
            var coupon = await  _couponService.GetCouponByCode(code);
            if(coupon == null)
            {
                _responseDto.errorMessage = "Coupon is not valid";
                return NotFound(_responseDto);
            }
            //Check if the Amount has the target to be applied coupon ,pass the code and the discount we are applying
            if(coupon.CouponMinAmount <= booking.BookingTotal)
            {
                booking.CouponCode = coupon.CouponCode;
                booking.Discount = coupon.CouponAmount;
                await _bookingService.saveChanges();
                _responseDto.message = "Coupon applied";
                return Ok(_responseDto);
            }
            else
            {
                _responseDto.errorMessage = "Total amount has not reached the target to be applied a discount";
                return BadRequest(_responseDto);
            }         
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponseDto>> GetBookings(Guid userId)
        {
            try
            {
                var bookings = _bookingService.GetAllBooking(userId);
                _responseDto.result = bookings;
                _responseDto.message = "success";
                return Ok(_responseDto);
            }catch(Exception ex)
            {
                _responseDto.errorMessage = ex.Message;
                return BadRequest(_responseDto);
            }
        }
    }
}

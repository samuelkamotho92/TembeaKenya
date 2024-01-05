using AutoMapper;
using BookingService.Dto;
using BookingService.Models;
using BookingService.Services.IService;
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
        public BookingController(IMapper mapper, IBookingService bookingService, ITour tourService, IHotel hotelService,ICoupon couponService)
        {
            _bookingService = bookingService;
            _tourService = tourService;
            _couponService = couponService;
            _mapper = mapper;
            _hotelService = hotelService;
            _responseDto = new ResponseDto();
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> AddBooking(BookingDto dto)
        {
            try
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
                //get the total price
                var total = (tour.price) + (hotel.AdultPrice * dto.Adults * (tour.endDate - tour.startDate).TotalDays) + (hotel.KidsPrice * dto.Kids) * (tour.endDate - tour.startDate).TotalDays;
                booking.BookingTotal = total;
                Console.WriteLine(total);
                Console.WriteLine(booking.BookingTotal);
                Console.WriteLine(booking);
                var res = await _bookingService.AddBooking(booking);
                _responseDto.result = res;

                return Ok(_responseDto);
            }catch (Exception ex)
            {
                _responseDto.errorMessage += ex.Message;
                return BadRequest(_responseDto);
            }
        }
    }
}

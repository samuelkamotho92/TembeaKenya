using AutoMapper;
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
        [Authorize]
        public async Task<ActionResult<ResponseDto>> AddBooking(BookingDto dto)
        {
           
                var booking = _mapper.Map<Booking>(dto);
                //get tour and  hotel based on the id
                var tour = await _tourService.GetATour(booking.TourId);
                Console.WriteLine($"This is the tour: {tour.price}");
                var hotel = await _hotelService.GetHotelById(booking.HotelId);
                Console.WriteLine($"This is hotel:{hotel}");
                if (hotel == null || tour == null)
                {
                    _responseDto.errorMessage = "Invalid Values";
                    return NotFound(_responseDto);
                }
            Double days = 1;
            if (tour.endDate == tour.startDate)
            {
                 days = 1;             
            }else
            {

                days = (tour.endDate - tour.startDate).TotalDays;
                Console.WriteLine(days);
            }

           var  total = (tour.price) + (hotel.AdultPrice * dto.Adults * days) + (hotel.KidsPrice * dto.Kids * days);
            booking.BookingTotal = total;
                var res = await _bookingService.AddBooking(booking);
                _responseDto.result = res;

                return Ok(_responseDto);
        
        }
    }
}

using AutoMapper;
using HotelService.Dto;
using HotelService.Models;
using HotelService.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HotelService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITour _tourService;
        private readonly IHotel _hotelService;
        private readonly ResponseDto _responseDto;
        public HotelController(IMapper mapper,ITour tourService,IHotel hotelService)
        {
            _mapper = mapper;
            _tourService = tourService;
            _hotelService = hotelService;
            _responseDto = new ResponseDto();
        }
        [HttpPost("Add Hotel")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> CreateHotel(HotelDto hotelDto)
        {
            try
            {
                //Check if tour id pass does exists
                var tour = _tourService.GetTourById(hotelDto.TourId);
                var hotel = _mapper.Map<Hotel>(hotelDto);
                string resp =    await  _hotelService.AddHotel(hotel);
               _responseDto.message = resp;
               _responseDto.statusCode = HttpStatusCode.Created;
                _responseDto.result = await _hotelService.GetAllHotel(hotel.TourId);
                return Created("", _responseDto);
            }catch (Exception ex)
            {
                _responseDto.errorMessage = $"{ex.Message}";
                return BadRequest(_responseDto);
            }
        }
        [HttpGet("Get All hotels in tour{tourId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> GetAllHotelsByTour(Guid tourId)
        {
            try
            {
                var hotels = await _hotelService.GetAllHotel(tourId);
                var hotelDetail = _mapper.Map<List<HotelResponseDto>>(hotels);
                _responseDto.result = hotels;
                _responseDto.message = "success";
                return Ok(_responseDto);
            }catch(Exception ex)
            {
                _responseDto.errorMessage = $"{ex.InnerException}";
                return BadRequest(_responseDto);
            }
        }
        [HttpGet("Get one hotel {tourId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> GetHotel(Guid tourId)
        {
            try
            {
                var hotel = await _hotelService.GetHotelById(tourId);
                _responseDto.result = hotel;
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.errorMessage = $"{ex.Message}";
                return BadRequest(_responseDto);
            }
          
        }
        [HttpDelete("Delete Hotel{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> DeleteHotel(Guid id)
        {
            try
            {
                var hotel = await _hotelService.GetHotelById(id);
                if (hotel != null)
                {
                    string resp = await _hotelService.DeleteHotel(hotel);                 
                    _responseDto.message = "Hotel deleted successfully";
                    _responseDto.statusCode = HttpStatusCode.NoContent;
                    return Ok(_responseDto);
                }
                _responseDto.errorMessage = "Hotel Not Found";
                return BadRequest(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.errorMessage = $"{ex.Message}";
                return BadRequest(_responseDto);
            }
        }
        [HttpPut("Update Hotel{id}")]
        [Authorize(Roles = "Admin")]
        public  async Task<ActionResult<ResponseDto>> UpdateHotel(Guid id,HotelDto hotelDto)
        {
            try
            {
                var hotel = await _hotelService.GetHotelById(id);
                if (hotel != null)
                {
                    //pass the hotel to update
                    var newHotelDetail = _mapper.Map(hotelDto, hotel);
                    string resp = await  _hotelService.UpdatedHotel(newHotelDetail);
                    _responseDto.message = resp;
                    _responseDto.result = await _hotelService.GetHotelById(id);
                    return Ok(_responseDto);
                }
                _responseDto.message = "Hotel not found";
                return BadRequest(_responseDto);
            }
            catch(Exception ex)
            {
                _responseDto.errorMessage = $"{ex.Message}";
                return BadRequest(_responseDto);
            }
        }
    }
}

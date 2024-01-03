using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourService.Dto;
using TourService.Migrations;
using TourService.Model;
using TourService.Services;

namespace TourService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITour _tourService;
        private readonly ResponseDto _responseDto;
        public TourController(IMapper mapper,ITour tourService)
        {
            _mapper = mapper;
            _tourService = tourService;
            _responseDto = new ResponseDto();
        }
        [HttpPost("create Tour")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> createTour(TourDto tourDto)
        {
            try
            {
                var tour = _mapper.Map<Tour>(tourDto);
                string resp = await   _tourService.createAtour(tour);
                _responseDto.message = resp;
                _responseDto.result = tour;
                return Ok(_responseDto);
            }catch(Exception ex)
            {
                _responseDto.errorMessage = ex.Message;
                return BadRequest(_responseDto);
            }
        }
        [HttpGet("Get All Tours")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> getAllTours()
        {
            try
            {
               var tours = await _tourService.getTours();
                _responseDto.result = tours;
                _responseDto.message = "success";
                return Ok(_responseDto);
            }catch(Exception ex)
            {
                _responseDto.errorMessage = ex.Message;
                _responseDto.result = null;
                return BadRequest(_responseDto);
            }
        }
        [HttpGet("{Id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> getOneTour(Guid Id)
        {
            try
            {
                Tour tour = await _tourService.getTour(Id);
                _responseDto.result = tour;
                _responseDto.message = "success";
                return Ok(_responseDto);
            }catch(Exception ex)
            {
                _responseDto.errorMessage=ex.Message;
                return BadRequest(_responseDto);
            }
        }
        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> deleteTour(Guid Id)
        {
            try
            {
                var tour = await _tourService.getTour(Id);  
                //var tourVal = _mapper.Map<Tour>(tour);
                string resp = await _tourService.deleteTour(tour);
                _responseDto.message = resp;
                _responseDto.result = await _tourService.getTours();
                return Ok(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.errorMessage = ex.Message;
                return BadRequest(_responseDto);
            }
        }
        [HttpPut("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ResponseDto>> updateTour(Guid Id, TourDto tourDto)
        {
            try
            {
                var tour = await  _tourService.getTour(Id);
                //CHECK IF TOUR exist
                if (tour != null)
                {
                    //update with new values
                    var newTour = _mapper.Map(tourDto, tour);
                    string resp = await _tourService.updateTour(newTour);
                    _responseDto.message = resp;
                    _responseDto.result = tour;
                    return Ok(_responseDto);
                }
                _responseDto.errorMessage = "Tour does not exist";
                return BadRequest(_responseDto);
            }
            catch (Exception ex)
            {
                _responseDto.errorMessage = ex.Message;
                return BadRequest(_responseDto);
            }
        }
   
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourService.Dto;
using TourService.Model;
using TourService.Services;

namespace TourService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourImageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITourImage _tourImageService;
        private readonly ITour _tourService;
        private readonly ResponseDto _responseDto;
        public TourImageController(IMapper mapper, ITourImage tourImageService,ITour tourService)
        {
            _mapper = mapper;
            _tourImageService = tourImageService;
            _tourService = tourService;
            _responseDto = new ResponseDto();
        }
        [HttpPost("Add Image Tour {Id}")]
        public async Task<ActionResult<ResponseDto>> addImageTour(Guid Id,AddTourImageDto tourImageDto)
        {
            try
            {
                var tour = await _tourService.getTour(Id);
                if(tour != null)
                {
                    var tourImage = _mapper.Map<TourImages>(tourImageDto);
                    string resp = await _tourImageService.AddImage(Id, tourImage);
                    _responseDto.message = resp;
                    _responseDto.result = tourImageDto;
                    return Created("",_responseDto);
                }
                _responseDto.errorMessage = "Tour not found";
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

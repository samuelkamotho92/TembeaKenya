using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourService.Dto;
using TourService.Services;

namespace TourService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourImageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITourImage _tourImageService;
        public TourImageController(IMapper mapper, ITourImage tourImageService)
        {
            _mapper = mapper;
            _tourImageService = tourImageService;
        }
        [HttpPost("Add Image Tour")]
        public async Task<ActionResult<TourImagesRespDto>> addImageTour()
        {

        }
    }
}

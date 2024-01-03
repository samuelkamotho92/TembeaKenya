using AutoMapper;
using TourService.Dto;
using TourService.Model;

namespace TourService.Profiles
{
    public class TourProfile : Profile
    {
        public TourProfile()
        {
            CreateMap<TourDto, Tour>().ReverseMap();
            CreateMap<AddTourImageDto, TourImages>().ReverseMap();
        }
    }
}

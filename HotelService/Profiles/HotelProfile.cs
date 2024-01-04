using AutoMapper;
using HotelService.Dto;
using HotelService.Models;

namespace HotelService.Profiles
{
    public class HotelProfile:Profile
    {
        public HotelProfile()
        {
           CreateMap<HotelDto, Hotel>().ReverseMap();
            CreateMap<HotelResponseDto, Hotel>().ReverseMap();
        }
    }
}

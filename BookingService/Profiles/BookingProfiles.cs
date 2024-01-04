using AutoMapper;
using BookingService.Dto;
using BookingService.Models;

namespace BookingService.Profiles
{
    public class BookingProfiles:Profile
    {
        public BookingProfiles()
        {
            CreateMap<BookingDto, Booking>().ReverseMap();
        }
    }
}

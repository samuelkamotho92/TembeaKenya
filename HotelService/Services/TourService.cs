using AutoMapper;
using HotelService.Dto;
using HotelService.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace HotelService.Services
{
    public class TourService : ITour
    {
        private readonly IHttpClientFactory _HttpClientFactory;
        private readonly IMapper _mapper;
        public TourService(IHttpClientFactory httpClientFactory)
        {
            _HttpClientFactory = httpClientFactory;
        }
        public async Task<TourDto> GetTourById(Guid Id)
        {
            var client = _HttpClientFactory.CreateClient("Tours");
            var response = await client.GetAsync($"{Id}");
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TourDto>(Convert.ToString(responseDto.result));
            }
            //get a tour with that id
            return new TourDto();
        }
    }
}

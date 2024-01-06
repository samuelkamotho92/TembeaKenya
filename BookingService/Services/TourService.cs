using BookingService.Dto;
using BookingService.Services.IService;
using Newtonsoft.Json;

namespace BookingService.Services
{
    public class TourService : ITour
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TourService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<TourDto> GetATour(Guid guid)
        {
            var client = _httpClientFactory.CreateClient("Tours");
            var response = await client.GetAsync(guid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto?.result != null && response.IsSuccessStatusCode)
            {
                Console.WriteLine(responseDto.result);
                return JsonConvert.DeserializeObject<TourDto>(responseDto.result.ToString());
            }
            return new TourDto();
        }
    }
}

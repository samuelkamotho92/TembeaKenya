using BookingService.Dto;
using BookingService.Services.IService;
using Newtonsoft.Json;

namespace BookingService.Services
{
    public class UserService : IUser
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<UserDto> GetUserById(Guid Id)
        {
            var client = _httpClientFactory.CreateClient("Users");
            var response = await client.GetAsync(Id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            if (responseDto.result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UserDto>(responseDto.result.ToString());
            }
            return new UserDto();
        }
    }
}

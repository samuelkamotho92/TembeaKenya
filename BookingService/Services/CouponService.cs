using BookingService.Dto;
using BookingService.Services.IService;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace BookingService.Services
{
    public class CouponService : ICoupon
    {
        private readonly IHttpClientFactory _httpClientFactory;
        
        public CouponService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            
        }
        public async Task<CouponDto> GetCouponByCode(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupons");
            Console.WriteLine(client);
            var response = await client.GetAsync(couponCode);
            var content = await response.Content.ReadAsStringAsync();
            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);
            Console.WriteLine(responseDto.result);
            if (responseDto?.result != null && response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CouponDto>(responseDto.result.ToString());
            }
            return new CouponDto();
        }
    }
}

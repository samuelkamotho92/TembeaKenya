using System.Net;

namespace Coupon_Service.Dtos
{
    public class ResponseDto
    {
        public object result { get; set; }

        public string message { get; set; }

        public HttpStatusCode statusCode { get; set; }

        public string errorMessage { get; set; }

    }
}

﻿using System.Net;

namespace BookingService.Dto
{
    public class ResponseDto
    {
        public string message { get; set; }

        public HttpStatusCode statusCode { get; set; }

        public string errorMessage { get; set; }

        public object result { get; set; }
    }
}

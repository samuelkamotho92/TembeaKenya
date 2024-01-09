namespace BookingService.Dto
{
    public class StripeRequestDto
    {
        public Guid BookingId { get; set; }

        public string ApprovedUrl { get; set; }

        public string CancelUrl { get; set; }

        public string? StripeSessionId { get; set; }

        //page where clients are redirected to pay
        public string? StripeSessionUrl { get;set; }
    }
}

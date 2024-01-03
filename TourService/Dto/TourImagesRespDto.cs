namespace TourService.Dto
{
    public class TourImagesRespDto
    {
        public Guid Guid { get; set; }
        public string TourName { get; set; } = string.Empty;

        public string TourDescription { get; set; } = string.Empty;

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public int price { get; set; }
        public List<AddTourImageDto> TourImages { get; set; }
    }
}

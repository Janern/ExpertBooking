using BusinessModels;

namespace WebSite.Models
{
    public class BookingResultModel
    {
        public Booking Booking { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage {get; set;}
    }
}
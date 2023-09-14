namespace WebSite.Models
{
    public class BookingInputModel
    {
        public string SelectedExpertsJson { get; set; }
        public string BookerEmailAddress { get; set; }
        public string TimePeriod { get; set; }
        public string Description { get; set; }
    }
}
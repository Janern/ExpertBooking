namespace WebSite.Models
{
    public class BookingInputModel
    {
        public List<string> SelectedExpertIds { get; set; }
        public string BookerEmailAddress { get; set; }
        public string TechnologyFilter { get; set;}
        public string TimePeriod { get; set; }
        public string Description { get; set; }
    }
}
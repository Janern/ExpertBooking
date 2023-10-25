namespace BusinessModels
{
    public class Booking
    {
        public string BookerEmailAddress { get; set; }
        public string[] ExpertIds { get; set;}
        public string TimePeriod { get; set; }
        public string Description { get; set; }
    }
}
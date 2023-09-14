namespace BusinessModels
{
    public class Booking
    {
        public string BookerEmailAddress { get; set; }
        public Expert[] Experts { get; set;}
        public string TimePeriod { get; set; }
        public string Description { get; set; }
    }
}
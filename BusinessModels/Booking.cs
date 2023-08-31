using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessModels
{
    public class Booking
    {
        public string BookerEmailAddress { get; set; }
        public string ExpertType { get; set; }
        public string ExpertRole { get; set; }
        public string TimePeriod { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
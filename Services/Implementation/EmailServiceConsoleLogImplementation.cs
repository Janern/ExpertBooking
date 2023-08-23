using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessModels;

namespace Services.Implementation
{
    public class EmailServiceConsoleLogImplementation : EmailService
    {
        public async Task<bool> SendEmail(Booking booking)
        {
            Console.WriteLine("Email sent to " + booking.BookerEmailAddress);
            await Task.CompletedTask;
            return true;
        }
    }
}
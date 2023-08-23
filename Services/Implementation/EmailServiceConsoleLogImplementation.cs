using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class EmailServiceConsoleLogImplementation : EmailService
    {
        public async Task<bool> SendEmail()
        {
            Console.WriteLine("Email sent");
            await Task.CompletedTask;
            return true;
        }
    }
}
using System.Text.Json;
using BusinessModels;
using WebSite.Models;

namespace WebSite.Helpers
{
    public static class BookingInputModelConverter
    {
        public static Booking Convert(BookingInputModel inputModel)
        {
            Expert[] expertsDeserialized = JsonSerializer.Deserialize<Expert[]>(inputModel.SelectedExpertsJson??"[]");
            return new Booking
            {
                Experts = expertsDeserialized,
                BookerEmailAddress = inputModel.BookerEmailAddress,
                TimePeriod = inputModel.TimePeriod,
                Description = inputModel.Description
            };
        }
    }
}
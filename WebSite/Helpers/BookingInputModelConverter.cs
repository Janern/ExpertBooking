using System.Text.Json;
using BusinessModels;
using WebSite.Models;

namespace WebSite.Helpers
{
    public static class BookingInputModelConverter
    {
        public static Booking Convert(BookingInputModel inputModel)
        {
            Expert[] expertsDeserialized = JsonSerializer.Deserialize<Expert[]>(inputModel.ExpertsJson);
            return new Booking
            {
                Experts = expertsDeserialized
            };
        }
    }
}
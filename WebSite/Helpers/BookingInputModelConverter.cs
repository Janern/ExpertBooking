using BusinessModels;
using WebSite.Models;

namespace WebSite.Helpers
{
    public static class BookingInputModelConverter
    {
        public static Booking Convert(BookingInputModel inputModel)
        {
            return new Booking
            {
                ExpertNames = inputModel.SelectedExpertIds.ToArray(),
                BookerEmailAddress = inputModel.BookerEmailAddress,
                TimePeriod = inputModel.TimePeriod,
                Description = inputModel.Description
            };
        }
    }
}
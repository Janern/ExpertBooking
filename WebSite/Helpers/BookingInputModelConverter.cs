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
                ExpertIds = inputModel.SelectedExpertIds.ToArray(),
                BookerEmailAddress = inputModel.BookerEmailAddress,
                TimePeriod = inputModel.TimePeriod,
                Description = inputModel.Description
            };
        }
    }
}
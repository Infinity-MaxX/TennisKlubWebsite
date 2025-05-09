using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisLibrary.Models;
using TennisLibrary.Services;

namespace TennisWebsite.Pages.BookingPages
{
    public class GetBookingsModel : PageModel
    {
        private BookingService bs;
        public List<Booking> bookings;
        public GetBookingsModel()
        {
            bs = new BookingService();
        }
        public async Task OnGetAsync()
        {
            bookings = await bs.GetAllBookingsAsync();
        }
    }
}

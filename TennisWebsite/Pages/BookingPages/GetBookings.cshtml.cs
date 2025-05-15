using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisLibrary.Models;
using TennisLibrary.Services;

namespace TennisWebsite.Pages.BookingPages
{
    public class GetBookingsModel : PageModel
    {
        private BookingService bs;
        private UserService us;
        public List<Booking> bookingsP1, bookingsP2;
        public GetBookingsModel()
        {
            bs = new BookingService();
        }
        public async Task OnGetAsync()
        {
            bookingsP1 = await bs.GetBookingsByUserAsync(HttpContext.Session.GetString("Username"));
            bookingsP2 = await bs.GetBookingsByPlayer2Async(HttpContext.Session.GetString("Username"));
        }
    }
}

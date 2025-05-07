using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisLibrary.Models;
using TennisLibrary.Services;

namespace TennisWebsite.Pages.BookingPages
{
    public class BookingModel : PageModel
    {
        [BindProperty]
        public Booking booking { get; set; }
        private BookingService bs;
        private CourtService cs;
        private UserService us;

        public BookingModel()
        {
            booking = new Booking();
            bs = new BookingService();
            cs = new CourtService();
            us = new UserService();
        }

        public async Task OnGetAsync(DateTime time, string court)
        {
            booking.Court = await cs.GetCourtAsync(court);
            booking.Start = time;
            booking.End = time.AddHours(1);
        }
    }
}

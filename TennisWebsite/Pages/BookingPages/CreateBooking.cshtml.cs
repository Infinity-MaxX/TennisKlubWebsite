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

        public async Task OnGetAsync(string time, string court)
        {
            DateTime tempTime = DateTime.Parse(time);
            booking.Court = await cs.GetCourtAsync(court);
            booking.Start = tempTime;
            booking.End = tempTime.AddHours(1);
        }
    }
}

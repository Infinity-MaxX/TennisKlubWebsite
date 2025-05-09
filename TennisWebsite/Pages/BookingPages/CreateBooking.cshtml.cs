using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisLibrary.Models;
using TennisLibrary.Services;

namespace TennisWebsite.Pages.BookingPages
{
    public class CreateBookingModel : PageModel
    {
        [BindProperty]
        public Booking booking { get; set; }

        [BindProperty]
        public string player1 { get; set; }
        
        [BindProperty]
        public string player2 { get; set; }

        private BookingService bs;
        private CourtService cs;
        private UserService us;

        public CreateBookingModel()
        {
            booking = new Booking();
            bs = new BookingService();
            cs = new CourtService();
            us = new UserService();
        }

        public async Task OnGetAsync(string time, string court)
        {
            booking = new Booking();
            bs = new BookingService();
            cs = new CourtService();
            us = new UserService();
            DateTime tempTime = DateTime.Parse(time);
            booking.Court = await cs.GetCourtAsync(court);
            booking.Start = tempTime;
            booking.End = tempTime.AddHours(1);
            player1 = HttpContext.Session.GetString("Username");
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (player1 != null && player2 != null && booking.Court != null && booking.Start >= DateTime.Now && booking.End > DateTime.Now)
            {
                User Player1 = await us.GetUserAsAdminAsync(player1);
                User Player2 = await us.GetUserAsAdminAsync(player2);
                Booking newBooking = new Booking(Player1, Player2, booking.Court, booking.Start, booking.End);
                await bs.AddBookingUserAsync(newBooking);
                return RedirectToPage("/index");
            }
            return RedirectToPage("CreateBooking", new { time = booking.Start.ToString(), court = booking.Court.Name });
        }
    }
}

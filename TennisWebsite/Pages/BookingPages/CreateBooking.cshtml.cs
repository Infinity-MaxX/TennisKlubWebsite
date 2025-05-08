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

        [BindProperty]
        public string player1 { get; set; }
        
        [BindProperty]
        public string player2 { get; set; }

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

        public async Task<IActionResult> OnPostCreateAsync()
        {

            //if (/*player1 != null && player2 != null && court != null /*&& start >= DateTime.Now && end > start*/ 1 == 1)
            //{
            //    return RedirectToPage("/index");
            //}
            //return Page();
            return Page(booking.Start, booking.Court);
        }
    }
}

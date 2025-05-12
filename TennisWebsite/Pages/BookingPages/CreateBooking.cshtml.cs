    using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using TennisLibrary.Helpers;
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

        private User Player1;


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
            bs= new BookingService();
            cs = new CourtService();
            booking= new Booking();
            us =new UserService();
            User Player1 = await us.GetUserAsAdminAsync(HttpContext.Session.GetString("Username"));

            if (time.IsNullOrEmpty())
            {
                if (Player1.AccessLevel >= AccessLevel.Admin)
                {
                    Console.WriteLine("Is null!");
                }
                else
                {
                    RedirectToPage("/index");
                }
            }

            else
            {
                DateTime tempTime = DateTime.Parse(time);
                booking.Court = await cs.GetCourtAsync(court);
                booking.Start = tempTime;
                booking.End = tempTime.AddHours(1);
                
                player1 = Player1.Name + " (" + Player1.Username + ")";
            }
            
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {

            if (player2 != null && booking.Court != null && booking.Start >= DateTime.Now && booking.End > booking.Start)
            {
                string player1UN = player1.Split('(')[1];
                User Player1 = await us.GetUserAsAdminAsync(player1UN.Split(')')[0]);
                User Player2 = await us.GetUserAsAdminAsync(player2);
                Booking newBooking = new Booking(Player1, Player2, booking.Court, booking.Start, booking.End);
                try
                {
                    await bs.AddBookingUserAsync(newBooking);
                }
                catch
                {
                    return RedirectToPage("CreateBooking", new { time = booking.Start.ToString(), court = booking.Court.Name });
                }
                await bs.AddBookingUserAsync(newBooking);
                return RedirectToPage("/index");
            }
            return RedirectToPage("CreateBooking", new { time = booking.Start.ToString(), court = booking.Court.Name });
        }
    }
}

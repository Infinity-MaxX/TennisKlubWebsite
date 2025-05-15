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

        public bool Admin = false;
        public int Error = 0;
        


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
            Admin = false;
            Error = 0;

            if (Player1 != null)
            {
                Console.WriteLine("not null");
                if (time.IsNullOrEmpty())
                {
                    if (Player1.AccessLevel >= AccessLevel.Admin)
                    {
                        booking.Start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
                        booking.End = booking.Start.AddHours(1);
                        booking.Court = await cs.GetCourtAsync(court);
                        Admin = true;
                    }
                    else
                    {
                        Error = 1;
                    }
                }

                else
                {
                    DateTime tempTime = DateTime.Parse(time);
                    booking.Court = await cs.GetCourtAsync(court);
                    booking.Start = tempTime;
                    booking.End = tempTime.AddHours(1);
                    player1 = Player1.Name + " (" + Player1.Username + ")";
                    Admin = false;
                    Page();
                }
            }
            else
            {
                Error = 2;
            }
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {

            if (player2 != null && booking.Court != null && booking.Start >= DateTime.Now && booking.End > booking.Start)
            {
                string player1UN = player1.Split('(')[1];
                User Player1 = await us.GetUserAsAdminAsync(player1UN.Split(')')[0]);
                User Player2 = await us.GetUserAsAdminAsync(player2);
                if (Player1 == null || Player2 == null)
                {
                    return Page();
                }
                if (Player1.Username == Player2.Username)
                {
                    return Page();
                }
                
                Booking newBooking = new Booking(Player1, Player2, booking.Court, booking.Start, booking.End);
                try
                {
                    await bs.AddBookingUserAsync(newBooking);
                }
                catch
                {
                    return RedirectToPage("CreateBooking", new { time = booking.Start.ToString(), court = booking.Court.Name });
                }
                return RedirectToPage("/index");
            }
            return RedirectToPage("CreateBooking", new { time = booking.Start.ToString(), court = booking.Court.Name });
        }
    }
}

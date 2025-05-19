    using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using TennisLibrary.Helpers;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;
using TennisLibrary.Services;
using TennisWebsite.ClassLibrary.Helpers;

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
        
        public List<User> Users = new List<User>();
        public List<Court> Courts = new List<Court>();

        private IBookingService bs;
        private ICourtService cs;
        private IUserService us;

        

        public CreateBookingModel(IBookingService bookingService, ICourtService courtService, IUserService userService)
        {
            booking = new Booking();
            bs = bookingService;
            cs = courtService;
            us = userService;
        }

        public async Task<IActionResult> OnGetUpdateAsync(string query)
        {
            Users = await us.GetAllUsersAsync();
            if (String.IsNullOrEmpty(query)) return new JsonResult(DLStringComparer<User>.ConvertIfNoQuery(Users, x => x.Name));

            return new JsonResult(DLStringComparer<User>.Matches(Users, x => x.Name, query));
        }

        public async Task OnGetAsync(string time, string court)
        {
            
            bs= new BookingService();
            cs = new CourtService();
            booking= new Booking();
            us =new UserService();
            Courts = await cs.GetAllCourts();
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

                List<Booking> bookingsP1 = await bs.GetBookingsByDatesAndUserAsync(Player1.Username, DateTime.Now.AddHours(1), DateTime.Now.AddYears(1));
                List<Booking> bookingsP2 = await bs.GetBookingsByDatesAndUser2Async(Player2.Username, DateTime.Now.AddHours(1), DateTime.Now.AddYears(1));
                Console.WriteLine("P1 bookings: " + bookingsP1.Count);
                Console.WriteLine("P2 bookings: " + bookingsP2.Count);
                if (bookingsP1.Count >= 4)
                {
                    Console.WriteLine("P1 too many bookings");
                    return Page();
                }
                else if (bookingsP2.Count >= 4)
                {
                    Console.WriteLine("P2 too many bookings");
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

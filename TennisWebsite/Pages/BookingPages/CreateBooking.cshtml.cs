    using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
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

        [BindProperty]
        public string bookingType { get; set; }

        [BindProperty]
        public bool Admin { get; set; }


        private bool ErrorHappened;

        
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
            Courts = await cs.GetAllCourtsAsync();
            User Player1 = await us.GetUserAsAdminAsync(HttpContext.Session.GetString("Username"));
            Admin = false;
            Error = 0;

            if (Player1 != null)
            {
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
                    booking.Type = "Booking";
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
            ModelState.ClearValidationState("player1");
            ModelState.ClearValidationState("player2");
            Console.WriteLine(player2);
            ErrorHappened = false;
            if (player2 != null && booking.Court != null && booking.Start >= DateTime.Now && booking.End > booking.Start)
            {
                User Player1;
                if (Admin == false)
                {
                    string player1UN = player1.Split('(')[1];
                    Player1 = await us.GetUserAsAdminAsync(player1UN.Split(')')[0]);
                }
                else
                {
                    Player1 = await us.GetUserAsAdminAsync(player1);
                }
                User Player2 = await us.GetUserAsAdminAsync(player2);
                
                if (Player1 == null)
                {
                    //return error: no player
                    ModelState.AddModelError("player1", "Vælg en rigtig bruger");
                    ErrorHappened = true;
                }
                
                if (Player2 == null)
                {
                    //return error: no player
                    ModelState.AddModelError("player2", "Vælg en rigtig bruger");
                    ErrorHappened = true;
                }

                if (Player1.Username == Player2.Username)
                {
                    // return error: same players
                    ModelState.AddModelError("player1", "De to brugere er de samme");
                    ModelState.AddModelError("player2", "De to brugere er de samme");
                    ErrorHappened = true;
                }
                if (ErrorHappened == true)
                {
                    return Page();
                }
                List<Booking> bookingsP1 = await bs.GetBookingsByTimePlayerAndType(Player1.Username, DateTime.Now.AddHours(1), DateTime.Now.AddYears(1), "Booking");
                List<Booking> bookingsP2 = await bs.GetBookingsByTimePlayer2AndType(Player1.Username, DateTime.Now.AddHours(1), DateTime.Now.AddYears(1), "Booking");

                if (bookingsP1.Count >= 4 && booking.Type == "Booking")
                {
                    // return error, too many bookings
                    ModelState.AddModelError("player1", "Du har allerede 4 bookinger");
                    ErrorHappened = true;
                }
                if (bookingsP2.Count >= 4 && booking.Type == "Booking")
                {
                    ModelState.AddModelError("player2", "Spilleren har allerede 4 bookinger");
                    ErrorHappened = true;
                }

                if (booking.Start < DateTime.Now)
                {
                    ModelState.AddModelError("booking.Start","Start tiden er i fortiden");
                    ErrorHappened = true;
                }

                if (booking.End < booking.Start)
                {
                    ModelState.AddModelError("booking.End", "Slut tiden er tidligere end start tiden");
                    ErrorHappened = true;
                }

                if (ErrorHappened == true)
                {
                    return Page();
                }

                Booking newBooking = new Booking(Player1, Player2, booking.Court, booking.Start, booking.End, booking.Type);
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
            if (Admin == false)
            {
                return RedirectToPage("CreateBooking", new { time = booking.Start.ToString(), court = booking.Court.Name });
            }
            else
            {
                return RedirectToPage("CreateBooking", new {time = "", court = booking.Court.Name });
            }
        }
    }
}

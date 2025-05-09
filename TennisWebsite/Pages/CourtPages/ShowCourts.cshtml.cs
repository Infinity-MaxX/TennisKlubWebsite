using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisLibrary.Models;
using TennisLibrary.Services;

namespace TennisWebsite.Pages.CourtPages
{
    public class ShowCourtsModel : PageModel
    {
        private CourtService cs;
        private BookingService bs;

        public List<Court> courts { get; private set; }
        public List<string> bookedCourtTimes { get; private set; }

        private List<Booking> bookings { get; set; }


        [BindProperty]
        public DateOnly date{ get; set;}


        public ShowCourtsModel()
        {
            courts = new List<Court>();
            bookedCourtTimes = new List<string>();
            bs = new BookingService();
            cs = new CourtService();
            date = new DateOnly();
        }

        public async Task OnGetAsync()
        {
            courts = await cs.GetAllCourts();
            date = DateOnly.FromDateTime(DateTime.Now);
            bookings = await bs.GetBookingsByDatesAsync(date.ToDateTime(new TimeOnly()), date.ToDateTime(new TimeOnly()).AddDays(1));
            foreach (Booking b in bookings)
            {
                bookedCourtTimes.Add(b.Court.Name + b.Start.TimeOfDay.Hours);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            courts = await cs.GetAllCourts();
            bookings = await bs.GetBookingsByDatesAsync(date.ToDateTime(new TimeOnly()), date.ToDateTime(new TimeOnly()).AddDays(1));
            foreach (Booking b in bookings)
            {
                bookedCourtTimes.Add(b.Court.Name + b.Start.TimeOfDay.Hours);
            }
            return Page();
        }
    }
}

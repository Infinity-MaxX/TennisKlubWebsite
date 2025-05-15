using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisLibrary.Models;
using TennisLibrary.Services;

namespace TennisWebsite.Pages.CourtPages
{
    public class EditCourtModel : PageModel
    {


        [BindProperty]
        public Court newCourt { get; set; }

        [BindProperty]
        public string OldCourt { get; set; }

        private CourtService cs;

        public EditCourtModel()
        {
            cs = new CourtService();
            newCourt = new Court();
        }

        public async Task<IActionResult> OnGetAsync(string courtName)
        {
            newCourt = await cs.GetCourtAsync(courtName);
            newCourt.LastMaintenance = DateOnly.FromDateTime(DateTime.Now);
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToPage("/Users/Login");
            }
            OldCourt = newCourt.Name;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await cs.UpdateCourtAsync(OldCourt, newCourt.Name, newCourt.LastMaintenance);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("NewCourt.Name", ex.Message);
                return Page();
            }
            return RedirectToPage("ShowCourts");
        }

    }
}

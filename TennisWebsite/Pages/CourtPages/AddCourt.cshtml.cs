using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisLibrary.Models;
using TennisLibrary.Services;
namespace TennisWebsite.Pages.CourtPages
{
    public class AddCourtModel : PageModel
    {
        [BindProperty]
        public Court newCourt {  get; set; }


        private CourtService cs;

        public AddCourtModel()
        {
            cs = new CourtService();
            newCourt = new Court();
        }

        public void OnGet()
        {
            //Access level check
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await cs.CreateCourtAsync(newCourt);
            return RedirectToPage("ShowCourts");
        }
    }
}

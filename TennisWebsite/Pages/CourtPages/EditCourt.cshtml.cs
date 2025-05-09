using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TennisWebsite.Pages.CourtPages
{
    public class EditCourtModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return RedirectToPage("/Users/Login");
            }
            return Page();
        }
    }
}

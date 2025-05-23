using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TennisWebsite.Pages.Users
{
    public class LogOutTerminalModel : PageModel
    {
        public IActionResult OnGet()
        {
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("Displayname");
            HttpContext.Session.Remove("AccessLevel");
            return RedirectToPage("/index");
        }
    }
}

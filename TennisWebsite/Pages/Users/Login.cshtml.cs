using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;
using TennisLibrary.Services;

namespace TennisWebsite.Pages.Users
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public string LoginUsername { get; set; }
        [BindProperty]
        public string LoginPass { get; set; }

        public string LoginStatus { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            IUserService tempService = new UserService();
            User loginUser = await tempService.GetUserLoginAsync(LoginUsername, LoginPass);
            if (loginUser == null)
            {
                LoginStatus = "login failed";
                return Page();
            }

            HttpContext.Session.SetString("Username", loginUser.Username);
            HttpContext.Session.SetString("Displayname", loginUser.Name);
            HttpContext.Session.SetInt32("AccesLevel", (int)loginUser.AccessLevel);

            return RedirectToPage("/index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisLibrary.Helpers;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;
using TennisLibrary.Services;

namespace TennisWebsite.Pages.Users
{
    public class LoginModel : PageModel
    {
        IUserService _userService;

        [BindProperty]
        public string LoginUsername { get; set; }
        [BindProperty]
        public string LoginPass { get; set; }

        public string LoginStatus { get; set; }

        public string OriginalDestination { get; private set; }

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet(string OriginalDestination)
        {
            this.OriginalDestination = OriginalDestination;
        }

        public async Task<IActionResult> OnPostAsync(string OriginalDestination)
        {
            User loginUser = await _userService.GetUserLoginAsync(LoginUsername, LoginPass);
            if (loginUser == null)
            {
                LoginStatus = "login failed";
                return Page();
            }

            HttpContext.Session.SetString("Username", loginUser.Username);
            HttpContext.Session.SetString("Displayname", loginUser.Name);
            HttpContext.Session.SetInt32("AccessLevel", (int)loginUser.AccessLevel);

            if(String.IsNullOrEmpty(OriginalDestination)) return RedirectToPage("/index");
            return RedirectToPage(OriginalDestination);
        }
    }
}

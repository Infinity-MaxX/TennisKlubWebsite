using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisLibrary.Models;
using TennisLibrary.Interfaces;
using TennisLibrary.Services;
using TennisLibrary.Helpers;
using TennisWebsite.ClassLibrary.Helpers;

namespace TennisWebsite.Pages.Users
{
    public class ShowAllUsersModel : PageModel
    {
        private IUserService _userService;
        private const int _siteAccesLevel = (int)AccessLevel.Admin;

        [BindProperty]
        public string SelectedUsername { get; set; }

        public List<User> Users { get; set; }

        public ShowAllUsersModel()
        {
            _userService = new UserService();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            int? AccessLevel = HttpContext.Session.GetInt32("AccessLevel");
            if (AccessLevel == null) return RedirectToPage("Login", new { OriginalDestination = "/Users/ShowAllUsers" });
            if(AccessLevel >= _siteAccesLevel)
            {
                Users = await _userService.GetAllUsersAsync();
                return Page();

            }
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetUpdateAsync(string query, int minAge, int maxAge)
        {
            Users = await _userService.GetAllUsersFilteredAsync(['a','m','k'], minAge, maxAge);
            if (String.IsNullOrEmpty(query)) return new JsonResult(DLStringComparer<User>.ConvertIfNoQuery(Users, x => x.Name));

            return new JsonResult(DLStringComparer<User>.Matches(Users, x => x.Name, query));
        }

        public async Task<IActionResult> OnPostDeleteAsync(string Username)
        {
            await _userService.DeleteUserAsync(Username);
            return RedirectToPage("ShowAllUsers");
        }
    }
}

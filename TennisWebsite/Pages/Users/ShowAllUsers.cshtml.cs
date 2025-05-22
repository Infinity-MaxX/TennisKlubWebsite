using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisLibrary.Helpers;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;
using TennisLibrary.Services;
using TennisWebsite.ClassLibrary.Helpers;
using static System.Net.Mime.MediaTypeNames;

namespace TennisWebsite.Pages.Users
{
    public class ShowAllUsersModel : PageModel
    {
        private IUserService _userService;
        private const int _siteAccesLevel = (int)AccessLevel.Admin;

        [BindProperty]
        public string SelectedUsername { get; set; }

        public List<User> Users { get; set; }

        public (char, string)[] Genders = new[] { ('M', "Mænd"), ('K', "Kvinder"), ('A', "Andre") };
        public bool[] FilteredGendersCheck = {true, true, true};
        public List<char> FilteredGendersInput = new List<char>();

        public ShowAllUsersModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            int? AccessLevel = HttpContext.Session.GetInt32("AccessLevel");
            if (AccessLevel == null) return RedirectToPage("Login", new { OriginalDestination = "/Users/ShowAllUsers" });
            if(AccessLevel >= _siteAccesLevel)
            {
                Users = await _userService.GetAllUsersFilteredAsync(['a', 'm', 'k'], null, null);
                return Page();

            }
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetUpdateAsync(string query, string genderInput, int minAge, int maxAge)
        {
            if(!String.IsNullOrEmpty(genderInput))
            {
                foreach(char c in genderInput)
                {
                    FilteredGendersInput.Add(c);
                }
            }
            Users = await _userService.GetAllUsersFilteredAsync(FilteredGendersInput, minAge, maxAge);
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

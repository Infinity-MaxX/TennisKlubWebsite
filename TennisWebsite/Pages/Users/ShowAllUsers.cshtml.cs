using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisLibrary.Models;
using TennisLibrary.Interfaces;
using TennisLibrary.Services;

namespace TennisWebsite.Pages.Users
{
    public class ShowAllUsersModel : PageModel
    {
        private IUserService _userService;

        public List<User> Users { get; set; }

        public ShowAllUsersModel()
        {
            _userService = new UserService();
        }

        public async Task OnGetAsync()
        {
            Users = await _userService.GetAllUsersAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string Username)
        {
            await _userService.DeleteUserAsync(Username);
            return RedirectToPage("ShowAllUsers");
        }
    }
}

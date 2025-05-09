using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisLibrary.Helpers;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;
using TennisLibrary.Services;
using TennisWebsite.ClassLibrary.Helpers;

namespace TennisWebsite.Pages.Users
{
    public class RegisterNewUserModel : PageModel
    {
        private IWebHostEnvironment _webHostEnvironment;
        private IUserService _userService;

        #region Userdata
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public char Gender { get; set; }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Phone { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Address { get; set; }

        [BindProperty]
        public string HomeMunicipality { get; set; }

        [BindProperty]
        public DateOnly BirthDate { get; set; }

        [BindProperty]
        public AccessLevel AccessLevel { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }
        #endregion

        #region MiscData

        public string UsernameMessage { get; set; }

        public int? SessionAccessLevel { get; private set; }

        public (char, string)[] Genders = new[] { ('M', "Mand"), ('K', "Kvinde"), ('A', "Andet") };
        #endregion

        public RegisterNewUserModel(IWebHostEnvironment webHostEnvironment, IUserService userService)
        {
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
        }

        public void OnGet()
        {


        }

        public async Task<IActionResult> OnPost()
        {
            string imagePath = Defaults.DefaultImage;
            if(Image != null) imagePath = FileManager.SaveImageFile(Image, _webHostEnvironment);

            User newUser = new User(imagePath, Name, Gender, Username, Phone, Email, Address, HomeMunicipality, BirthDate);

            await _userService.AddUserAsync(newUser, Password);
            return RedirectToPage("/Index");
        }

    }
}

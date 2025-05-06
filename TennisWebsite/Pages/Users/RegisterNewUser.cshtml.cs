using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisLibrary.Helpers;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;
using TennisLibrary.Services;

namespace TennisWebsite.Pages.Users
{
    public class RegisterNewUserModel : PageModel
    {
        private IWebHostEnvironment _webHostEnvironment;

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

        public RegisterNewUserModel(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void OnGet()
        {
            SessionAccessLevel = HttpContext.Session.GetInt32("AccessLevel");


        }

        public async Task<IActionResult> OnPost()
        {
            IUserService tempUserService = new UserService();
            string imagePath = "DefaultUser.jpg";
            if(Image != null) imagePath = SaveImageFile(Image);

            User newUser = new User(imagePath, Name, Gender, Username, Phone, Email, Address, HomeMunicipality, BirthDate);

            await tempUserService.AddUserAsync(newUser, Password);
            return Page();
        }

        private string SaveImageFile(IFormFile file)
        {
            if (file == null) throw new NullReferenceException("No file to save");
            string destinationfolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/memberimages");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
            string filePath = Path.Combine(destinationfolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                Image.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

    }
}

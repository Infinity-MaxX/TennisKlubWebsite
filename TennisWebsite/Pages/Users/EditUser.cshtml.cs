using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisLibrary.Helpers;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;
using TennisLibrary.Services;
using TennisWebsite.ClassLibrary.Helpers;

namespace TennisWebsite.Pages.Users
{
    public class EditUserModel : PageModel
    {
        private IUserService _userService;
        private IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public char Gender { get; set; }

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
        public IFormFile Image { get; set; }


        public string? OldImageName { get; set; }
        public string OldUserName { get; set; }

        public bool KeepImage { get; set; }


        public (char, string)[] Genders = { ('M', "Mand"), ('K', "Kvinde"), ('A', "Andet") };

        public EditUserModel(IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> OnGetAsync(string Username)
        {
            KeepImage = true;
            User tempUser = await _userService.GetUserAsAdminAsync(Username);
            if(tempUser != null)
            {
                OldUserName = tempUser.Username;
                Name = tempUser.Name;
                Gender = tempUser.Gender;
                Phone = tempUser.Phone;
                Email = tempUser.Email;
                Address = tempUser.Address;
                HomeMunicipality = tempUser.HomeMunicipality;
                BirthDate = tempUser.BirthDate;

                OldImageName = tempUser.ImageName;
                return Page();
            }
            return RedirectToPage("Login");
        }
        public async Task<IActionResult> OnPostAsync(string OldUserName, string OldImageName)
        {
            if (OldImageName == null || !KeepImage) OldImageName = Defaults.DefaultImage;
            string destinationFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/memberimages");
            string imageName = OldImageName;
            if(Image != null && Image.Length>0 && !KeepImage)
            {
                imageName = FileManager.SaveImageFile(Image, _webHostEnvironment);
            }
            if (await _userService.EditUserAsync(OldUserName, imageName, Name, Gender, Phone, Email, Address, HomeMunicipality, BirthDate))
            {
                //Ideally this should only ever be the path if you are now editing yourself and you are an admin.
                return RedirectToPage("ShowAllUsers");
            }
            return Page();
        }

    }

}

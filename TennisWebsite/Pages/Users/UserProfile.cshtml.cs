using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Numerics;
using System.Reflection;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;
using TennisWebsite.ClassLibrary.Helpers;

namespace TennisWebsite.Pages.Users
{

    public class UserProfileModel : PageModel
    {
        private IUserService _userService;

        #region personal data
        public string Name { get; set; }
        public char Gender { get; set; }
        public DateOnly BirthDate { get; set; }
        #endregion

        #region image
        public string ImageName { get; set; }
        #endregion

        #region Contact information
        public string Phone { get; set; }
        public string Email { get; set; }
        #endregion

        #region System information
        public string Username { get; set; }
        public string AccessName { get; set; }
        #endregion

        #region Address
        public string Address { get; set; }
        public string HomeMunicipality { get; set; }
        #endregion

        #region constructors
        public UserProfileModel(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        public async Task OnGetAsync(string RouteUsername)
        {
            User tempUser = await _userService.GetUserAsAdminAsync(HttpContext.Session.GetString("Username"));
            if (tempUser != null)
            {
                Name = tempUser.Name;
                Gender = tempUser.Gender;
                BirthDate = tempUser.BirthDate;

                ImageName = tempUser.ImageName;

                Phone = tempUser.Phone;
                Email = tempUser.Email;

                Username = tempUser.Username;
                AccessName = tempUser.AccessLevel.ToString();

                Address = tempUser.Address;
                HomeMunicipality = tempUser.HomeMunicipality;

            }
        }
    }
}

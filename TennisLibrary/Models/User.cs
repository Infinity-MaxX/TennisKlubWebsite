using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLibrary.Helpers;

namespace TennisLibrary.Models
{
    public class User
    {
        #region instance fields

        #endregion


        #region constructors

        /// <summary>
        /// The constructor for the user object, a variant also exists without imagePath.
        /// </summary>
        /// <param name="imagePath">The path to the user's portrait as stored in the system</param>
        /// <param name="name">The displayname of the user</param>
        /// <param name="gender">The gender identity of the user, expressed as a single character</param>
        /// <param name="username">The username and unique identifier of the user</param>
        /// <param name="phone">The phone number of the user</param>
        /// <param name="email">The email address of the user</param>
        /// <param name="address">The address (postalnumber, road, house-number) of the user</param>
        /// <param name="homeMunicipality">The municipality that the user lives in</param>
        /// <param name="birthDate">The birthdate, DD/MM/YYYY of the user.</param>
        public User(string imagePath, string name, char gender, string username, string phone, string email, string address, string homeMunicipality, DateOnly birthDate)
        {
            imagePath = imagePath;

            Name = name;
            Gender = gender;
            Username = username;
            Phone = phone;
            Email = email;
            Address = address;
            HomeMunicipality = homeMunicipality;
            BirthDate = birthDate;

            AccessLevel = AccessLevel.Guest; //Can be read as AccessLevel = 0
        }

        /// <summary>
        /// A variant of the constructor for the user object, a variant also exists starting with a string imagePath.
        /// </summary>
        /// <param name="name">The displayname of the user</param>
        /// <param name="gender">The gender identity of the user, expressed as a single character</param>
        /// <param name="username">The username and unique identifier of the user</param>
        /// <param name="phone">The phone number of the user</param>
        /// <param name="email">The email address of the user</param>
        /// <param name="address">The address (postalnumber, road, house-number) of the user</param>
        /// <param name="homeMunicipality">The municipality that the user lives in</param>
        /// <param name="birthDate">The birthdate, DD/MM/YYYY of the user.</param>
        public User(string name, char gender, string username, string phone, string email, string address, string homeMunicipality, DateOnly birthDate)
        {
            Name = name;
            Gender = gender;
            Username = username;
            Phone = phone;
            Email = email;
            Address = address;
            HomeMunicipality = homeMunicipality;
            BirthDate = birthDate;

            AccessLevel = AccessLevel.Guest; //Can be read as AccessLevel = 0
        }

        /// <summary>
        /// A variant of the constructor for the user object. Uses a user object and a password to create a new user object. The new user is identical to the input object, except for the saved password which cannot be extracted from user.
        /// </summary>
        /// <param name="user"></param>
        public User(User user)
        {
            ImagePath = user.ImagePath;

            Name = user.Name;
            Gender = user.Gender;
            Username = user.Username;
            Phone = user.Phone;
            Email = user.Email;
            Address = user.Address;
            HomeMunicipality = user.HomeMunicipality;
            BirthDate = user.BirthDate;

            AccessLevel = AccessLevel.Guest; //Can be read as AccessLevel = 0
        }
        #endregion

        #region properties

        public string Name { get; set; }

        public char Gender { get; set; }

        public string Username { get; private set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string HomeMunicipality { get; set; }

        public DateOnly BirthDate { get; set; }

        public AccessLevel AccessLevel { get; private set; }

        public string? ImagePath { get; set; }
        #endregion


        #region methods



        public override string ToString()
        {
            return $"User: {Name} ({Gender}) {AccessLevel}.\n" +
                $"Contact information: Phone number: {Phone}, Email-address: {Email}, Address {Address}, HomeMunicipality {HomeMunicipality}\n" +
                $"Profile: {Username}";
        }
        #endregion
    }
}

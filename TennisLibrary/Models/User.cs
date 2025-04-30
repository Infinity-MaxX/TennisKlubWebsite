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
        private string _hashPass;
        #endregion


        #region constructors
        public User(string name, char gender, string username, string hashPass, string phone, string email, string address, string homeMunicipality, DateOnly birthDate)
        {
            Name = name;
            Gender = gender;
            Username = username;
            _hashPass = hashPass;
            Phone = phone;
            Email = email;
            Address = address;
            HomeMunicipality = homeMunicipality;
            BirthDate = birthDate;

            AccessLevel = AccessLevel.Guest; //Can be read as AccessLevel = 0
        }

        public User(User user, string hashPass)
        {
            Name = user.Name;
            Gender = user.Gender;
            Username = user.Username;
            _hashPass = hashPass;
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
        #endregion


        #region methods

        public bool CheckPass(string hashPass) //You can ask the user if a given hash matches it's own, but you cannot get direct access to the hash.
        {
            if (hashPass == _hashPass) return true;
            else return false;
        }

        public bool ChangePass(string hashPass, string newHashPass)
        {
            if(CheckPass(hashPass))
            {
                _hashPass = newHashPass;
                return true;
            }
            return false;
        }

        public bool ChangePass(User attemptor, string newHashPass)
        {
            if (attemptor.AccessLevel >= AccessLevel.Admin)
            {
                _hashPass = newHashPass;
                return true;
            }
            return false;
        }

        public bool ChangeAccess(User attemptor, AccessLevel newLevel)
        {
            if (attemptor.AccessLevel >= AccessLevel.Admin)
            {
                AccessLevel = newLevel;
                return true;
            }
            else return false;
        }


        public override string ToString()
        {
            return $"User: {Name} ({Gender}) {AccessLevel}.\n" +
                $"Contact information: Phone number: {Phone}, Email-address: {Email}, Address {Address}, HomeMunicipality {HomeMunicipality}\n" +
                $"Profile: {Username}";
        }
        #endregion
    }
}

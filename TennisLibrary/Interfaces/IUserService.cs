using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLibrary.Models;

namespace TennisLibrary.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddUserAsync(User newUser);
        //Task<bool> AddUserAsync(string name, string gender, string username, string hashPass, string phone, string email, string address, string homeMunicipality, DateOnly birthDate);

        Task<bool> EditUserAsync(string userNameIdent, string newName, string newGender, string newPhone, string newEmail, string newAddress, string newHomeMunicipality, DateOnly newBirthDate);

        Task<bool> DeleteUserAsync(string userNameIdent, out User? discarded);

        Task<List<User>> GetAllUsersAsync();

        Task<User> LoginAsAsync(string userName, string hashPass);
    }
}

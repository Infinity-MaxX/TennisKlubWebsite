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
        //Task<bool> AddUserAsync(User newUser);
        Task<bool> AddUserAsync(User newUser, string password);

        Task<User> GetUserAsAdminAsync(string username);

        Task<User> GetUserLoginAsync(string username, string password);
        Task<List<User>> GetAllUsersAsync();

        Task<List<User>> GetAllUsersFilteredAsync(char[] genders, double? minAge, double? maxAge);

        Task<bool> EditUserAsync(string queryUsername, string newImagePath, string newName, char newGender, string newPhone, string newEmail, string newAddress, string newHomeMunicipality, DateOnly newBirthDate);

        Task<bool> DeleteUserAsync(string queryUsernameIdent);
    }
}

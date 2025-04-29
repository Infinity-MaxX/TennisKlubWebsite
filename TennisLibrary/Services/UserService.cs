using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;

namespace TennisLibrary.Services
{
    public class UserService : IUserService
    {
        public Task<bool> AddUserAsync(User newUser)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> LoginAsAsync(string userName, string hashPass)
        {
            throw new NotImplementedException();
        }
    }
}

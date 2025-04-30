using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;
using Microsoft.Data.SqlClient;

namespace TennisLibrary.Services
{
    public class UserService : IUserService
    {
        private string connectionString = ConnectionManager.ConnectionString;

        private string

            insertQuery = "",
            searchByIdentQuery = "SELECT * From TennisUser Where Username = @username",
            searchAllQuery = "",
            editQuery = "",
            deleteQuery = ""
            ;

        public async Task<bool> AddUserAsync(string name, string gender, string username, string hashPass, string phone, string email, string address, string homeMunicipality, DateOnly birthDate)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand insertCommand;
                }
                catch (SqlException sqlExp)
                {
                    throw sqlExp;
                }
            }
            return false;
        }
        public Task<User> GetUser(string username)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(string usernameIdent, out User? discarded)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditUserAsync(string usernameIdent, string newName, string newGender, string newPhone, string newEmail, string newAddress, string newHomeMunicipality, DateOnly newBirthDate)
        {
            throw new NotImplementedException();
        }
    }
}

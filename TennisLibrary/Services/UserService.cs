using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Data;
using System.IO;
using TennisLibrary.Helpers;

namespace TennisLibrary.Services
{
    public class UserService : IUserService
    {

        private string

            insertQuery = "INSERT into TennisUser Values(@username, @gender, @name, @phone, @email, @address, @homeMunicipality, @birthdate, @accessLevel, @imagePath, @hashPass)",
            searchByIdentQuery = "SELECT * From TennisUser Where Username = @username",
            searchLoginQuery = "SELECT * From TennisUser Where Username = @username AND @password",
            searchAllQuery = "",
            editQuery = "",
            deleteQuery = ""
            ;

        public async Task<bool> AddUserAsync(User newUser, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);

                    insertCommand.Parameters.AddWithValue("@imagePath", newUser.ImagePath);
                    insertCommand.Parameters.AddWithValue("@username", newUser.Username);
                    insertCommand.Parameters.AddWithValue("@gender", newUser.Gender);
                    insertCommand.Parameters.AddWithValue("@name", newUser.Name);
                    insertCommand.Parameters.AddWithValue("@phone", newUser.Phone);
                    insertCommand.Parameters.AddWithValue("@email", newUser.Email);
                    insertCommand.Parameters.AddWithValue("@address", newUser.Address);
                    insertCommand.Parameters.AddWithValue("@homeMunicipality", newUser.HomeMunicipality);
                    insertCommand.Parameters.AddWithValue("@birthDate", newUser.BirthDate);
                    insertCommand.Parameters.AddWithValue("@accessLevel", newUser.AccessLevel);

                    insertCommand.Parameters.AddWithValue("@hashPass", Hasher.CreateHashString(password));

                    return 0 < await insertCommand.ExecuteNonQueryAsync();
                }
                catch (SqlException sqlExp)
                {
                    throw sqlExp;
                }
            }
        }
        public async Task<User> GetUserAsAdminAsync(string queryUsername)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand searchCommand = new SqlCommand(searchByIdentQuery, connection);
                    searchCommand.Parameters.AddWithValue("@username", queryUsername);

                    SqlDataReader reader = await searchCommand.ExecuteReaderAsync();



                    while (reader.Read())
                    {
                        string imagePath = reader.GetString("ImagePath");
                        string name = reader.GetString("Name");
                        char gender = reader.GetChar("Gender");
                        string username = reader.GetString("Username");
                        string phone = reader.GetString("Phone");
                        string email = reader.GetString("Email");
                        string address = reader.GetString("Address");
                        string homeMunicipality = reader.GetString("HomeMunicipality");
                        DateOnly birthDate = DateOnly.FromDateTime(reader.GetDateTime("Birthdate"));
                        AccessLevel accessLevel = (AccessLevel)reader.GetInt32("AccessLevel");
                        return new User(imagePath, name, gender, username, phone, email, address, homeMunicipality, birthDate, accessLevel);
                    }
                    return null;

                }
                catch (SqlException sqlExp)
                {
                    throw sqlExp;
                }
            }
        }

        public async Task<User> GetUserLoginAsync(string queryUsername, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand searchCommand = new SqlCommand(searchLoginQuery, connection);
                    searchCommand.Parameters.AddWithValue("@username", queryUsername);
                    searchCommand.Parameters.AddWithValue("@password", password);

                    SqlDataReader reader = await searchCommand.ExecuteReaderAsync();



                    while (reader.Read())
                    {
                        string imagePath = reader.GetString("ImagePath");
                        string name = reader.GetString("Name");
                        char gender = reader.GetChar("Gender");
                        string username = reader.GetString("Username");
                        string phone = reader.GetString("Phone");
                        string email = reader.GetString("Email");
                        string address = reader.GetString("Address");
                        string homeMunicipality = reader.GetString("HomeMunicipality");
                        DateOnly birthDate = DateOnly.FromDateTime(reader.GetDateTime("Birthdate"));
                        AccessLevel accessLevel = (AccessLevel)reader.GetInt32("AccessLevel");
                        return new User(imagePath, name, gender, username, phone, email, address, homeMunicipality, birthDate, accessLevel);
                    }
                    return null;

                }
                catch (SqlException sqlExp)
                {
                    throw sqlExp;
                }
            }
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

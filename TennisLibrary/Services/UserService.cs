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

namespace TennisLibrary.Services
{
    public class UserService : IUserService
    {
        private string connectionString = ConnectionManager.ConnectionString;

        private string

            insertQuery = "INSERT into TennisUser Values(@username, @gender, @name, @phone, @email, @address, @homeMunicipality, @birthdate, @accessLevel)",
            searchByIdentQuery = "SELECT * From TennisUser Where Username = @username",
            searchLoginQuery = "SELECT * From TennisUser Where Username = @username AND @password",
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
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);

                    insertCommand.Parameters.AddWithValue("@username", username);
                    insertCommand.Parameters.AddWithValue("@gender", gender);
                    insertCommand.Parameters.AddWithValue("@name", name);
                    insertCommand.Parameters.AddWithValue("@phone", phone);
                    insertCommand.Parameters.AddWithValue("@email", email);
                    insertCommand.Parameters.AddWithValue("@address", address);
                    insertCommand.Parameters.AddWithValue("@homeMunicipality", homeMunicipality);
                    insertCommand.Parameters.AddWithValue("@birthDate",birthDate);

                    return 0 < await insertCommand.ExecuteNonQueryAsync();
                }
                catch (SqlException sqlExp)
                {
                    throw sqlExp;
                }
            }
            return false;
        }
        public async Task<User> GetUserAsAdmin(string queryUsername)
        {
            using (SqlConnection connection = new SqlConnection())
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
                        return new User(imagePath, name, gender, username, phone, email, address, homeMunicipality, birthDate);
                    }
                    return null;

                }
                catch (SqlException sqlExp)
                {
                    throw sqlExp;
                }
            }
        }

        public async Task<User> GetUserLogin(string queryUsername, string password)
        {
            using (SqlConnection connection = new SqlConnection())
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
                        return new User(imagePath, name, gender, username, phone, email, address, homeMunicipality, birthDate);
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

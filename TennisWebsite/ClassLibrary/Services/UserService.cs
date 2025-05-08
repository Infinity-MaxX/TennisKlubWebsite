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
            searchLoginQuery = "SELECT * From TennisUser Where Username = @username AND HashPass = @password",
            searchAllQuery = "SELECT * From TennisUser",
            editQuery = "Update TennisUser Set Gender = @gender, Name = @name, Phone = @phone, Email = @email, Address = @address, HomeMunicipality = @homeMunicipality, Birthdate = @birthdate, ImagePath = @imagePath Where Username = @queryUsername",
            deleteQuery = "Delete From TennisUser Where Username = @username"
            ;

        public async Task<bool> AddUserAsync(User newUser, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);

                    insertCommand.Parameters.AddWithValue("@imagePath", newUser.ImageName);
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
                        char gender = reader.GetString("Gender")[0];
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
                    searchCommand.Parameters.AddWithValue("@password", Hasher.CreateHashString(password));

                    SqlDataReader reader = await searchCommand.ExecuteReaderAsync();



                    while (reader.Read())
                    {
                        string imagePath = reader.GetString("ImagePath");
                        string name = reader.GetString("Name");
                        char gender = reader.GetString("Gender")[0];
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

        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> results = new List<User>();
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand searchCommand = new SqlCommand(searchAllQuery, connection);

                    SqlDataReader reader = await searchCommand.ExecuteReaderAsync();



                    while (reader.Read())
                    {
                        string imagePath = reader.GetString("ImagePath");
                        string name = reader.GetString("Name");
                        char gender = reader.GetString("Gender")[0];
                        string username = reader.GetString("Username");
                        string phone = reader.GetString("Phone");
                        string email = reader.GetString("Email");
                        string address = reader.GetString("Address");
                        string homeMunicipality = reader.GetString("HomeMunicipality");
                        DateOnly birthDate = DateOnly.FromDateTime(reader.GetDateTime("Birthdate"));
                        AccessLevel accessLevel = (AccessLevel)reader.GetInt32("AccessLevel");
                        results.Add(new User(imagePath, name, gender, username, phone, email, address, homeMunicipality, birthDate, accessLevel));
                    }
                    await connection.CloseAsync();
                }
                catch (SqlException sqlExp)
                {
                    throw sqlExp;
                }
            }
            return results;
        }

        public async Task<bool> DeleteUserAsync(string queryUsername)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand insertCommand = new SqlCommand(deleteQuery, connection);
                    insertCommand.Parameters.AddWithValue("@username", queryUsername);

                    return 0 < await insertCommand.ExecuteNonQueryAsync();
                }
                catch (SqlException sqlExp)
                {
                    throw sqlExp;
                }
            }
        }

        public async Task<bool> EditUserAsync(string queryUsername, string newImagePath, string newName, char newGender, string newPhone, string newEmail, string newAddress, string newHomeMunicipality, DateOnly newBirthDate)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand insertCommand = new SqlCommand(editQuery, connection);

                    insertCommand.Parameters.AddWithValue("@queryUsername", queryUsername);

                    insertCommand.Parameters.AddWithValue("@imagePath", newImagePath);
                    insertCommand.Parameters.AddWithValue("@gender", newGender);
                    insertCommand.Parameters.AddWithValue("@name", newName);
                    insertCommand.Parameters.AddWithValue("@phone", newPhone);
                    insertCommand.Parameters.AddWithValue("@email", newEmail);
                    insertCommand.Parameters.AddWithValue("@address", newAddress);
                    insertCommand.Parameters.AddWithValue("@homeMunicipality", newHomeMunicipality);
                    insertCommand.Parameters.AddWithValue("@birthDate", newBirthDate);

                    return 0 < await insertCommand.ExecuteNonQueryAsync();
                }
                catch (SqlException sqlExp)
                {
                    throw sqlExp;
                }
            }
        }
    }
}

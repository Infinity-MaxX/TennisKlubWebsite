using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;

namespace TennisLibrary.Services
{
    public class BookingService : IBookingService
    {
        private string insertQuery = "INSERT into TennisBooking Values(@Player1, @Player2, @Start, @End, @CourtName)";
        private string deleteSQL = "DELETE from TennisBooking where BookingID = @BookingID";
        private string updateSQLTime = "Update TennisBooking set LastMaintenance = @LastMaintenance where Name = @Name";
        private string updateSQLPlayer = "Update TennisCourt set LastMaintenance = @LastMaintenance where Name = @Name";

        async public Task<bool> AddBookingAdminAsync(Booking newBooking)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);

                    insertCommand.Parameters.AddWithValue("@Player1", newBooking.Player1);
                    insertCommand.Parameters.AddWithValue("@Player2", newBooking.Player2);
                    insertCommand.Parameters.AddWithValue("@Start", newBooking.Start);
                    insertCommand.Parameters.AddWithValue("@End", newBooking.End);
                    insertCommand.Parameters.AddWithValue("@CourtName", newBooking.CourtName);
                    return 0 < await insertCommand.ExecuteNonQueryAsync();
                }
                catch (SqlException sqlExp)
                {
                    throw sqlExp;
                }
            }
        }

        async public Task<bool> AddBookingUserAsync(Booking newBooking)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);

                    insertCommand.Parameters.AddWithValue("@Player1", newBooking.Player1);
                    insertCommand.Parameters.AddWithValue("@Player2", newBooking.Player2);
                    insertCommand.Parameters.AddWithValue("@Start", newBooking.Start);
                    insertCommand.Parameters.AddWithValue("@CourtName", newBooking.CourtName);
                    return 0 < await insertCommand.ExecuteNonQueryAsync();
                }
                catch (SqlException sqlExp)
                {
                    throw sqlExp;
                }
            }
        }

        async public Task<bool> DeleteBooking(int ID)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(deleteSQL, connection);
                    command.Parameters.AddWithValue("@BookingID", ID);
                    int noOfRows = await command.ExecuteNonQueryAsync();

                    return noOfRows == 1;
                }

                catch (SqlException sqlx)
                {
                    Console.WriteLine(sqlx.Message);
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }

        public Task<List<Booking>> GetAllBookingsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Booking>> GetBookingsByUserAsync(string Username)
        {
            throw new NotImplementedException();
        }

        async public Task<bool> UpdateBooking(int ID, User Player2)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(updateSQLPlayer, connection);
                    command.Parameters.AddWithValue("@Player2", Player2);
                    int noOfRows = await command.ExecuteNonQueryAsync();

                    return noOfRows == 1;
                }
                catch (SqlException sqlx)
                {
                    Console.WriteLine(sqlx.Message);
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }

        public Task<bool> UpdateBooking(int ID, DateTime Start)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBookingAdmin(int ID, DateTime Start, DateTime End)
        {
            throw new NotImplementedException();
        }
    }
}

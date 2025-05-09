using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;

namespace TennisLibrary.Services
{
    public class BookingService : IBookingService
    {
        private string insertQuery = "INSERT into TennisBooking Values(@Court, @Player1, @Player2, @Start, @End)";
        private string deleteSQL = "DELETE from TennisBooking where BookingID = @BookingID";
        private string updateSQLTime = "Update TennisBooking set Start = @start and End = @end where BookingID = @ID";
        private string updateSQLTimeAndPlayer = "Update TennisBooking set Player2 = @Player2 and set Start = @start and End = @end where BookingID = @ID";
        private string updateSQLPlayer = "Update TennisBooking set Player2 = @Player2 where BookingID = @ID";
        private string getSQLBookings = "SELECT * from TennisBooking";

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
                    insertCommand.Parameters.AddWithValue("@Court", newBooking.Court);
                    return 0 < await insertCommand.ExecuteNonQueryAsync();
                }
                catch (SqlException sqlExp)
                {
                    throw sqlExp;
                }
                finally
                {
                    await connection.CloseAsync();
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

                    insertCommand.Parameters.AddWithValue("@Player1", newBooking.Player1.Username);
                    insertCommand.Parameters.AddWithValue("@Player2", newBooking.Player2.Username);
                    insertCommand.Parameters.AddWithValue("@Start", newBooking.Start);
                    insertCommand.Parameters.AddWithValue("@End", newBooking.End);
                    insertCommand.Parameters.AddWithValue("@Court", newBooking.Court.Name);
                    return 0 < await insertCommand.ExecuteNonQueryAsync();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
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

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    List<Booking> bookings = new List<Booking>();

                    SqlCommand command = new SqlCommand(getSQLBookings, connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    IUserService tempUserService = new UserService();
                    ICourtService tempCourtService = new CourtService();

                    while (reader.Read())
                    {
                        User Player1 = await tempUserService.GetUserAsAdminAsync(reader.GetString("Player1"));
                        User Player2 = await tempUserService.GetUserAsAdminAsync(reader.GetString("Player2"));
                        Court Court = await tempCourtService.GetCourtAsync(reader.GetString("CourtName"));
                        bookings.Add(new Booking(Player1, Player2, Court, reader.GetDateTime("StartDate"), reader.GetDateTime("EndDate")));
                    }
                    return bookings;
                }
                catch (SqlException sqlExp)
                {
                    throw sqlExp;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }

        public async Task<List<Booking>> GetBookingsByDatesAsync(DateTime Start, DateTime End)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    List<Booking> bookings = new List<Booking>();

                    SqlCommand command = new SqlCommand(getSQLBookings, connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    IUserService tempUserService = new UserService();
                    ICourtService tempCourtService = new CourtService();

                    while (reader.Read())
                    {
                        DateTime tempStart = reader.GetDateTime("StartDate");
                        DateTime tempEnd = reader.GetDateTime("EndDate");
                        if ((tempStart > Start && tempStart < End) || (tempEnd > Start && tempEnd < End) || (tempStart < Start && tempEnd > End))
                        {
                            User Player1 = await tempUserService.GetUserAsAdminAsync(reader.GetString("Player1"));
                            User Player2 = await tempUserService.GetUserAsAdminAsync(reader.GetString("Player2"));
                            Court Court = await tempCourtService.GetCourtAsync(reader.GetString("CourtName"));
                            bookings.Add(new Booking(Player1, Player2, Court, reader.GetDateTime("StartDate"), reader.GetDateTime("EndDate")));
                        }
                    }
                    return bookings;
                }
                catch (SqlException sqlExp)
                {
                    throw sqlExp;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }

        public async Task<List<Booking>> GetBookingsByUserAsync(string Username)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    List<Booking> bookings = new List<Booking>();

                    SqlCommand command = new SqlCommand(getSQLBookings, connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    IUserService tempUserService = new UserService();
                    ICourtService tempCourtService = new CourtService();

                    while (reader.Read())
                    {
                        User Player1 = await tempUserService.GetUserAsAdminAsync(reader.GetString("Player1"));
                        if (Player1.Username == Username)
                        {
                            User Player2 = await tempUserService.GetUserAsAdminAsync(reader.GetString("Player2"));
                            Court Court = await tempCourtService.GetCourtAsync(reader.GetString("CourtName"));
                            bookings.Add(new Booking(Player1, Player2, Court, reader.GetDateTime("StartDate"), reader.GetDateTime("EndDate")));
                        }
                    }
                    return bookings;
                }
                catch (SqlException sqlExp)
                {
                    throw sqlExp;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }

        async public Task<bool> UpdateBookingPlayer2(int ID, User Player2)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(updateSQLPlayer, connection);
                    command.Parameters.AddWithValue("@Player2", Player2);
                    command.Parameters.AddWithValue("@ID", ID);
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

        async public Task<bool> UpdateBookingStart(int ID, DateTime Start)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(updateSQLTime, connection);
                    command.Parameters.AddWithValue("@Start", Start);
                    command.Parameters.AddWithValue("@end", Start.AddHours(1));
                    command.Parameters.AddWithValue("@ID", ID);
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

        async public Task<bool> UpdateBookingTimeAdmin(int ID, DateTime Start, DateTime End)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(updateSQLTime, connection);
                    command.Parameters.AddWithValue("@Start", Start);
                    command.Parameters.AddWithValue("@end", End);
                    command.Parameters.AddWithValue("@ID", ID);
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

        public async Task<bool> UpdateBookingStartPlayer2(int ID, DateTime Start, User Player2)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(updateSQLTimeAndPlayer, connection);
                    command.Parameters.AddWithValue("@Start", Start);
                    command.Parameters.AddWithValue("@end", Start.AddHours(1));
                    command.Parameters.AddWithValue("@ID", ID);
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

        public async Task<bool> UpdateBookingTimeAndPlayer2Admin(int ID, User Player2, DateTime Start, DateTime End)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(updateSQLTimeAndPlayer, connection);
                    command.Parameters.AddWithValue("@Start", Start);
                    command.Parameters.AddWithValue("@end", End);
                    command.Parameters.AddWithValue("@ID", ID);
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
    }
}

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
        async Task<bool> IBookingService.AddBookingAsync(Booking newBooking)
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
    }
}

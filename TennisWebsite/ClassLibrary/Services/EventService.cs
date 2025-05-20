using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using TennisLibrary;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;
using TennisWebsite.Interfaces;
using TennisWebsite.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TennisWebsite.Services
{
    public class EventService : IEventService
    {

        #region instance fields
        private string insertEventSql = "INSERT INTO TENNISEVENT " +
            "(OrgUsername, StartDate, EndDate, EventType, Name, Description, MinAttendees, MaxAttendees)" +
            "VALUES (@OrgUsername, @StartDate, @EndDate, @EventType, @Name, @Description, @MinAttendees, @MaxAttendees)";
        private string deleteSql = "DELETE FROM TENNISEVENT WHERE EventID = @Id";
        private string selectSql = "SELECT * "
        #endregion

        #region Constructors
        public EventService()
        {

        }

        #endregion

        #region Methods
        public async Task<bool> CreateEventAsync(Event newEvent)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(insertEventSql, connection);
                    command.Parameters.AddWithValue("@OrgUsername", newEvent.Organiser);
                    command.Parameters.AddWithValue("@StartDate", newEvent.Start);
                    command.Parameters.AddWithValue("@EndDate", newEvent.End);
                    command.Parameters.AddWithValue("@EventType", newEvent.Type);
                    command.Parameters.AddWithValue("@Name", newEvent.Name);
                    command.Parameters.AddWithValue("@Description", newEvent.Description);
                    command.Parameters.AddWithValue("@MinAttendees", newEvent.MinParticipants);
                    command.Parameters.AddWithValue("@MaxAttendees", newEvent.MaxParticipants);
                    int noOfRows = await command.ExecuteNonQueryAsync();

                    return noOfRows == 1;
                }

                catch (SqlException sqlx)
                {
                    Console.WriteLine(sqlx.Number);
                    Console.WriteLine(sqlx.Message);
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    SqlCommand DeleteAttendees = new SqlCommand("DELETE * FROM TennisAttendees Where EventID = @EventID", connection);
                    DeleteAttendees.Parameters.AddWithValue("@EventID", id);
                    await command.ExecuteNonQueryAsync();
                    int noOfRows = await command.ExecuteNonQueryAsync();

                    return noOfRows == 1;
                }

                catch (SqlException sqlx)
                {
                    Console.WriteLine(sqlx.Number);
                    Console.WriteLine(sqlx.Message);
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }

        public Task<List<Event>> GetAllEventsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetEvent(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetEventByType(string type)
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetEventsByOrganiser(string orgName)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetParticipantsByEventAsync(int eventID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> JoinEvent(int eventID, string userName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LeaveEvent(int eventID, string userName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEvent(string organiser, string name, string description, string start, string end, int maxParticipants, int minParticipants)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

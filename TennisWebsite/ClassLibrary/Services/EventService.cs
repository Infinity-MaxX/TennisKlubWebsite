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
using TennisLibrary.Services;
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
        private string selectSql = "SELECT * From TennisEvent";
        private string selectAttendeeSql = "SELECT * FROM TennisEvent";
        private string insertAttendeeSql = "Insert Into TennisAttendees (@EventID, @Username";
        private string deleteAttendeeSql = "Delete from TennisAttendees where Username = @Username and EventID = @eventID";
        private string updateEventSql = "Update TennisEvent Set " +
            "OrgUsername = @orgUser, StartDate = @start, EndDate = @end, EventType = @type, Name = @name, Description = @des, " +
            "MinAttendees = @min, MaxAttendees = @max where EventID = @id";
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
                    SqlCommand DeleteAttendees = new SqlCommand("DELETE FROM TennisAttendees Where EventID = @EventID", connection);
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

        public async Task<List<Event>> GetAllEventsAsync()
        {
            using(SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(selectSql, connection);

                    List<Event> events = new List<Event>();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        Event e = new Event(reader.GetInt32(0));
                        e.Organiser = reader.GetString(1);
                        e.Start = reader.GetDateTime(2);
                        e.End = reader.GetDateTime(3);
                        e.Type = reader.GetString(4);
                        e.Name = reader.GetString(5);
                        e.Description = reader.GetString(6);
                        e.MinParticipants = reader.GetInt32(7);
                        e.MaxParticipants = reader.GetInt32(8);
                        events.Add(e);
                    }

                    return events;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<Event> GetEvent(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(selectSql + "Where EventID = @id", connection);
                    command.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Event e = new Event(0);
                    if (reader.Read())
                    {
                        e = new Event(reader.GetInt32(0));
                        e.Organiser = reader.GetString(1);
                        e.Start = reader.GetDateTime(2);
                        e.End = reader.GetDateTime(3);
                        e.Type = reader.GetString(4);
                        e.Name = reader.GetString(5);
                        e.Description = reader.GetString(6);
                        e.MinParticipants = reader.GetInt32(7);
                        e.MaxParticipants = reader.GetInt32(8);
                    }

                    return e;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<List<Event>> GetEventsByType(string type)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(selectSql + "Where Type = @type", connection);
                    command.Parameters.AddWithValue("@type", type);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    List<Event> events = new List<Event>();
                    while (reader.Read())
                    {
                        Event e = new Event(reader.GetInt32(0));
                        e.Organiser = reader.GetString(1);
                        e.Start = reader.GetDateTime(2);
                        e.End = reader.GetDateTime(3);
                        e.Type = reader.GetString(4);
                        e.Name = reader.GetString(5);
                        e.Description = reader.GetString(6);
                        e.MinParticipants = reader.GetInt32(7);
                        e.MaxParticipants = reader.GetInt32(8);
                        events.Add(e);
                    }

                    return events;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<List<Event>> GetEventsByOrganiser(string orgName)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(selectSql + "Where OrgUsername = @name", connection);
                    command.Parameters.AddWithValue("@name", orgName);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    List<Event> events = new List<Event>();
                    while (reader.Read())
                    {
                        Event e = new Event(reader.GetInt32(0));
                        e.Organiser = reader.GetString(1);
                        e.Start = reader.GetDateTime(2);
                        e.End = reader.GetDateTime(3);
                        e.Type = reader.GetString(4);
                        e.Name = reader.GetString(5);
                        e.Description = reader.GetString(6);
                        e.MinParticipants = reader.GetInt32(7);
                        e.MaxParticipants = reader.GetInt32(8);
                        events.Add(e);
                    }

                    return events;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<List<User>> GetParticipantsByEventAsync(int eventID)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    UserService us = new UserService();
                    SqlCommand command = new SqlCommand(selectAttendeeSql + "Where EventID = @id", connection);
                    command.Parameters.AddWithValue("@id", eventID);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    List<User> users = new List<User>();
                    while (reader.Read())
                    {
                        User u = await us.GetUserAsAdminAsync(reader.GetString(2));
                        users.Add(u);
                    }

                    return users;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<bool> JoinEvent(int eventID, string userName)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(insertAttendeeSql, connection);
                    command.Parameters.AddWithValue("@EventID", eventID);
                    command.Parameters.AddWithValue("@Username", userName);

                    return 0 < await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<bool> LeaveEvent(int eventID, string userName)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(deleteAttendeeSql, connection);

                    command.Parameters.AddWithValue("@eventID", eventID);
                    command.Parameters.AddWithValue("@Username", userName);

                    return 0 < await command.ExecuteNonQueryAsync();
                }

                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public async Task<bool> UpdateEvent(int eventID, string organiser, string name, string description, string start, string end, string type,int maxParticipants, int minParticipants)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(updateEventSql, connection);

                    command.Parameters.AddWithValue("@orgUser", organiser);
                    command.Parameters.AddWithValue("@start", start);
                    command.Parameters.AddWithValue("@end", end);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@des", description);
                    command.Parameters.AddWithValue("@min", minParticipants);
                    command.Parameters.AddWithValue("@max", maxParticipants);
                    command.Parameters.AddWithValue("@id", eventID);

                    return 0 < await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        #endregion
    }
}

using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using TennisLibrary;
using TennisLibrary.Models;
using TennisWebsite.ClassLibrary.Interfaces;
using TennisWebsite.ClassLibrary.Models;

namespace TennisWebsite.ClassLibrary.Services
{
    public class GearService : IGearService
    {
        #region Instances
        private string queryString = "SELECT * FROM TennisGear";
        private string filterByIdSql = "SELECT * FROM TennisGear WHERE GearID = @ID";
        private string filterByTypeSql = "SELECT * FROM TennisGear WHERE Name = @Name";
        private string insetBookingGearSql = "INSERT INTO TennisBookedGear Values(@BookingID, @GearID, @Count)";
        private string insertSql = "INSERT INTO TennisGear Values(@Name, @Description)";
        private string deleteSql = "DELETE FROM TennisGear WHERE GearID = @ID";
        private string updateSql = "UPDATE TennisGear SET Name = @Name, Description = @Description WHERE GearID = @ID";
        //private string connectionString = ConnectionManager.ConnectionString; // static, call when needed
        #endregion

        #region Properties
        public bool Status { get; set; }
        #endregion

        #region Constructor
        public GearService()
        {
            
        }
        #endregion
         
        #region Methods
        public async Task<bool> AddGearAsync(Gear gear)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@Name", gear.Name);
                    command.Parameters.AddWithValue("@Description", gear.Description);
                    await connection.OpenAsync();

                    int numberOfRows = await command.ExecuteNonQueryAsync();

                    return numberOfRows > 0;
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error: " + sqlExp.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<bool> BookGearAsync(int bookingID, int gearID, int count)
        {
            int status = await CheckStatus(gearID);
            if (status - count < 0) { return false; }

            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insetBookingGearSql, connection);
                    command.Parameters.AddWithValue("@BookingID", bookingID);
                    command.Parameters.AddWithValue("@GearID", gearID);
                    command.Parameters.AddWithValue("@Count", count);
                    await connection.OpenAsync();

                    int numberOfRows = await command.ExecuteNonQueryAsync();
                    return numberOfRows > 0;
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error: " + sqlExp.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<int> CheckStatusAsync(int gearID)
        {
            Gear gear = await GetGearAsync(gearID);
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(filterByIdSql, connection);
                    command.Parameters.AddWithValue("@GearID", gearID);
                    await connection.OpenAsync();

                    return gear.Count;
                    //int amount = gear.Count;
                    //if (amount == 0) { return false; }
                    //return true;
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error: " + sqlExp.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<bool> DeleteGearAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    command.Parameters.AddWithValue("@ID", id);
                    await connection.OpenAsync();

                    int numberOfRows = await command.ExecuteNonQueryAsync();
                    if (numberOfRows == 0) { return false; }
                    return true;
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error: " + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    await connection.CloseAsync();
                }
                return false;
            }
        }
        public async Task<List<Gear>> GetAllAsync()
        {
            List<Gear> gears = new List<Gear>();
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int gearID = reader.GetInt32("GearID");
                        string gearName = reader.GetString("Name");
                        string gearDescription = reader.GetString("Description");
                        Gear gear = new Gear(gearID, gearName, gearDescription);
                        gears.Add(gear);
                    }
                    await reader.CloseAsync();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error: " + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
            return gears;
        }
        public async Task<Gear> GetByTypeAsync(string name)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                Gear gear = null;

                try
                {
                    SqlCommand command = new SqlCommand(filterByTypeSql, connection);
                    command.Parameters.AddWithValue("@Name", name);
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        string? gearDescription = reader.GetString("Description");
                        gear = new Gear(name, gearDescription);
                    }
                    await reader.CloseAsync();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error: " + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    await connection.CloseAsync();
                }
                return gear;
            }
        }
        public async Task<Gear> GetGearAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                Gear gear = null;

                try
                {
                    SqlCommand command = new SqlCommand(filterByIdSql, connection);
                    command.Parameters.AddWithValue("@ID", id);
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        string gearName = reader.GetString("Name");
                        string? gearDescription = reader.GetString("Description");
                        gear = new Gear(id, gearName, gearDescription);
                    }
                    await reader.CloseAsync();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error: " + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    await connection.CloseAsync();
                }
                return gear;
            }
        }
        #endregion
    }
}

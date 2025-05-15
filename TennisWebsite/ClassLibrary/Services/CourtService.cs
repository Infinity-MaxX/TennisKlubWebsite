using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;

namespace TennisLibrary.Services
{
    public class CourtService : ICourtService
    {

        private string insertSql = "Insert INTO TennisCourt (CourtName, Type) Values(@Name, @Type)";
        private string deleteSQL = "Delete from TennisCourt where CourtName = @Name";
        private string getSQL = "Select * from TennisCourt";
        private string updateSQL = "Update TennisCourt set";

        public CourtService()
        {

        }

        public async Task<bool> CreateCourtAsync(Court court)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@Name", court.Name);
                    command.Parameters.AddWithValue("@Type", court.Type);
                    int noOfRows = await command.ExecuteNonQueryAsync();

                    return noOfRows == 1;
                }

                catch (SqlException sqlx)
                {
                    Console.WriteLine(sqlx.Number);
                    if (sqlx.Number == 2627) throw new Exception("En bane med det givne navn findes allerede");
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }

        public async Task<bool> DeleteCourtAsync(string name)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(deleteSQL, connection);
                    command.Parameters.AddWithValue("@Name", name);
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

        public async Task<List<Court>> GetAllCourts()
        {
            List<Court> courts = new List<Court>();
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(getSQL, connection);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        courts.Add(new Court(reader.GetString("CourtName"), reader.GetString("Type")));
                    }

                    return courts;
                }

                catch (SqlException sqlx)
                {
                    Console.WriteLine(sqlx.Message);
                    return null;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }

        public async Task<Court> GetCourtAsync(string name)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    Court court = new Court();
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(getSQL + " Where CourtName = @Name", connection);
                    command.Parameters.AddWithValue("@Name", name);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if(reader.Read())
                    {
                        court.Name = reader.GetString("CourtName");
                        court.Type = reader.GetString("Type");
                    } 

                    return court;
                }

                catch (SqlException sqlx)
                {
                    Console.WriteLine(sqlx.Message);
                    return null;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }

        public async Task<List<Court>> GetCourtsOfTypeAsync(string type)
        {
            List<Court> courts = new List<Court>();
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(getSQL + "Where Type = @Type", connection);
                    command.Parameters.AddWithValue("@Type", type);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        courts.Add(new Court(reader.GetString("Name"), reader.GetString("Type")));
                    }

                    return courts;
                }

                catch (SqlException sqlx)
                {
                    Console.WriteLine(sqlx.Message);
                    return null;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }

        public async Task<bool> UpdateCourtAsync(string oldName, string? name, DateOnly? maintenance)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    if (maintenance == null && string.IsNullOrEmpty(name)) 
                        throw new Exception("Intet input givet til navn eller veligeholdese");
                    await connection.OpenAsync();
                    string sqlString = updateSQL;

                    if (!string.IsNullOrEmpty(name)) sqlString += " CourtName = @Name";

                    if (maintenance != null && sqlString.Contains("CourtName")) 
                        sqlString += ", LastMaintenance = @LastMaintenance";

                    if (maintenance != null && !sqlString.Contains("CourtName"))
                        sqlString += " LastMaintenance = @LastMaintenance";
                    sqlString += " Where CourtName = @CourtName";
                    SqlCommand command = new SqlCommand(sqlString, connection);
                    command.Parameters.AddWithValue("@CourtName", oldName);

                    if (!string.IsNullOrEmpty(name)) command.Parameters.AddWithValue("@Name", name);
                    if (maintenance != null) command.Parameters.AddWithValue("@LastMaintenance", maintenance);

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

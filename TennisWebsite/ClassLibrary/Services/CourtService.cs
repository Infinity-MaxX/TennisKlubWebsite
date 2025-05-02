using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;

namespace TennisLibrary.Services
{
    public class CourtService : ICourtService
    {

        private string insertSql = "Insert INTO TennisCourt Values(@Name, @Type)";
        private string deleteSQL = "Delete from TennisCourt where Name = @Name";
        private string getSQL = "Select * from TennisCourt";
        private string updateSQL = "Update TennisCourt set LastMaintenance = @LastMaintenance where Name = @Name";

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
                    Console.WriteLine(sqlx.Message);
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

        public async Task<Court> GetCourtAsync(string name)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    Court court = new Court();
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(getSQL + "Where Name = @Name", connection);
                    command.Parameters.AddWithValue("@Name", name);
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if(reader.Read())
                    {
                        court.Name = reader.GetString("Name");
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

        public async Task<bool> UpdateLastMaintenanceAsync(string name, DateOnly maintenance)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand(updateSQL, connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@LastMaintenance", maintenance);
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

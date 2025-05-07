using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TennisLibrary.Services
{
    public class BlogService : IBlogService
    {
        #region Instances
        public List<Blog> _blogPosts;
        private string queryString = "SELECT * FROM TennisBlog";
        private string filterByAuthorSql = "SELECT * FROM TennisBlog WHERE Author = @Author";
        private string filterByDateSql = "SELECT * FROM TennisBlog WHERE PostDate = @Date";
        private string filterByIdSql = "SELECT * FROM TennisBlog WHERE BlogID = @ID";
        private string insertSql = "INSERT INTO TennisBlog Values(@Author, @Title, @Body, @Date)";
        private string deleteSql = "DELETE FROM TennisBlog WHERE BlogID = @ID";
        private string updateSql = "UPDATE TennisBlog SET Author = @Author, Title = @Title, Body = @Body WHERE BlogID = @ID";
        //private string connectionString = ConnectionManager.ConnectionString; // static, call when needed
        #endregion

        #region Constructor
        public BlogService() 
        {
            
        }
        #endregion

        #region Methods
        public async Task<bool> CreatePostAsync(Blog post)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@Author", post.Author);
                    command.Parameters.AddWithValue("@Title", post.Title);
                    command.Parameters.AddWithValue("@Body", post.Body);
                    command.Parameters.AddWithValue("@Date", post.Date);
                    await connection.OpenAsync();

                    int numberOfRows = await command.ExecuteNonQueryAsync();

                    return numberOfRows > 0;
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
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
        public async Task<bool> DeletePostAsync(int id)
        {
            //Blog toDelete = await GetByIdAsync(id);
            //if (toDelete == null) { return false; }

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
                    Console.WriteLine("Database error" + sqlExp.Message);
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
        public async Task<List<Blog>> GetAllPostsAsync()
        {
            List<Blog> posts = new List<Blog>();
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int postID = reader.GetInt32("BlogID");
                        string postAuthor = reader.GetString("Author");
                        string? postTitle = reader.GetString("Title");
                        string postBody = reader.GetString("Body");
                        Blog post = new Blog(postID, postAuthor, postTitle, postBody);
                        posts.Add(post);
                    }
                    await reader.CloseAsync();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
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
            return posts;
        }
        public async Task<List<Blog>> GetByAuthorAsync(string author)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                List<Blog> posts = new List<Blog>();

                try
                {
                    SqlCommand command = new SqlCommand(filterByAuthorSql, connection);
                    command.Parameters.AddWithValue("@Author", author);
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        string postAuthor = reader.GetString("Author");
                        string? postTitle = reader.GetString("Title");
                        string postBody = reader.GetString("Body");
                        Blog post = new Blog(postAuthor, postTitle, postBody);
                        posts.Add(post);
                    }
                    await reader.CloseAsync();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    await connection.CloseAsync();
                }
                return posts;
            }
        }
        public async Task<List<Blog>> GetByDateAsync(DateOnly date)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                List<Blog> posts = new List<Blog>();

                try
                {
                    SqlCommand command = new SqlCommand(filterByDateSql, connection);
                    command.Parameters.AddWithValue("@Date", date);
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        string postAuthor = reader.GetString("Author");
                        string? postTitle = reader.GetString("Title");
                        string postBody = reader.GetString("Body");
                        Blog post = new Blog(postAuthor, postTitle, postBody);
                        posts.Add(post);
                    }
                    await reader.CloseAsync();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    await connection.CloseAsync();
                }
                return posts;
            }
        }
        public async Task<Blog> GetByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                Blog post = null;

                try
                {
                    SqlCommand command = new SqlCommand(filterByIdSql, connection);
                    command.Parameters.AddWithValue("@ID", id);
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        string postAuthor = reader.GetString("Author");
                        string? postTitle = reader.GetString("Title");
                        string postBody = reader.GetString("Body");
                        post = new Blog(id, postAuthor, postTitle, postBody);
                    }
                    await reader.CloseAsync();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    await connection.CloseAsync();
                }
                return post;
            }
        }
        public async Task<bool> UpdatePostAsync(string author, string? title, string body)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionManager.ConnectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(updateSql, connection);
                    command.Parameters.AddWithValue("@Author", author);
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Body", body);
                    await connection.OpenAsync();

                    int numberOfRows = await command.ExecuteNonQueryAsync();

                    return numberOfRows > 0;
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
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
        #endregion
    }
}

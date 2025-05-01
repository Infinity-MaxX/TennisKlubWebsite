using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;

namespace TennisLibrary.Services
{
    public class BlogService : IBlogService
    {
        #region Instances
        public List<Blog> _blogPosts;
        private string queryString = "SELECT * FROM Blog";
        private string filterByAuthorSql = "SELECT * FROM Blog WHERE Author = @Author";
        private string filterByDateSql = "SELECT * FROM Blog WHERE Date = @Date";
        private string insertSql = "INSERT INTO Blog Values(@ID, @Author, @Title, @Body, @Date)";
        private string deleteSql = "DELETE FROM Blog WHERE BlogPostID = @ID";
        private string updateSql = "UPDATE Blog SET Author = @Author, Title = @Title, Body = @Body WHERE BlogPostID = @ID";
        //private string connectionString = ConnectionManager.ConnectionString; // static, call when needed
        #endregion

        #region Constructor
        public BlogService() 
        {
            
        }
        #endregion

        #region Methods
        public Task<bool> CreatePostAsync(Blog newPost)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePostAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Blog>> GetAllPostsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Blog> GetByAuthorAsync(string author)
        {
            throw new NotImplementedException();
        }
        public Task<Blog> GetByDateAsync(DateOnly date)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePostAsync(string post, Blog newPost)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

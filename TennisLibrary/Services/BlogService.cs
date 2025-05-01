using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;

namespace TennisLibrary.Services
{
    public class BlogService : IBlogService
    {
        #region Instances

        #endregion

        #region Properties

        #endregion

        #region Constructor

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

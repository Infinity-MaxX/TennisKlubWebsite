using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLibrary.Models;

namespace TennisLibrary.Interfaces
{
    public interface IBlogService
    {
        Task<bool> CreatePostAsync(Blog newPost);
        Task<bool> DeletePostAsync(int id);
        Task<List<Blog>> GetAllPostsAsync();
        Task<Blog> GetByAuthorAsync(string author);
        Task<Blog> GetByDateAsync(DateOnly date);
        Task<bool> UpdatePostAsync(string post, Blog newPost);
    }
}

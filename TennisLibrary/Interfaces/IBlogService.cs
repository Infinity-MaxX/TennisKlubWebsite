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
        Task<bool> CreatePostAsync(Blog post);
        Task<bool> DeletePostAsync(int id);
        Task<List<Blog>> GetAllPostsAsync();
        Task<List<Blog>> GetByAuthorAsync(string author);
        Task<List<Blog>> GetByDateAsync(DateOnly date);
        Task<Blog> GetByIdAsync(int id);
        Task<bool> UpdatePostAsync(string author, string? title, string body);
    }
}

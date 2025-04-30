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
        Task<bool> UpdatePostAsync(Blog post);
        Task<bool> DeletePostAsync(Blog post);
        Task<List<Blog>> GetAllPostsAsync();
    }
}

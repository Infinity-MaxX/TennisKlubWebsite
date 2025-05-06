using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;

namespace TennisWebsite.Pages.Blogs
{
    public class EditBlogPostsModel : PageModel
    {
        #region Instances
        private IBlogService _blogPostService;
        #endregion

        #region Properties
        [BindProperty]
        public Blog Post { get; set; }
        #endregion

        #region Constructor
        public EditBlogPostsModel()
        {

        }
        #endregion

        #region Methods
        public async Task OnGetAsync(int id)
        {
            Post = await _blogPostService.GetByIdAsync(id);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _blogPostService.UpdatePostAsync(Post.Author, Post.Title, Post.Body);
            return Redirect("ShowBlogPosts");
        }
        #endregion
    }
}

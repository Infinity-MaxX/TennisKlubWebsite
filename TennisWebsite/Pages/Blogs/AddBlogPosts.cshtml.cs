using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;

namespace TennisWebsite.Pages.Blogs
{
    public class AddBlogPostsModel : PageModel
    {
        #region Instances
        private IBlogService _blogPostService;
        #endregion

        #region Properties
        [BindProperty]
        public Blog BlogPost { get; set; }
        #endregion

        #region Constructor
        public AddBlogPostsModel(IBlogService blogPostService)
        {
            _blogPostService = blogPostService;
        }
        #endregion

        #region Methods
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _blogPostService.CreatePostAsync(new Blog(BlogPost.Author, BlogPost.Title, BlogPost.Body));
            return RedirectToPage("ShowPosts");
        }
        #endregion
    }
}

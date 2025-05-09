using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using TennisLibrary.Helpers;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;
using TennisLibrary.Services;

namespace TennisWebsite.Pages.Blogs
{
    public class EditBlogPostsModel : PageModel
    {
        #region Instances
        private IBlogService _blogPostService;
        private const int _siteAccessRequirement = (int)AccessLevel.Admin;
        #endregion

        #region Properties
        [BindProperty]
        public Blog Post { get; set; }
        #endregion

        #region Constructor
        public EditBlogPostsModel()
        {
            _blogPostService = new BlogService();
        }
        #endregion

        #region Methods
        public async Task<IActionResult> OnGetAsync(int id)
        {
            int? SessionAccessLevel = HttpContext.Session.GetInt32("AccessLevel");

            if (SessionAccessLevel != null && SessionAccessLevel <= _siteAccessRequirement)
            {
                Post = await _blogPostService.GetByIdAsync(id);
                return Page();
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _blogPostService.UpdatePostAsync(Post.Author, Post.Title, Post.Body);
            return Redirect("ShowBlogPosts");
        }
        #endregion
    }
}

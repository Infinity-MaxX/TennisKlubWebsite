using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using TennisLibrary.Helpers;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;
using TennisLibrary.Services;

namespace TennisWebsite.Pages.Blogs
{
    public class ShowBlogPostsModel : PageModel
    {
        #region Instances
        private IBlogService _blogPostService;
        private const int _siteAccessRequirement = (int)AccessLevel.Admin;
        #endregion

        #region Properties
        public List<Blog> BlogPosts { get; set; }
        public bool IsAdmin { get; private set; }
        #endregion

        #region Constructor
        public ShowBlogPostsModel()
        {
            _blogPostService = new BlogService();
        }
        #endregion

        #region Methods
        public async Task OnGetAsync()
        {
            int? SessionAccessLevel = HttpContext.Session.GetInt32("AccessLevel");
            IsAdmin = (SessionAccessLevel != null && SessionAccessLevel >= _siteAccessRequirement);
            BlogPosts = await _blogPostService.GetAllPostsAsync();
        }

        /// <summary>
        /// A delete function as one does not need an extra page to delete a post.
        /// </summary>
        public async Task<IActionResult> OnGetDeleteAsync(int DeleteNo)
        {
            int? SessionAccessLevel = HttpContext.Session.GetInt32("AccessLevel");

            if (SessionAccessLevel != null && SessionAccessLevel <= _siteAccessRequirement)
            {
                await _blogPostService.DeletePostAsync(DeleteNo);
                return RedirectToPage("ShowBlogPosts");
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
        #endregion
    }
}

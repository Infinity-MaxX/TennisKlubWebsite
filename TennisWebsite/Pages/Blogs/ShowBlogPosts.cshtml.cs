using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;
using TennisLibrary.Services;

namespace TennisWebsite.Pages.Blogs
{
    public class ShowBlogPostsModel : PageModel
    {
        #region Instances
        private IBlogService _blogPostService;
        #endregion

        #region Properties
        public List<Blog> BlogPosts { get; set; }
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
            BlogPosts = await _blogPostService.GetAllPostsAsync();
        }

        /// <summary>
        /// A delete function as one does not need an extra page to delete a post.
        /// </summary>
        public async Task<IActionResult> OnGetDelete(int DeleteNo)
        {
            await _blogPostService.DeletePostAsync(DeleteNo);
            return RedirectToPage("ShowPosts");
        }
        #endregion
    }
}

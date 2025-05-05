using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using TennisLibrary.Helpers;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;
using TennisLibrary.Services;

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

        [BindProperty]
        public int ID { get; private set; }

        [BindProperty]
        public string Author { get; set; }

        [BindProperty]
        public string? Title { get; set; }

        [BindProperty]
        public string Body { get; set; }

        [BindProperty]
        public DateTime Date {  get; set; }

        [BindProperty]
        public AccessLevel AccessLevel { get; set; }

        public int? SessionAccessLevel { get; private set; }
        #endregion

        #region Constructor
        public AddBlogPostsModel()
        {
            _blogPostService = new BlogService();
        }
        #endregion

        #region Methods
        public async Task OnGetAsync()
        {
            SessionAccessLevel = HttpContext.Session.GetInt32("AccessLevel");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _blogPostService.CreatePostAsync(new Blog(BlogPost.Author, BlogPost.Title, BlogPost.Body));
            return RedirectToPage("ShowPosts");
        }
        #endregion
    }
}

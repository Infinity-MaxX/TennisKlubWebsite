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
        private const int _siteAccessRequirement = (int)AccessLevel.Admin;
        #endregion

        #region Properties
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
        #endregion

        #region Constructor
        public AddBlogPostsModel()
        {
            _blogPostService = new BlogService();
        }
        #endregion

        #region Methods
        public IActionResult OnGetAsync()
        {
            int? SessionAccessLevel = HttpContext.Session.GetInt32("AccessLevel");

            if (SessionAccessLevel != null && SessionAccessLevel <= _siteAccessRequirement)
            {
                Author = HttpContext.Session.GetString("Displayname");
                return Page();
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await _blogPostService.CreatePostAsync(new Blog(Author, Title, Body));
            return RedirectToPage("ShowBlogPosts");
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TennisWebsite.Interfaces;
using TennisWebsite.Models;

namespace TennisWebsite.Pages.Events
{
    public class AddEventModel : PageModel
    {

        [BindProperty]
        public Event NewEvent { get; set; }

        private IEventService _eventService;

        public AddEventModel(IEventService eventService)
        {
            _eventService = eventService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _eventService.CreateEventAsync(NewEvent);
            return RedirectToPage("/Pages/Events/ShowEvents");
        }
    }
}

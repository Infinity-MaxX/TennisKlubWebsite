using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLibrary.Models;
using TennisWebsite.Models;

namespace TennisWebsite.Interfaces
{
    public interface IEventService
    {
        int EventCount { get; }
        Task<bool> CreateEventAsync(Event evnt);
        Task<bool> DeleteEventAsync(DateTime date);
        Task<List<Event>> GetAllAsync();
        Task<List<User>> GetAllParticipantsForEventAsync();
        Task<Event> GetEvent(DateTime date);
        Task<Event> GetEventByType(string type);
        Task<User> GetUserByEvent(User user); // so, for an event, find me this specific user
        Task<bool> UpdateEvent(User organiser, string name, string description, 
            string start, string end, int maxParticipants, int minParticipants);
    }
}

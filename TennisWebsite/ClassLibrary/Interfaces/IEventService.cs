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
        Task<bool> CreateEventAsync(Event newEvent);
        Task<bool> DeleteEventAsync(int id);
        Task<List<Event>> GetAllEventsAsync();
        Task<List<User>> GetParticipantsByEventAsync(int eventID);
        Task<Event> GetEvent(int id);
        Task<List<Event>> GetEventsByType(string type);
        Task<List<Event>> GetEventsByOrganiser(string orgName);
        Task<bool> JoinEvent(int eventID, string userName);
        Task<bool> LeaveEvent(int eventID, string userName);
        public Task<bool> UpdateEvent(int eventID, string organiser, string name, string description, 
            string start, string end, string type, int maxParticipants, int minParticipants);
    }
}

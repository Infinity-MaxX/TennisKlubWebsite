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
        Task<Event> GetEventAsync(int id);
        Task<List<Event>> GetEventsByTypeAsync(string type);
        Task<List<Event>> GetEventsByOrganiserAsync(string orgName);
        Task<bool> JoinEventAsync(int eventID, string userName);
        Task<bool> LeaveEventAsync(int eventID, string userName);
        public Task<bool> UpdateEventAsync(int eventID, string organiser, string name, string description, 
            string start, string end, string type, int maxParticipants, int minParticipants);
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;
using TennisWebsite.Interfaces;
using TennisWebsite.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TennisWebsite.Services
{
    public class EventService : IEventService
    {
        #region Instances
        List<Event> _events;
        #endregion

        #region Properties
        public int EventCount { get { return _events.Count; } }

        #endregion

        #region Constructors

        #endregion

        #region Methods
        public Task<bool> CreateEventAsync(Event evnt)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEventAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<List<Event>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllParticipantsForEventAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetEvent(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<Event> GetEventByType(string type)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByEvent(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEvent(User organiser, string name, string description, 
            string start, string end, int maxParticipants, int minParticipants)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

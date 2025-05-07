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
        //List<User> _participants;
        #endregion

        #region Properties
        public int EventCount { get { return _events.Count; } }
        #endregion

        #region Constructors

        #endregion

        #region Methods

        #endregion
    }
}

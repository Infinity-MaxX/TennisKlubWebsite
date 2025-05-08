using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLibrary.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TennisWebsite.Models
{
    public class Event
    {
        #region Instances
        private int _id;
        private User _organiser;
        private DateOnly _date;
        private DateTime _start;
        private DateTime _end;
        private List<User> _participants;
        #endregion

        #region Properties
        public int ID { get { return _id; } }
        public int ParticipantCount { get { return _participants.Count; } }
        public int MaxParticipants { get; set; }
        public int MinParticipants { get; set; }
        public User Organiser
        {
            get { return _organiser; }
            set { _organiser = value; }
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateOnly Date
        {
            get { return _date; }
        }
        public DateTime Start
        {
            get { return _start; }
            set { _start = value; }
        }
        public DateTime End
        {
            get { return _end; }
            set { _end = value; }
        }
        #endregion

        #region Constructors
        public Event()
        {
            _participants = new List<User>();
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"Event ID: {_id}\nEvent Organiser: {Organiser}\nEvent Name: {Name}" +
                $"\nEvent Description: {Description}\nEvent Type: {Type}\nEvent Date: {Date}" +
                $"\nTime of Event: From {Start} to {End}.";
        }
        #endregion
    }
}

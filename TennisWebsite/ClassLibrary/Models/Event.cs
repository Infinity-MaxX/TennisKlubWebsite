using System;
using System.Collections.Generic;
using System.Globalization;
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
        private DateTime _start;
        private DateTime _end;
        //private DateOnly _date;
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
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime Start { get { return _start; } }
        public DateTime End { get { return _end; } }
        //public DateOnly Date { get { return _date; } }
        #endregion

        #region Constructors
        // default constructor
        public Event()
        {
            _participants = new List<User>();
        }

        // parameterised constructors
        public Event(User organiser, string name, string type, string description,
            string start, string end, int maxParticipants, int minParticipants)
        {
            _organiser = organiser;
            Name = name;
            Type = type;
            Description = description;
            MaxParticipants = maxParticipants;
            MinParticipants = minParticipants;
            _participants = new List<User>();
            //_date = DateOnly.Parse(date);
            _start = DateTime.Parse(start);
            _end = DateTime.Parse(end);
        }
        public Event(int id, User organiser, string name, string type, string description,
            string start, string end, int maxParticipants, int minParticipants)
        {
            _id = id;
            _organiser = organiser;
            Name = name;
            Type = type;
            Description = description;
            MaxParticipants = maxParticipants;
            MinParticipants = minParticipants;
            _participants = new List<User>();
            //_date = DateOnly.Parse(date);
            _start = DateTime.Parse(start);
            _end = DateTime.Parse(end);
        }
        #endregion

        #region Methods
        public void AddParticipant(User participant)
        {
            _participants.Add(participant);
        }
        public void RemoveParticipant(User participant)
        {
            _participants.Remove(participant);
        }
        public override string ToString()
        {
            return $"Event ID: {_id}\nEvent Organiser: {Organiser}\nEvent Name: {Name}" +
                $"\nEvent Description: {Description}\nEvent Type: {Type}\nEvent Date: {Start.Date}" +
                $"\nTime of Event: From {Start.TimeOfDay} to {End.TimeOfDay}.";
        }
        #endregion
    }
}

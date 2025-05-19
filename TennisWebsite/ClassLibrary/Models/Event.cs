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
        private string _organiser;
        private DateTime _start;
        private DateTime _end;
        #endregion

        #region Properties
        public int ID { get { return _id; } }
        public int MaxParticipants { get; set; }
        public int MinParticipants { get; set; }
        public string Organiser
        {
            get { return _organiser; }
            set { _organiser = value; }
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime Start { get { return _start; } }
        public DateTime End { get { return _end; } }
        #endregion

        #region Constructors
        // default constructor
        public Event()
        {

        }

        // parameterised constructors
        public Event(string organiser, string name, string type, string description,
            string start, string end, int maxParticipants, int minParticipants)
        {
            _organiser = organiser;
            Name = name;
            Type = type;
            Description = description;
            MaxParticipants = maxParticipants;
            MinParticipants = minParticipants;
            _start = DateTime.Parse(start);
            _end = DateTime.Parse(end);
        }
        public Event(int id, string organiser, string name, string type, string description,
            DateTime start, DateTime end, int maxParticipants, int minParticipants)
        {
            _id = id;
            _organiser = organiser;
            Name = name;
            Type = type;
            Description = description;
            MaxParticipants = maxParticipants;
            MinParticipants = minParticipants;
            _start = start;
            _end = end;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"Event ID: {_id}\nEvent Organiser: {Organiser}\nEvent Name: {Name}" +
                $"\nEvent Description: {Description}\nEvent Type: {Type}\nEvent Date: {Start.Date}" +
                $"\nTime of Event: From {Start.TimeOfDay} to {End.TimeOfDay}.";
        }
        #endregion
    }
}

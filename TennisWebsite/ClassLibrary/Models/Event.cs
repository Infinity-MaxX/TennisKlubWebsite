using Microsoft.IdentityModel.Tokens;
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
        #endregion

        #region Properties
        public int ID { get { return _id; } set {_id = value; } }
        public int MaxParticipants { get; set; }
        public int MinParticipants { get; set; }
        public string Organiser { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        #endregion

        #region Constructors
        // default constructor
        public Event()
        {

        }


        public Event(int id)
        {
            ID = id;
        }

        // parameterised constructors
        public Event(string organiser, string name, string type, string description,
            string start, string end, int maxParticipants, int minParticipants)
        {
            Organiser = organiser;
            Name = name;
            Type = type;
            Description = description;
            MaxParticipants = maxParticipants;
            MinParticipants = minParticipants;
            Start = DateTime.Parse(start);
            End = DateTime.Parse(end);
        }
        public Event(int id, string organiser, string name, string type, string description,
            DateTime start, DateTime end, int maxParticipants, int minParticipants)
        {
            _id = id;
            Organiser = organiser;
            Name = name;
            Type = type;
            Description = description;
            MaxParticipants = maxParticipants;
            MinParticipants = minParticipants;
            Start = start;
            End = end;
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

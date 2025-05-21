using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisLibrary.Models
{
    public class Booking
    {
        #region Instance Fields
        private int _bookingID;
        #endregion

        #region Constructors
        public Booking()
        {

        }
        public Booking(User player1, User player2, Court courtName, DateTime start, string type)
        {
            Player1 = player1;
            Player2 = player2;
            Court = courtName;
            Start = start;
            End = start.AddHours(1);
            Type = type;
        }

        public Booking(User player1, User player2, Court courtName, DateTime start, DateTime end, string type)
        {
            Player1 = player1;
            Player2 = player2;
            Court = courtName;
            Start = start;
            End = end;
            Type = type;
        }
        #endregion

        #region Properties
        public User Player1 { get; set; }

        public User Player2 { get; set; }

        public Court Court { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Type { get; set; }

        public int ID { get; }

        #endregion

        #region Methods
        public override string ToString()
        {
            return $"Booking data\nBokee: {Player1.Name}\nCo-player: {Player2.Name}\nCourt: {Court.Name}\nStart time: {Start}\nEnd time: {End}";
        }
        #endregion
    }
}

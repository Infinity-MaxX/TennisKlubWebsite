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
        public Booking(User player1, User player2, Court courtName, DateTime start)
        {
            Player1 = player1;
            Player2 = player2;
            CourtName = courtName;
            Start = start;
            End = start.AddHours(1);
        }
        public Booking(User player1, User player2, Court courtName, DateTime start, DateTime end)
        {
            Player1 = player1;
            Player2 = player2;
            CourtName = courtName;
            Start = start;
            End = end;
        }
        #endregion

        #region Properties
        public User Player1 { get; set; }

        public User Player2 { get; set; }

        public Court CourtName { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int ID { get; }

        #endregion

        #region Methods
        public override string ToString()
        {
            return $"";
        }

        #endregion
    }
}

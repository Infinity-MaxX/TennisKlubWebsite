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

        #endregion

        #region Constructors
        public Booking(User player1, User player2, Court courtName, DateTime start, DateTime end)
        {
            player1 = Player1;
            player2 = Player2;
            courtName = CourtName;
            start = Start;
            end = End;
        }
        #endregion

        #region Properties
        public User Player1 { get; set; }

        public User Player2 { get; set; }

        public Court CourtName { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        #endregion

        #region Methods
        public override string ToString()
        {
            return $"";
        }

        #endregion
    }
}

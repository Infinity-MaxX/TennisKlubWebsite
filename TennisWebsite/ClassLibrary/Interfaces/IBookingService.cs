using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLibrary.Models;

namespace TennisLibrary.Interfaces
{
    public interface IBookingService
    {
        Task<bool> AddBookingAsync(Booking newBooking);

        /*Task<List<Booking>> GetAllBookingsAsync();

        Task<List<Booking>> GetBookingsByUserAsync(string Username);

        Task<bool> DeleteBooking();*/


    }
}

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
        Task<bool> AddBookingUserAsync(Booking newBooking);

        Task<bool> AddBookingAdminAsync(Booking newBooking);

        Task<List<Booking>> GetAllBookingsAsync();

        Task<List<Booking>> GetBookingsByUserAsync(string Username);

        Task<bool> DeleteBooking(Booking booking);

        Task<Booking> UpdateBooking(int ID, User Player2);

        Task<Booking> UpdateBooking(int ID, DateTime Start);
    }
}

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

        Task<bool> DeleteBooking(int ID);

        Task<bool> UpdateBooking(int ID, User Player2);

        Task<bool> UpdateBooking(int ID, DateTime Start);

        Task<bool> UpdateBookingAdmin(int ID, DateTime Start, DateTime End);
    }
}

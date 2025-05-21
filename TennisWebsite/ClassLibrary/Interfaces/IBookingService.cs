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

        Task<List<Booking>> GetAllBookingsAsync();

        Task<List<Booking>> GetBookingsByUserAsync(string Username);

        Task<List<Booking>> GetBookingsByPlayer2Async(string Username);

        Task<List<Booking>> GetBookingsByDatesAsync(DateTime Start, DateTime End);

        Task<List<Booking>> GetBookingsByDatesAndUserAsync(string Username, DateTime Start, DateTime End);
        
        Task<List<Booking>> GetBookingsByDatesAndUser2Async(string Username, DateTime Start, DateTime End);

        Task<List<Booking>> GetBookingsByTrainer(string Username);

        Task<List<Booking>> GetBookingsByType(string Type);

        Task<List<Booking>> GetBookingsByTypeAndDates(string Type, DateTime Start, DateTime End);

        Task<List<Booking>> GetBookingsByTrainerAndDates(string Username, DateTime Start, DateTime End);

        Task<bool> DeleteBooking(int ID);

        Task<bool> UpdateBookingPlayer2(int ID, User Player2);

        Task<bool> UpdateBookingStart(int ID, DateTime Start);

        Task<bool> UpdateBookingStartPlayer2(int ID, DateTime Start, User Player2);

        Task<bool> UpdateBookingTimeAdmin(int ID, DateTime Start, DateTime End);
        
        Task<bool> UpdateBookingTimeAndPlayer2Admin(int ID, User Player2, DateTime Start, DateTime End);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLibrary.Interfaces;
using TennisLibrary.Models;

namespace TennisLibrary.Services
{
    public class BookingService : IBookingService
    {
        Task<bool> IBookingService.AddBookingAsync(Booking newBooking)
        {
            throw new NotImplementedException();
        }

        Task<List<User>> IBookingService.GetAllBookingsAsync()
        {
            throw new NotImplementedException();
        }
    }
}

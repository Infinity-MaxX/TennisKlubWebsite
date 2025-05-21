using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLibrary.Models;
using TennisWebsite.ClassLibrary.Models;
namespace TennisWebsite.ClassLibrary.Interfaces;
public interface IGearService
{
    bool Status { get; set; }
    Task<bool> AddGear(Gear gear);
    Task<bool> BookGear(int bookingID, int gearID);
    //Task<bool> BorrowGear(int id);
    Task<bool> CheckStatus(int id);
    Task<bool> DeleteGear(int id);
    Task<List<Gear>> GetAllAsync();
    Task<Gear> GetByTypeAsync(string name);
    Task<Gear> GetGearAsync(int id);
    //Task<bool> ReturnGear(int id);
}
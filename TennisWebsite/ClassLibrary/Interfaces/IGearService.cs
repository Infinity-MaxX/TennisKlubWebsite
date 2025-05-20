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
    int Count { get; }
    bool Status { get; set; }
    Task<bool> AddGear(string name, string? description);
    Task<bool> BookGear(int bookingID, int gearID);
    //Task<bool> BorrowGear(int id);
    Task<bool> CheckStatus(int id);
    Task<bool> DeleteGear(int id);
    Task<List<Gear>> GetAll();
    Task<Gear> GetByIdAsync(int id);
    Task<Gear> GetByTypeAsync(string type);
    Task<Gear> GetGear(int id);
    //Task<bool> ReturnGear(int id);
}
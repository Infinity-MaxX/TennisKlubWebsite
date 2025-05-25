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
    Task<bool> AddGearAsync(Gear gear);
    Task<bool> BookGearAsync(int bookingID, int gearID, int count);
    Task<int> CheckStatusAsync(int gearID);
    Task<bool> DeleteGearAsync(int id);
    Task<List<Gear>> GetAllAsync();
    Task<Gear> GetByTypeAsync(string name);
    Task<Gear> GetGearAsync(int id);
}
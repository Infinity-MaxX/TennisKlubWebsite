using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisLibrary.Models;

namespace TennisLibrary.Interfaces
{
    public interface ICourtService
    {
        Task<bool> CreateCourtAsync(Court court);
        Task<Court> GetCourtAsync(string name);
        Task<List<Court>> GetCourtsOfTypeAsync(string type);
        Task<List<Court>> GetAllCourts();
        Task<bool> UpdateCourtAsync(string oldCourt, Court newCourt);
        Task<bool> DeleteCourtAsync(string name);
    }
}

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
        Task<List<Court>> GetCourtsOfTypeAsync(string name);
        Task<bool> UpdateCourtAsync(string name, Court newCourt);
        Task<bool> DeleteCourtAsync(string name);
    }
}
